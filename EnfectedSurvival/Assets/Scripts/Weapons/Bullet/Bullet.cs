using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //PREFABS
    public GameObject particleWallPrefab;
    public GameObject particleBloodPrefab;

    //DATABASE
    [SerializeField] WeaponDatabase weaponDatabase;

    //VARIABLES
    public int damage;

    //GAMEOBJECTS
    GameObject[] mine;

    void Start()
    {

        DetectWeaponType();
        Destroy(this.gameObject, 2f); //Destroy after 2s
    }

    public void OnCollisionEnter2D(Collision2D Col)
    {
        if(Col.gameObject.tag == "Wall") // Trigger with wall
        {
            Particle(particleWallPrefab);
        }
        if(Col.gameObject.tag == "Zombie") // Trigger with zombie
        {
            GameObject particle = Particle(particleBloodPrefab);
            if (Col.gameObject.name == "Zombie3(Clone)")
            {
                particle.GetComponent<Transform>().localScale *= 2;
            }
        }
        if(Col.gameObject.tag == "Mine")
        {
            Destroy(gameObject);
        }
    }

    GameObject Particle(GameObject prefab)
    {
        GameObject particle = Instantiate(prefab, transform.position, Quaternion.identity); //Create particle
        Destroy(this.gameObject); //Destroy object 
        return particle;
    }


    void DetectWeaponType()
    {
        //detect weapon and change damage
        Weapon weapon = weaponDatabase.GetWeapon(PlayerPrefs.GetInt("selectedWeapon"));
        damage = weapon.bulletDamage;
    }
}
