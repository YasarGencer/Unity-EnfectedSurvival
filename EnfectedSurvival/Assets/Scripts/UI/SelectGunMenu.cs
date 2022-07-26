using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGunMenu : MonoBehaviour
{

    //public GameObject[] weapons; //weapons array
    private UI ui;
    public Shoot shoot;
    
    [SerializeField] WeaponDatabase weaponDatabase;

    public void Start()
    {
        ui = GameObject.Find("UIManager").GetComponent<UI>();
        ui.UpdateUI(weaponDatabase);
    }
    public void SelectWeapon(int weaponId)
    {   
        PlayerPrefs.SetInt("selectedWeapon", weaponId);
        shoot.GetWeaponID();
        ui.UpdateUI(weaponDatabase);
    }

    


}
