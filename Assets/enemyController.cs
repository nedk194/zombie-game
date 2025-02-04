using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    
public float enemySpeed; // float as it increases by varying percentages
public List<node> path1; // list of nodes, as oppose to array as it is already in that format, from pathfinder class
private pathfinder pathfinderRef; // an instance of pathfinder
private Vector3 playerPos;
private int nodeCount; // a counter variable for the enemies progress through the path1 list, used in different functions
private Vector3 tempPos; // the position the enemy has come from, normaly the last node location, unless path was just recalculated, 
float xRotation; // float as the rotation should look smooth, not jumping from integer to integer
float enemyHealth; // float as it is increased by variying percentages that are unknown 
public GameObject healthPickup; // reference to healthPickup object
public GameObject ammoPickup; // reference to ammo pickup object

void Awake(){ // called as soon as instantiated, before anything else
    pathfinderRef = GetComponent<pathfinder>(); // get the pathfinder component and assign it to pathfinderRef
}
    
private void Start(){ // called after awake
    playerPos = GameObject.Find("survivor").transform.position; // reference is assigned this way as instances 
    InvokeRepeating("calculatePath" , 0, 1); // this function calls the claculatePath method after 0.1 second and every 1 second after
}
public float timer;
void Update(){
    playerPos = GameObject.Find("survivor").transform.position;
    Vector3 targetPosition = path1[nodeCount].getPosition(); //sets the target position to the item in path1 under index of nodeCount .getposition
    
    transform.position = Vector3.Lerp( tempPos , targetPosition, timer * enemySpeed   ); // sets the position to a linear interpolation between tempPos and targetPosition
    // lerp function returns value that is between two points by timer* speed, so when timer*speed = 0 it would have reached targetPosition
    timer += Time.deltaTime; // the timer variable counts up real world time, representing the enemies progress from tempPos to targetPosition

    if (transform.position == targetPosition){ // when interpolation has reached the target position
        timer = 0; // reset timer so it starts from 0 again 
        nodeCount += 1; // make the tagetPosition the location of the next node in the list
        tempPos = transform.position;  // mkaes the previous position = to current position, so interpolation is between 2 nodes, not from original node
    }

    transform.eulerAngles = new Vector3 (0,0, getXRotation(transform.position, playerPos)); // set direction to look at player

}



void calculatePath(){ // called every second, recalculates the fastest route to the player
    path1 = pathfinderRef.getPath(transform.position , playerPos);  // calls getPath, which calls the pathfinding algorithm passing enemy position and player position
    nodeCount = 0; // resets node count                              route is variable type pathfinder therfore inheriting all its methods and attributes
    timer = 0; // reset timer
    tempPos = transform.position; // reset tempPos

}

private float getXRotation(Vector3 enemyPos,  Vector3 playerPos){ // the same as getXRotation for player but modified to be realtive to enemy and player positions
    float opp = playerPos.x - enemyPos.x;
    float adj = playerPos.y - enemyPos.y; // the verticies of the right angle triangle are relative to enemy and player
    

    if (adj > 0)
    {
        xRotation = (Mathf.PI/2 - Mathf.Atan(opp / adj) );
    }
    else if (adj <= 0)
    {
        xRotation = (Mathf.PI*(3/2) - Mathf.Atan(opp / adj) + Mathf.PI/2);
    }
            
            
    return (xRotation * Mathf.Rad2Deg); 
    

}

void OnCollisionEnter2D(Collision2D bullet){ // when collision is entered
    if(bullet.gameObject.name == "bullet(Clone)"){ // if its with a bullet
    enemyHit(); // call hit method
    }

}
private void enemyHit(){ // when enemy collids with bullet
    enemyHealth = enemyHealth - player.damage; // health is minus player static method for damage
    mainGameController.score +=1; // update static attibutes for points and score
    mainGameController.points +=1;
    
    if ( enemyHealth <= 0){  // if health is below 0
        mainGameController.score +=10; // update score and points
        mainGameController.points +=10; 
        if (1 == Random.Range(0, 8)){  // a 1 in 8 chance 
            Instantiate(healthPickup, transform.position, transform.rotation);  // instatiate a health pickup in the location the enemy was
        }
        else{}
            if(1 == Random.Range(0, 5)){ // if not dropped a health pickup, 1 in 5 chance to drop an ammo pickup
                Instantiate(ammoPickup, transform.position, transform.rotation);
            }     
        Destroy(gameObject); // finaly detroy this instance of the enemy
    }
}

public void setEnemySpeed(float speed){ // method is public as it is used in the enemy spawn class
    enemySpeed = speed; 
}
public void setEnemyHealth(float health){ // public as its used in enemy spawn class
    enemyHealth = health;
}
void OnDrawGizmos(){ // used to draw the path in the editor view of the game 
    if(path1 != null){
        for (int i = 0; i < path1.Count -1; i++)
        {
            Gizmos.DrawLine(path1[i].getPosition(), path1[i+1].getPosition() );
        }
    }
}

}