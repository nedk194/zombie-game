using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class inGameUIController : MonoBehaviour
{
    public Canvas self;
    
    void Update(){
        if ( SceneManager.GetActiveScene().name != "Main"){
            disableUI();
        } 
        else{
            enableUI();
        }
    }
    public void disableUI(){
        self.enabled = false;
    }
    public void enableUI(){
        self.enabled = true;
    }
}
