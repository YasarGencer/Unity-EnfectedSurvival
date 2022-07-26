using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDatabase", menuName = "weapons/weapon select database")]
public class WeaponDatabase : ScriptableObject
{
    public Weapon[] weapons;
    public int weaponCount { get { return weapons.Length; } }

    public Weapon GetWeapon(int index)
    {
        return weapons[index];
    }
}