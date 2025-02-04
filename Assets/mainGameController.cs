using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainGameController : MonoBehaviour
{
    float timeCount;
    public static int score;  // static as its accessed through multiple scenes and multiple classes, also there should only be 1 instance of score
    public static int points;  // same with points, points is the same as score but are spent when upgrading
    public enemySpawn spawn;  // reference to enemy spawn class
    public int round; // integer value for what round player is on
    private float enemyCount;  // represents how many enemies are left
    public roundNumTextContrroller roundDisp; // references to UI elements
    public pointsNumController scoreDisp;
    public scoreNumTextContrroller pointsDisp;
    float elapsedTime;  // the time since last enemy was spawned
    
    void Start() // initialisation
    {
        round = 0;
        timeCount =0;
        roundDisp.setRoundDisplay(round);
        elapsedTime = 0;
        score = 0;
        points = 0;
    }

    void Update()
    {
        enemyCount = (GameObject.FindGameObjectsWithTag("Enemy")).Length; // always checking how many enemies there are
        timeCount += Time.deltaTime;
        scoreDisp.setScoreDisplay(score);  // update UI
        pointsDisp.setScoreDisplay(points);
        
        if (enemyCount <= 0){ // when there are no enemies

            elapsedTime += Time.deltaTime; // increase elapsed time by the time betweeen frames (so it shows real time)
            if(elapsedTime >= 2){  // wait 2 seconds
                round += 1; // increase round
                score += 100; // increase score and points
                points += 100;
                elapsedTime =0; // reset elapsed time
                spawn.round = round; // apdate round for enemy spawn class
                roundDisp.setRoundDisplay(round); // update round display
                if(round <= 3){ // the first 3 rounds behave differently to the rest 
                    startRound(round); // call start round function
                    }
                else{
                    StartCoroutine( startRoundHigher(round) ); // after round three, start round higher is called with start corotine as it is IEnumerator 
                    }
                }
                
        }
    }

    void startRound(int roundNum){ // for rounds 1 - 3
        for (int i = 0; i < roundNum; i++)
            {
                spawn.spawnEnemy(); // spawn enemies = to round number
            }
    }
    IEnumerator startRoundHigher(int roundNum){
        spawn.spawnEnemy(); // spawn an initial enemy so the update function doesnt call function straight away as enemy count >0
        for (int i = 0; i < Mathf.RoundToInt(roundNum * 1.5f); i++) // spawns round number x 1.5 amount of enemies (rounded to int)
        {
            yield return new WaitForSeconds(Random.Range(1, 5) ); // wait 3- 7 seconds
            
            spawn.spawnEnemy(); // spawn 2 enemies
            spawn.spawnEnemy();
        }
        
    }
}
