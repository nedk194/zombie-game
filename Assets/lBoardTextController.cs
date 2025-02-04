using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lBoardTextController : MonoBehaviour
{
    public Text selfText;
    public void setText(string text){
        selfText.text = text;
    }
}