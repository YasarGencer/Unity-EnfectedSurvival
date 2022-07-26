using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Weapon
{
    [Header("Name")]
    public string weaponName;
    [Header("Ammo Counts")]
    public int magCapasite;
    public int currentAmmo, totalAmmo;
    [Header("Graphics")]
    public Sprite inGameSprite;
    public Sprite imageUI;
    [Header("Bullet")]
    public Vector3 bulletSize;
    public int bulletDamage, bulletSpeed;
    public GameObject bulletPrefab;
    [Header("Camera")]
    public float shakeAmount;
}