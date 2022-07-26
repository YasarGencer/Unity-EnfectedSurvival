using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public WeaponDatabase weaponDatabase, holderDatabase;

    public void Awake()
    {
        for (int i = 0; i < weaponDatabase.weapons.Length; i++)
        {
            Databases(i);
        }
    }
    public void Databases(int i)
    {
        weaponDatabase.GetWeapon(i).weaponName = holderDatabase.GetWeapon(i).weaponName;
        weaponDatabase.GetWeapon(i).magCapasite = holderDatabase.GetWeapon(i).magCapasite;
        weaponDatabase.GetWeapon(i).currentAmmo = holderDatabase.GetWeapon(i).currentAmmo;
        weaponDatabase.GetWeapon(i).totalAmmo = holderDatabase.GetWeapon(i).totalAmmo;
        weaponDatabase.GetWeapon(i).bulletDamage = holderDatabase.GetWeapon(i).bulletDamage;
        weaponDatabase.GetWeapon(i).inGameSprite = holderDatabase.GetWeapon(i).inGameSprite;
        weaponDatabase.GetWeapon(i).imageUI = holderDatabase.GetWeapon(i).imageUI;
        weaponDatabase.GetWeapon(i).bulletSize = holderDatabase.GetWeapon(i).bulletSize;
        weaponDatabase.GetWeapon(i).bulletSpeed = holderDatabase.GetWeapon(i).bulletSpeed;
        weaponDatabase.GetWeapon(i).bulletPrefab = holderDatabase.GetWeapon(i).bulletPrefab;
        weaponDatabase.GetWeapon(i).shakeAmount = holderDatabase.GetWeapon(i).shakeAmount;
    }
}
