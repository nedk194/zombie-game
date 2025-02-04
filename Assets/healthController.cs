using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthController : MonoBehaviour
{
    void hit(){
        Destroy(gameObject);
    }
    void Start(){
        Invoke("delete", 10);
    }

    void delete(){
        Destroy(gameObject);
    }
}
