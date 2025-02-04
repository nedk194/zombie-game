using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
private int health; // holds player health
private int maxHealth; // holds players maximum health
public healthBarController healthBarUI; // refernce to health bar
public ammoTextController ammoTextUI;  // reference to ammo ui
public mainGameController plMainRef; // reference to main game controller
public static int damage; // damage each bullet does, static as it is accessed in enemyController
public static bool justDied; // boolean value for wheather the player has just died, used to determin if end screen should be diplayed, static as needed from different scene
public static int finalScore;// used to hold the score on player death, static as its used on multiple scenes
private float speed;  // float value for how far player moves each frame
private int ammo; // integer for bullets left to fire
private int maxAmmo; // integer for maximum bullets player can hold
private float fireRate; // float value for bullets fired per second fire1 is held down 
private float lastFired; // float value that stores the time a bullet was last fired
private Vector3 move; // vector3 value holding information on where user wants the player to move
private Vector3 mousePos; // hold the vector position of the mouse, from the orogin
private BoxCollider2D coll; // reference to the 2d box collider of the player
private RaycastHit2D block; // reference to the rayCast type
private float xRotation; // holds value of rotation from x axis (actualy the z axis but appers as the x value)
private float opp; // the opposite verticy in the trigonometric equation
private float adj; // the adjacent verticy
private float AccuracyBound; // the bounds for which the bullet is inaccurate to the direction the player faces
public GameObject bulletClass; // reference to the bullet class, is publuc so it can be assgned in the inspector
public Transform firePoint; // the position/rotation of the firepoint
    void Start() // initialises variables, sets there starting value or assignes them components
    {
        coll = GetComponent<BoxCollider2D>();  
        health = 100;
        maxHealth = 100;
        AccuracyBound = 10f;
        maxAmmo = 100;
        damage = 20;
        fireRate = 5;
        speed = 4.0f;
        ammo = 100;
    }

    void FixedUpdate() // called independantly of frame rate
    {
        movement();
        mousePos = Input.mousePosition; // input.mousePosition gets the mouse position as a vector 3 
        transform.eulerAngles = new Vector3 (0,0, getXRotation(mousePos)); // euler angles are a stored as vector 3, calls getXRotation for the z rotation

        if (Input.GetButton("Fire1")){ // fire1 is the left mouse click
            if (ammo > 0){ // as long as there is ammo
                if (Time.time - lastFired > 1/ fireRate){ // must wait 1/firerate before allowing to fire again
                    lastFired = Time.time; // reset last fired
                    shoot(); // call shoot method 
                    ammo -= 1; } } } // decrease ammo
                      
        

    }
public int takeDamage(int d, int h) // method for reducuing damage
{
    mainGameController.score -= 10; // references the maingamecontroller to reduce score
    if (d >= h){
        
        return (0); } // if damage is bigger than health then set health to 0 as player is eliminated
    else{
        healthBarUI.setHealth(h-d);
        return h - d; } // otherwise return health minus damage
} 
private void movement(){
    float x = Input.GetAxis("Horizontal");// returns 1 for right, -1 for left and 0 for others
        float y = Input.GetAxis("Vertical");// same as before but for y value (vertical axis)

        move = new Vector3(x, y, 0); // a representaation of where the user wants the player to move
        
        block = Physics2D.BoxCast(transform.position, coll.size, 0, new Vector2(move.x, 0), Mathf.Abs(move.x * Time.deltaTime * speed ), LayerMask.GetMask("Walls")); // assigns the block variable to be a box cast
        if (block.collider == null)  // the box is cast in the direction and size of the next movement, if anything is returned as a collider, do not let player move
        {
            transform.Translate((move.x * Time.deltaTime)*speed, 0 ,0, Space.World);//moves by move.x*delta time which is the time taken since the last frame. this makes it move in relation to real time not by frames per second
        }  
        
        block = Physics2D.BoxCast(transform.position, coll.size, 0, new Vector2(0, move.y), Mathf.Abs(move.y * Time.deltaTime * speed ), LayerMask.GetMask("Walls"));
        if (block.collider == null) 
        {
            transform.Translate(0 , (move.y * Time.deltaTime)*speed ,0, Space.World);//moves by move.x*delta time which is the time taken since the last frame. this makes it move in relation to real time not by frames per second
        }
}
private float getXRotation(Vector3 mousePos){
    opp = mousePos.x - 400; // player position is always at 400, 300 as it is fied in the centre of the screen
    adj = mousePos.y - 300; // these verticies make up a right angle triangle with the player and mouse
        
    if (mousePos.y > 300) // if mouse is in top half of screen
    {
        xRotation = (Mathf.PI/2 - Mathf.Atan(opp / adj) ); //atan is arctan, the inverse function of tan
    }
    else if (mousePos.y < 300) // if mouse is in bottom half of screen
    {
        xRotation = (Mathf.PI*(3/2) - Mathf.Atan(opp / adj) + Mathf.PI/2);
    }
            
            
    return (xRotation * Mathf.Rad2Deg); // convert output angle from radians to degrees, as that is the format of the euler angles
}

