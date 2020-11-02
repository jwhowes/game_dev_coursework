using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingNavMeshOld : MonoBehaviour{
    [SerializeField] private float width, height, depth;
    [SerializeField] private float voxelLength;
    public bool[,,] isObstacle;
    [SerializeField] private LayerMask obstacles;
    [System.NonSerialized] public int numWidth, numHeight, numDepth;
    void Start(){
        numWidth = (int)(width/voxelLength);
        numHeight = (int)(height/voxelLength);
        numDepth = (int)(depth/voxelLength);
        isObstacle = new bool[numWidth, numHeight, numDepth];
        for(int x = 0; x < numWidth; x++){
            for(int y = 0; y < numHeight; y++){
                for(int z = 0; z < numDepth; z++){
                    isObstacle[x, y, z] = Physics.CheckBox(VoxelToWorld(x, y, z), new Vector3(1, 1, 1) * voxelLength/2, Quaternion.identity, LayerMask.GetMask("Terrain"));
                }
            }
        }
    }
    public Vector3 VoxelToWorld(int x, int y, int z){
        return (new Vector3(x, y, z) - new Vector3(numWidth, numHeight, numDepth)/2) * voxelLength/2;
    }
    private Vector3 VoxelToWorld(Vector3 v){
        return VoxelToWorld((int)v.x, (int)v.y, (int)v.z);
    }
    public Vector3 WorldToVoxel(Vector3 worldPos){
        Vector3 ret = (worldPos * 2/voxelLength) + new Vector3(numWidth, numHeight, numDepth)/2;
        return new Vector3(Mathf.Round(ret.x), Mathf.Round(ret.y), Mathf.Round(ret.z));
    }
    public List<Vector3> GetPath(Vector3 start, Vector3 target){  // Improve with a heap
        // Also use a data structure to store parents (that way I can remove values from fringe which stops them being searched again).
            // Won't need prev list
            // The data structure can probably just be a vector and a pointer to previous (in path)
            // Should also experiment with storing everything in the data structure (so it's a vector, a path cost, a dist to target, and a pointer)
        List<Vector3> fringe = new List<Vector3>();
        List<float> path_cost = new List<float>();
        List<float> dist_to_target = new List<float>();
        List<int> prev = new List<int>();
        List<Vector3> path = new List<Vector3>();
        HashSet<int> explored = new HashSet<int>();
        fringe.Add(start);
        path_cost.Add(0f);
        dist_to_target.Add(Vector3.Distance(start, target));
        prev.Add(-1);
        int current = 0;
        while(explored.Count < fringe.Count){
            // Find voxel in fringe with lowest f value (by index in fringe)
            current = -1;
            for(int i = 0; i < fringe.Count; i++){
                if(!explored.Contains(i)){  // Can remove this check once data structure is set up
                    if(current == -1){
                        current = i;
                    }else if(path_cost[i] + dist_to_target[i] < path_cost[current] + dist_to_target[current] || (path_cost[i] + dist_to_target[i] == path_cost[current] + dist_to_target[current] && dist_to_target[i] < dist_to_target[i])){  // TODO: If they're equal, select the one with lowest dist_to_target
                        current = i;
                    }
                }
            }
            // Remove current from fringe, path_cost, dist_to_target (once data structure is set up)
            explored.Add(current);
            if(dist_to_target[current] <= voxelLength){
                path.Add(target);
                while(current != -1){
                    path.Insert(0, fringe[current]);
                    current = prev[current];
                }
                return path;
            }
            foreach(Vector3 v in Neighbours(WorldToVoxel(fringe[current]))){  // Will need to change this as not in fringe doesn't imply not in explored (once data structure is set up)
                if(fringe.Contains(v)){
                    int index = fringe.IndexOf(v);
                    if(!explored.Contains(index) && path_cost[current] + Vector3.Distance(fringe[current], v) < path_cost[index]){
                        path_cost[index] = path_cost[current] + Vector3.Distance(fringe[current], v);
                        prev[index] = current;
                    }
                }else{
                    fringe.Add(v);
                    path_cost.Add(path_cost[current] + Vector3.Distance(fringe[current], v));
                    dist_to_target.Add(Vector3.Distance(v, target));
                    prev.Add(current);
                }
            }
        }
        return null;
    }
    private List<Vector3> Neighbours(Vector3 v){
        List<Vector3> neighbours = new List<Vector3>();
        for(int x = -1; x < 2; x++){
            for(int y = -1; y < 2; y++){
                for(int z = -1; z < 2; z++){
                    if(!((x == 0 && y == 0 && z == 0) || v.x + x < 0 || v.x + x >= numWidth || v.y + y < 0 || v.y + y >= numHeight || v.z + z < 0 || v.z + z >= numDepth || isObstacle[(int)v.x + x, (int)v.y + y, (int)v.z + z])){
                        neighbours.Add(VoxelToWorld(v + new Vector3(x, y, z)));
                    }
                }
            }
        }
        return neighbours;
    }
}
