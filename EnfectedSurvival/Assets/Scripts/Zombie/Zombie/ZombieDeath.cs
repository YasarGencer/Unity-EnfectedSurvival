using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDeath : MonoBehaviour
{
    public void DefaultZombieDie(List<GameObject> zombieList, Collectables collectables)
    {
        zombieList.Remove(this.gameObject);
        collectables.CreateCollectables(this.transform);
        Destroy(gameObject);
    }
    public void BossZombieDie(List<GameObject> zombieList)
    {
        DeleteAllZombies(this.GetComponent<ZombieSummonning>().zombieList);//deletes minions from wavemanagers list

        UI ui = GameObject.Find("UIManager").GetComponent<UI>();
        ui.zombieHealthBar.gameObject.SetActive(false); // close health bar   
    }
    public void DeleteAllZombies(List<GameObject> zombieList) => StartCoroutine(DeleteZombies(zombieList));
    IEnumerator DeleteZombies(List<GameObject> zombieList)
    {
        Destroy(this.gameObject.GetComponent<SpriteRenderer>());//destroys the spirte of boss
        if (zombieList.Count > 0)//destroys minion one by one
        {
            GameObject zombie = zombieList[0];
            zombieList.Remove(zombie);
            Destroy(zombie);
            yield return new WaitForSeconds(0.05f);
            StartCoroutine(DeleteZombies(zombieList));
        }
        else 
        {
            WaveManager waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
            waveManager.zombieList.Remove(this.gameObject);//deletes this from wavemanagers list
            Destroy(this.gameObject);//destroys the boss
        } 
    }
}
