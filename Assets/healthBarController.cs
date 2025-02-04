using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBarController : MonoBehaviour
{

    public Slider healthBar;
    public void setHealth(int health){
        healthBar.value = health;
    }
    public void setMaxHealth(int maxHealth){
        healthBar.maxValue = maxHealth;
    }

}
