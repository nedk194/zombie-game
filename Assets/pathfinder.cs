using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathfinder : MonoBehaviour
{

grid Grid; // Grid is an instance of the grid class, so creates its own grid for every instance of pathfinder
void Awake(){
Grid = GetComponent<grid>(); // gives components from grid to Grid, so grid class's methods can be referenced 
}
public void pathfinding(Vector3 start, Vector3 end){ // takes parameters of two locations in the world
     node startNode = Grid.getGridLocation(start); // converts both locations into grid coordinates by calling function from Grid 
     node endNode = Grid.getGridLocation(end); // A reference to grid class is used through Grid variable
     if (   (startNode != null) && (endNode != null)    ){ // this catches all errors for when getGridLocation cant find the node as I was getting lots of console errors  
        List<node> open = new List<node>(); //list of nodes that can hold all nodes open to be next current node
        HashSet<node> closed = new HashSet<node>(); // hashSet is used as a hash set does bare minimum needed, can use contains function. tried with list but slowed performace greatly

        open.Add(startNode); // adds the starting node to the open set

        while (open.Count != 0){  // while there are nodes in open (nodes eligable to be next currentNode)
            node currentNode = open[0];  // puts current node at front of the list 
            foreach (node n in open) {  // checks every node in open 
                if (n.fValue() < currentNode.fValue() ||( n.fValue() == currentNode.fValue() && n.hCost < currentNode.hCost )  ){  //  either if a node has smaller fValue (sum of h and g), or same fValue but smaller hCost (guess of distance to endNode)
                    currentNode = n; // if true then set new current node
                }   }
            
            open.Remove(currentNode); // remove current node from open list, as it has already been considered
            closed.Add(currentNode); // adds current node to closed set so it will not be re-added to open when checking for neighbours

            if (currentNode == endNode){  // checks if end node has been found
                
                constructPath(startNode, endNode); // construct path doesnt output anything, it just updates the value of path, given the parameters found in pathfinding
                // this functions job is to show the route between start and end by making the next node in the route the parent of the last, 
                // the construct path function uses the parents to construct the route into a list of nodes that is readable for the enemyController class
            }
            foreach (node neighbour in Grid.getNeighbours(currentNode)){  // if end not yet found, call getNeighbour function from grid and loop for every neighbour found
                if(!neighbour.walkable || closed.Contains(neighbour)    ){  //  disregarding all neighbours that are either not walkable or in closed (already been considered)
                    continue;
                }

                int movementCostToNeighbour = currentNode.gCost + getDistance(currentNode, neighbour); // distance from start node to neighbour node, via currant node
                if (movementCostToNeighbour < neighbour.gCost || !open.Contains(neighbour)){ // if this route is new best route to neighbour

                neighbour.gCost = movementCostToNeighbour;
                neighbour.hCost = getDistance(neighbour, endNode); // these 3 lines update the neighbour node's attributes as a new route has been found to them 
                neighbour.parent = currentNode; // meaning the previous (if there was one) route is redundant, as this is faster

                if (!open.Contains(neighbour)){ // adds neighbour to open if this is first time visiting it 
                    open.Add(neighbour);
                }
                }
            }      
        }
    }   
}
int getDistance(node start, node end){
    int xDistance = Mathf.Abs(start.xGrid - end.xGrid);  // difference in x coordiantes, abs is used so value is never negative  
    int yDistance = Mathf.Abs(start.yGrid - end.yGrid);  // difference in y values 
    
    if (xDistance > yDistance){   // compare difference in x and y 
        return(14 * yDistance + 10*(xDistance - yDistance) ); // equation changes accordingling 
        }
    else {return 14*xDistance + 10*(yDistance - xDistance); } // if xDistance and yDistance are = then both equations are valid 

}
public List<node> path; // path is a list of nodes as oppose to a basic array as the length of the array is unknown  
void constructPath(node start, node end){
    path = new List<node>(); // list of nodes 
    node n = end; // a node that will traverse through the path

    while (n != start){ // loops until worked back to start node
        path.Add(n); // adds current node
        n = n.parent; // n traverses to the parent 
    }
    path.Reverse(); // path is in reverse order so the reverse function puts in correct order
    }
public List<node> getPath(Vector3 start, Vector3 end){ // this is a function that can be callable from the enemyController class returning the path for this instance of pathfinding
    pathfinding(start, end);
    return path;
}
}