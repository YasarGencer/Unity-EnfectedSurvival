using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineObject : MonoBehaviour
{
    //COLLIDERS
    CircleCollider2D cc; 

    //VARIABLES
    public int minePower;

    //SSCRIPTS
    UI ui;

    //GAMEOBJECTS
    GameObject Player;

    //DATABASES
    public WeaponDatabase weaponDatabase;
    Weapon mine;

    //EFFECTS
    public GameObject blowUpEffect;

   
   
    void Start()
    {
        mine = weaponDatabase.GetWeapon(1);

        //unprefab objects
        ui = GameObject.FindWithTag("UIManager").GetComponent<UI>(); //get ui script

        //object options at start
        cc = gameObject.AddComponent<CircleCollider2D>(); //create circle collider
        cc.isTrigger = false; //set trigger of created collider
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 1); //set z position to 1 
        cc.radius = 0.1f; //set start radius
    }

    IEnumerator BlowUp()
    {
        if(cc.radius < minePower)
        {
            Instantiate(blowUpEffect, transform.position, Quaternion.identity);//create blow up effect
            cc.radius += 1; //add radius
            yield return new WaitForSeconds(0.01f); //timer
            StartCoroutine(BlowUp()); //return
            Destroy(gameObject); //destroy  
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Zombie")
        {
            StartCoroutine(BlowUp()); //Blow up when zombie touch
        }
        if(col.gameObject.tag == "Bullet")
        {
            StartCoroutine(BlowUp());
        }
        if(col.gameObject.tag == "Player" && Input.GetButton("Fire2")) //press "Fire2" for collect mines
        {
            
            mine.currentAmmo++; //add mine count
            ui.MineAmount(mine.currentAmmo); //write mine count to ui
            Destroy(gameObject); //destroy
        }
    }


}
