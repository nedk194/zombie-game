using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid : MonoBehaviour
{
public node[,] Grid; // A 2d array of nodes
public LayerMask walls; // a layermask that is assigned a value in the inspector
void Start(){
    createGrid();
}
private void createGrid(){
    Grid = new node[78 ,76]; // creates a list of nodes in the dimensions of the grid
    Vector3 firstNodePos = new Vector3(-39,-38,0); //- Vector3.right * 39 - Vector3.up * 38; 
    for (int m = 1; m <= 77; m++){  //  loop within a loop that iterates for the dimensions of the 2d array of nodes
        for (int n = 1; n <= 75 ; n++){ // changed the bounds of the grid to improve performance
            Vector3 nodePosition = firstNodePos + Vector3.right * (m + 0.5f)   + Vector3.up  *(n + 0.5f); 
            bool walkable = !(Physics2D.BoxCast(nodePosition, new Vector2(0.9f, 0.9f), 0, new Vector2(0,0), 0, walls)); // does a box cast and returns true if  there is no wall and false if there is
            Grid[m,n] = new node (nodePosition, walkable, m, n); // instantiates a node in the correct position in the grid array
        }
    }
}
public node getGridLocation(Vector3 position){
    float xFraction = ((position.x + 39) / 78); 
    float yFraction = ((position.y + 38) / 76); // returns a fraction of how far into the grid it is where 1 is on the right and 0 is on the left
    
    xFraction = Mathf.Clamp01(xFraction);
    yFraction = Mathf.Clamp01(yFraction); // keeps the value inside of 0 and 1 to prevent errors
    int x = Mathf.RoundToInt(xFraction * (77)  ) ;
    int y = Mathf.RoundToInt(yFraction * (75)  ); // the fraction of how far across the grid * by the grids size returns the exact grid it is in 

    return Grid[x,y];
}
public List<node> getNeighbours(node currentNode){  // takes parameter of starting node
    List<node> neighbours = new List<node>();  // creates a list of nodes that the neighbours can be added to 
    for (int x = -1; x <= 1; x++){
        for (int y = -1; y <= 1; y++){  // a loop within a loop that start from either side of the nodes x and y coordinate creating a 3x3 cube
            if (x == 0 && y == 0){  // diregards the starting node
                continue;   }
            
            int xCheck = currentNode.xGrid + x;  // a variable representing the neighbours x coordinate
            int yCheck = currentNode.yGrid + y;  // same for y

            if ( (xCheck >= 1 && 78 >= xCheck)  &&  (yCheck >= 1 && 75 >= yCheck)  ){  // checks that both x and y coordinate for neighbour are within the bounds 
                neighbours.Add(Grid[xCheck, yCheck] );  // if neighbours r within grid array then add them to neighbours list to be outputted
            }
        }
    }
    return neighbours;
}
}
