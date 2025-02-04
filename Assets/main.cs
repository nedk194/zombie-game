using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class main : MonoBehaviour  // controls the menus
{
    public Canvas endScreen;  // reference to each screen
    public Canvas startScreen;
    public Canvas boardScreen;

    void Start(){
        startScreen.gameObject.SetActive(true);// on start set startscreen as active and the others as
        endScreen.gameObject.SetActive(false);
        boardScreen.gameObject.SetActive(false);
    }
    void Update(){ // always checking
        if (player.justDied == true){ // when players died
            startScreen.gameObject.SetActive(false); 
            endScreen.gameObject.SetActive(true);  // set endscreen to active and the others false
            boardScreen.gameObject.SetActive(false);
            
            player.justDied = false; // once end screen is active, set justDied to false
        }
    }
        
public void onStartPress() // method assigned to start game button
    {
        SceneManager.LoadScene("Main"); // loads main scene
    }
public void onExitPress() // exits game when exit is pressed
    {
        Application.Quit();
    }
}

