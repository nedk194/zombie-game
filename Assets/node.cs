using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node 
{
    public int gCost; // distance from node to start node
    public int hCost;  // rough distance from node to end node 
    public bool walkable;  // boolean for if node is walkable or not
    public node parent;  // node that came before this node in the quickest route
    public int xGrid;  // x cooridante in the grid of nodes
    public int yGrid; // y cooridnate in grid of nodes
    public Vector3 position;  // its vector3 position
    public node(Vector3 _position, bool _walkable, int _xGrid, int _yGrid){  // constructor method, means class can act as a variable type that takes these parameters
        walkable = _walkable;
        position = _position;
        xGrid = _xGrid;
        yGrid = _yGrid;

    }
    public int fValue(){ // returns h+g costs
        return hCost + gCost;
    }
    public Vector3 getPosition(){ // returns position relative to world 
        return position;
    }

}