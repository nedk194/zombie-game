using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ammoTextController : MonoBehaviour
{
public Text ammoCount;

    public void setAmmo(int ammo, int maxAmmo){
        ammoCount.text = (ammo.ToString() + "/" + maxAmmo.ToString());
    }
}
