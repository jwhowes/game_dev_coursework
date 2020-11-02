using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingNavMesh : MonoBehaviour{
    private class Node
    {
        public Vector3 pos;  // The world position of the node
        public bool isObstacle;

        public float path_cost;
        public float dist_to_target;
        public Node prev;
    }
    [SerializeField] private float width, height, depth;
    [SerializeField] private float voxelLength;
    private Node[,,] nodes;
    [SerializeField] private LayerMask obstacles;
    [System.NonSerialized] public int numWidth, numHeight, numDepth;
    
    void Start(){
        numWidth = (int)(width/voxelLength);
        numHeight = (int)(height/voxelLength);
        numDepth = (int)(depth/voxelLength);
        nodes = new Node[numWidth, numHeight, numDepth];
        for(int x = 0; x < numWidth; x++){
            for(int y = 0; y < numHeight; y++){
                for(int z = 0; z < numDepth; z++){
                    nodes[x, y, z] = new Node();
                    nodes[x, y, z].isObstacle = Physics.CheckBox(VoxelToWorld(x, y, z), new Vector3(1, 1, 1) * voxelLength/2, Quaternion.identity, LayerMask.GetMask("Terrain"));
                    nodes[x, y, z].pos = VoxelToWorld(x, y, z);
                }
            }
        }
    }
    public Vector3 VoxelToWorld(int x, int y, int z)
    {
        return (new Vector3(x, y, z) - new Vector3(numWidth, numHeight, numDepth)/2) * voxelLength/2;
    }
    private Vector3 VoxelToWorld(Vector3 v)
    {
        return VoxelToWorld((int)v.x, (int)v.y, (int)v.z);
    }
    public Vector3 WorldToVoxel(Vector3 worldPos)
    {
        Vector3 ret = (worldPos * 2/voxelLength) + new Vector3(numWidth, numHeight, numDepth)/2;
        return new Vector3(Mathf.Floor(ret.x), Mathf.Floor(ret.y), Mathf.Floor(ret.z));
    }
    private Node WorldToNode(Vector3 pos){
        Vector3 v = WorldToVoxel(pos);
        return nodes[(int)v.x, (int)v.y, (int)v.z];
    }
    public List<Vector3> GetPath(Vector3 start, Vector3 target){  // Improve with a heap
        // Also use a data structure to store parents (that way I can remove values from fringe which stops them being searched again).
        // Won't need prev list
        // The data structure can probably just be a vector and a pointer to previous (in path)
        // Should also experiment with storing everything in the data structure (so it's a vector, a path cost, a dist to target, and a pointer)
        for(int x = 0; x < numWidth; x++){
            for(int y = 0; y < numHeight; y++){
                for(int z = 0; z < numDepth; z++){
                    nodes[x, y, z].path_cost = 0;
                    nodes[x, y, z].dist_to_target = Vector3.Distance(nodes[x, y, z].pos, target);
                    nodes[x, y, z].prev = null;
                }
            }
        }
        List<Node> fringe = new List<Node>();  // TODO: Replace with a heap
        List<Vector3> path = new List<Vector3>();
        path.Add(target);
        HashSet<Node> explored = new HashSet<Node>();
        fringe.Add(WorldToNode(start));
        int a = 0;
        Node curr = null;
        while(fringe.Count > 0){
            int current = 0;
            for(int i = 1; i < fringe.Count; i++){
                if(fringe[i].path_cost + fringe[i].dist_to_target < fringe[current].path_cost + fringe[current].dist_to_target || (fringe[i].path_cost + fringe[i].dist_to_target == fringe[current].path_cost + fringe[current].dist_to_target && fringe[i].dist_to_target < fringe[i].dist_to_target)){
                    current = i;
                }
            }
            curr = fringe[current];
            fringe.RemoveAt(current);
            explored.Add(curr);
            if(curr.dist_to_target < voxelLength){
                while(curr != null){
                    path.Insert(0, curr.pos);
                    curr = curr.prev;
                }
                return path;
            }
            foreach(Node v in Neighbours(curr)){
                if(!explored.Contains(v)){
                    if(fringe.Contains(v)){
                        if(!explored.Contains(v) && curr.path_cost + Vector3.Distance(curr.pos, v.pos) < v.path_cost){
                            v.path_cost = curr.path_cost + Vector3.Distance(curr.pos, v.pos);
                            v.prev = curr;
                        }
                    }else{
                        fringe.Add(v);
                        v.path_cost = curr.path_cost + Vector3.Distance(curr.pos, v.pos);
                        v.prev = curr;
                    }
                }
            }
        }
        return path;
    }
    private List<Node> Neighbours(Node n){
        List<Node> neighbours = new List<Node>();
        Vector3 node_voxel = WorldToVoxel(n.pos);
        int n_x = (int)node_voxel.x;
        int n_y = (int)node_voxel.y;
        int n_z = (int)node_voxel.z;
        for(int x = -1; x < 2; x++){
            for(int y = -1; y < 2; y++){
                for(int z = -1; z < 2; z++){
                    if(!((x == 0 && y == 0 && z == 0) || n_x + x < 0 || n_x + x >= numWidth || n_y + y < 0 || n_y + y >= numHeight || n_z + z < 0 || n_z + z >= numDepth || nodes[n_x + x, n_y + y, n_z + z].isObstacle)){
                        neighbours.Add(nodes[n_x + x, n_y + y, n_z + z]);
                    }
                }
            }
        }
        return neighbours;
    }
}
