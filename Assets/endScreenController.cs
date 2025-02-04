using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endScreenController : MonoBehaviour
{
    public void onBackPress(){
        SceneManager.LoadScene("Menu");
    }
    public void onBoardPress(){
        SceneManager.LoadScene("Leaderboard");
    }
    public void resetJustDied(){
        player.justDied = false;
    }
}