private void shoot(){     
    firePoint.BroadcastMessage("setAccuracy", AccuracyBound); // tells firepoint to call setAccuracy method, randomizing its rotation within accuracyBounds
    GameObject bullet = Instantiate(bulletClass, firePoint.position, firePoint.rotation ); // creates an instance of bullet and assignes it to bullet
    Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>(); // assignes the rigid body component of bullet to variable bulletRB
    bulletRB.AddForce(firePoint.up * 25, ForceMode2D.Impulse); // adds a force to bullet using rigid body component 
    ammoTextUI.setAmmo(ammo, maxAmmo); // adjust the ammo ui accordingly
    firePoint.BroadcastMessage("setRotation"); // resets the rotation of firepoint
    }

void OnCollisionEnter2D(Collision2D col){ // method called on a collision 
    if (col.gameObject.name == "enemy(Clone)"){ // if collision is with an enemy clone
        health = takeDamage(20, health); // reduce health
        if (health <= 0){  // if dead
            justDied = true; // used so menu scene knows to display the end screen
            finalScore = mainGameController.score; // sets final score so it cannot be altered after player dies
            SceneManager.LoadScene("Menu"); // loads menu scene
        }
        }
}
private void OnTriggerEnter2D(Collider2D col){ // when player enters collision with a trigger
    if (col.name == "speedBuy"){  // identifies which collider it is 
        if (mainGameController.score >= 200){  // checks if score is high enough
            mainGameController.score -=200; // reduces points
            speed = speed + 2; // increases speed
        }

    }
    if (col.name == "fireRateBuy"){
        if (mainGameController.score >= 100){  // each of these if statements are for different colliders but have the same basic function 
            mainGameController.score -= 100;
            fireRate = fireRate + 4;
        }

    }
    if (col.name == "accuracyBuy"){
        if (mainGameController.score >= 500){
            mainGameController.score -=500;
            AccuracyBound = AccuracyBound /1.5f;
        }

    }
    if (col.name == "maxAmmoBuy"){
        if (mainGameController.score >= 100){
            mainGameController.score -=100;
            maxAmmo += 20;
            ammoTextUI.setAmmo(ammo, maxAmmo);
        }

    }

    if (col.name == "healthBuy"){
        if (mainGameController.score >= 1000){
            mainGameController.score -=1000;
            maxHealth += 50;
            healthBarUI.setMaxHealth(maxHealth);
        }

    }
    if (col.name == "damageBuy"){
        if (mainGameController.score >= 1000){
            mainGameController.score -=1000;
            damage += 20;
        }

    }
    if (col.name == "healthPickup(Clone)"){
        col.BroadcastMessage("hit"); // cant call function as normal as the obect being referenced is abstract
        if (    !(health >= maxHealth-20) ){
            health += 20;   }
        else{
            health = maxHealth;   }
        healthBarUI.setHealth(health);
    }   

    if (col.name == "ammoPickup(Clone)"){
        col.BroadcastMessage("hit");
        if (    !(ammo >= maxAmmo-40) ){
            ammo += 40;   }
        else{
            ammo = maxAmmo;   }
        ammoTextUI.setAmmo(ammo, maxAmmo);
    } 

    
}

}
    

