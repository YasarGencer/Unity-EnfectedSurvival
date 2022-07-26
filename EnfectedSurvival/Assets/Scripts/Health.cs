using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int health, maxHealth;
    public int stamina, maxStamina;

    private WaveManager waveManager;
    private Collectables collectables;
    public UI ui;

    public void Start()
    {
        if (gameObject.name == "Zombie3(Clone)") ui = GameObject.Find("UIManager").GetComponent<UI>();

        SetMaxlHealth(); SetMaxStamina();

        waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        collectables = GameObject.Find("Karakter").GetComponent<Collectables>();
    }
    public void SetMaxlHealth()
    {
        health = maxHealth;
        if (ui)
        {
            if (gameObject.tag == "Player") ui.SetMaxHealth(maxHealth, ui.healthBar);
            else if (gameObject.tag == "Zombie") ui.SetMaxHealth(maxHealth, ui.zombieHealthBar);
        }
    }
    public void DecreaseHealth(int value)
    {
        if(health > value)//decrease health
        {
            health -= value;
            if (ui)
            {
                if (gameObject.tag == "Player") ui.SetHealth(health, ui.healthBar);
                else if (gameObject.tag == "Zombie") ui.SetHealth(health, ui.zombieHealthBar);
            }
        }
        else//Die
        {
            if (gameObject.tag == "Player") PlayerDeath();
            else if (gameObject.tag == "Zombie")
            {
                if (gameObject.name == "Zombie3(Clone)") this.GetComponent<ZombieDeath>().BossZombieDie(waveManager.zombieList);
                else this.GetComponent<ZombieDeath>().DefaultZombieDie(waveManager.zombieList, collectables);
            } 
        }
    }
    public void SetMaxStamina()
    {
        stamina = maxStamina;
        if (gameObject.tag == "Player") ui.SetMaxStamina(stamina);
    }
    public void DecreaseStamina(int value)
    {
        if (stamina >= 0)
        {
            stamina -= value;
            if (gameObject.tag == "Player") ui.SetStamina(stamina);
        }
    }
    public void PlayerDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
