using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class leaderboardScreenController : MonoBehaviour
{
    public void onLBackPress(){
        SceneManager.LoadScene("Menu");
    }
}
