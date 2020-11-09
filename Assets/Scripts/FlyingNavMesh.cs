using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class FlyingNavMesh : MonoBehaviour{
    private class Node{
        public Vector3 pos;  // The world position of the node
        public bool isObstacle;

        public float path_cost;
        public float dist_to_target;
        public Node prev;

        public int heapIndex;
    }
    private class Heap{
        public Node[] nodes;
        private int length = 0;
        public Heap(int maxLength){
            nodes = new Node[maxLength];
        }
        public Node Dequeue(){
            Node first = nodes[0];
            length--;
            nodes[0] = nodes[length];
            nodes[0].heapIndex = 0;
            Heapify(0);
            return first;
        }
        private void Heapify(int v){
            int smallest = -1;
            while(smallest != v){
                smallest = v;
                if(2*v + 1 < length && Better(2*v + 1, v)){
                    smallest = 2*v + 1;
                }
                if(2*v + 2 < length && Better(2*v + 2, v)){
                    smallest = 2*v + 2;
                }
                if(smallest != v){
                    nodes[smallest].heapIndex = v;
                    nodes[v].heapIndex = smallest;
                    Node temp = nodes[smallest];
                    nodes[smallest] = nodes[v];
                    nodes[v] = temp;
                    v = smallest;
                }
            }
        }
        public void Enqueue(Node node){
            node.heapIndex = length;
            nodes[length] = node;
            BubbleUp(length);
            length++;
        }
        public bool Contains(Node node){
            return node.heapIndex < length && Equals(nodes[node.heapIndex], node);
        }
        public int Count(){
            return length;
        }
        public void Requeue(Node node){
            BubbleUp(node.heapIndex);
        }
        private void BubbleUp(int v){
            int parent = (v - 1)/2;
            while(v > 0 && Better(v, parent)){
                nodes[parent].heapIndex = v;
                nodes[v].heapIndex = parent;
                Node temp = nodes[parent];
                nodes[parent] = nodes[v];
                nodes[v] = temp;
                v = parent;
                parent = (v - 1)/2;
            }
        }
        private bool Better(int i, int j){
            return nodes[i].path_cost + nodes[i].dist_to_target < nodes[j].path_cost + nodes[j].dist_to_target || (nodes[i].path_cost + nodes[i].dist_to_target == nodes[j].path_cost + nodes[j].dist_to_target && nodes[i].dist_to_target < nodes[i].dist_to_target);
        }
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
        Debug.Log(VoxelToWorld(0, 0, 0));
    }
    public Vector3 VoxelToWorld(int x, int y, int z){
        return (new Vector3(x, y, z) - new Vector3(numWidth, numHeight, numDepth)/2) * voxelLength/2;
    }
    private Vector3 VoxelToWorld(Vector3 v){
        return VoxelToWorld((int)v.x, (int)v.y, (int)v.z);
    }
    public Vector3 WorldToVoxel(Vector3 worldPos){
        Vector3 ret = (worldPos * 2/voxelLength) + new Vector3(numWidth, numHeight, numDepth)/2;
        return new Vector3(Mathf.Floor(ret.x), Mathf.Floor(ret.y), Mathf.Floor(ret.z));
    }
    private Node WorldToNode(Vector3 pos){
        Vector3 v = WorldToVoxel(pos);
        return nodes[(int)v.x, (int)v.y, (int)v.z];
    }
    public List<Vector3> GetPath(Vector3 start, Vector3 target){
        for(int x = 0; x < numWidth; x++){
            for(int y = 0; y < numHeight; y++){
                for(int z = 0; z < numDepth; z++){
                    nodes[x, y, z].path_cost = 0;
                    nodes[x, y, z].dist_to_target = Vector3.Distance(nodes[x, y, z].pos, target);
                    nodes[x, y, z].prev = null;
                }
            }
        }
        Heap fringe = new Heap(numWidth*numHeight*numDepth);
        List<Vector3> path = new List<Vector3>();
        HashSet<Node> explored = new HashSet<Node>();
        path.Add(target);
        fringe.Enqueue(WorldToNode(start));
        while(fringe.Count() > 0){
            Node curr = fringe.Dequeue();
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
                        if(curr.path_cost + Vector3.Distance(curr.pos, v.pos) < v.path_cost){
                            v.path_cost = curr.path_cost + Vector3.Distance(curr.pos, v.pos);
                            v.prev = curr;
                            fringe.Requeue(v);
                        }
                    }else{
                        fringe.Enqueue(v);  // For some reason, when the enqueue is here it works but when it's after the assignment it doesn't
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
