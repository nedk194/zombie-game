using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject bulletClass;
    public Transform firePoint;
    void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            shoot();
        }
    }

    private void shoot(){
        GameObject bullet = Instantiate(bulletClass, firePoint.position, firePoint.rotation );
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(firePoint.up * 20f, ForceMode2D.Impulse);
    }
}
