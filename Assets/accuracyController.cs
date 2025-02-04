using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class accuracyController : MonoBehaviour
{
    public Vector3 tempAngle; // a vector 3 that stores the original angle before it is randomized
    public void setAccuracy(int bounds){ // the bounds directly translate to angles
        tempAngle = transform.eulerAngles; // sets tempAngle as current rotation
        transform.eulerAngles += new Vector3(0,0, Random.Range(- bounds, bounds) ); // sets rotation angle to be the current rotation + a random value between the bounds
    }
    public void setRotation(){  // resets the rotation to tempAngle
        transform.eulerAngles = tempAngle;
    }
}
