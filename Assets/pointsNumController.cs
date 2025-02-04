using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pointsNumController : MonoBehaviour
{
public Text scoreDisplay;

void Update(){
    setScoreDisplay(mainGameController.points);
}
    public void setScoreDisplay(int score){
        scoreDisplay.text = score.ToString();
    }
}
