using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreNumTextContrroller : MonoBehaviour
{
public Text pointsDisplay;

    public void setScoreDisplay(int score){
        pointsDisplay.text = score.ToString();
    }
}
