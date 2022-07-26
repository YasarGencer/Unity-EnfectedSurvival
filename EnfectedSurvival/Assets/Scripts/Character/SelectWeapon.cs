using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWeapon : MonoBehaviour
{
    //GAMEOBJECTS
    public GameObject weaponPanel;

    //SCRIPTS
    public Shoot shoot;

    void Update()
    {
        //show weapon select bar when pressing "TAB"
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            weaponPanel.SetActive(true); //showw weapon select menu 
            Time.timeScale = 0.4f; //slow mo
            BlockShoot();
        }
        if(Input.GetKeyUp(KeyCode.Tab))
        {
            weaponPanel.SetActive(false); //hide weapon select menu
            Time.timeScale = 1f; //normal mo
            AllowShoot();
        }
    }

    void BlockShoot()
    {
        //control all weapons
        shoot.ableToShoot = false;
        shoot.ableToMine = false;
    }

    void AllowShoot()
    {
        //control all weapons
        shoot.ableToShoot = true;
        shoot.ableToMine = true;
    }
}
