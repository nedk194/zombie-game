using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class roundNumTextContrroller : MonoBehaviour
{
public Text roundDisplay;

    public void setRoundDisplay(int round){
        roundDisplay.text = round.ToString();
    }
}

