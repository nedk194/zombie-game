using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{

public GameObject enemyClass; // reference to enemy game object
public Vector3[ ] spawnPoints; // array of vector3s
public int round; // integer in this class that is assigned to be equal to the round variable in main game controler class

void Start(){ 
   spawnPoints = new Vector3[ ]{new Vector3(-11f, -32.85f, 0),   // initialises the array with all these locations 
                                new Vector3(11f, -32.85f, 0),
                                new Vector3(11f, 31.8f, 0),
                                new Vector3(-11f, 30f, 0),
                                new Vector3(-36.1f, 7.8f, 0),  
                                new Vector3(-36.1f, -9.5f, 0),
                                new Vector3(35.9f, -9.5f, 0),
                                new Vector3(35.9f, 7.9f, 0)  };
}
    public void spawnEnemy(){ // public as its called in main game controller
        int r = Random.Range(0, 8); // random number between 1 and 8
        GameObject en = Instantiate(enemyClass, spawnPoints[r], transform.rotation );  // en is an instance of enemy class, spawned in a randopm location rfom the list of vectors
        enemyController enControl = en.GetComponent<enemyController>();  // reference to enemy controller class
        enControl.setEnemySpeed(    Random.Range(1, round * 1.007f)  );  // calls the set enemy speed method and sets it between 1 and round number x 1.01, so enemies get progressivly harde
        enControl.setEnemyHealth(   (3 * round) +15 );  // sets enemy health relative to round, 
    }

}
