using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public WeaponDatabase weaponDatabase;
    public CollectableDatabase collectableDatabase;
    private UI ui;
    public GameObject collectableTexts;

    Weapon weapon;
    Health health;
    GameObject collectable;
    private void Start()
    {
        ui = GameObject.Find("UIManager").GetComponent<UI>();
        health = this.GetComponent<Health>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "CollectableAmmo")
        {
            if (collision.gameObject.name == "PistolAmmo(Clone)")
            {
                weapon = weaponDatabase.GetWeapon(0);
                ui.WriteCollectables(collectableTexts, "+" + weapon.magCapasite + " pistol ammo");
            }
            else if (collision.gameObject.name == "AutoAmmo(Clone)")
            {
                weapon = weaponDatabase.GetWeapon(2);
                ui.WriteCollectables(collectableTexts, "+" + weapon.magCapasite + " auto ammo");
            }
            else if (collision.gameObject.name == "RifleAmmo(Clone)")
            {
                weapon = weaponDatabase.GetWeapon(3);
                ui.WriteCollectables(collectableTexts, "+" + weapon.magCapasite + " rifle ammo");
            }
            else if (collision.gameObject.name == "CrossAmmo(Clone)")
            {
                weapon = weaponDatabase.GetWeapon(4);
                weapon.totalAmmo += weapon.magCapasite * 3;
                ui.WriteCollectables(collectableTexts, "+" + weapon.magCapasite * 4 + " arrows");
            }
                
            weapon.totalAmmo += weapon.magCapasite;
            Destroy(collision.gameObject);
            ui.UpdateUI(weaponDatabase);
        }
        if (collision.gameObject.tag == "CollectableHealth")
        {
            ui.WriteCollectables(collectableTexts, "+250 health");
            health.DecreaseHealth(-250);
            if (health.health > health.maxHealth) health.SetMaxlHealth();

            Destroy(collision.gameObject);
        }
    }
    public void CreateCollectables(Transform transform)
    {
        int luck = Random.Range(0, 10);
        if (luck <= 3)
        {
            int choosenCollectable = Random.Range(0, collectableDatabase.collectableCount);
            collectable = collectableDatabase.GetCollectable(choosenCollectable);
            collectable = Instantiate(collectable, transform.position, Quaternion.identity);
        }
    }
}
