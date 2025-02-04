using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inputFieldController : MonoBehaviour
{
    public InputField nameInput;
    public highScores highScoreRef;
    public Text userNameInput;


public void stopInputing(){
    nameInput.readOnly = true;
    string textInput = userNameInput.text;
    
    highScoreRef.addScore(mainGameController.points, textInput);
}


}

