using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject[] zombies;
    public GameObject bossZombie;
    private int waveCount = 1;
    private UI ui;
    public List<GameObject> zombieList;

    public void Start()
    {
        ui = GameObject.Find("UIManager").GetComponent<UI>();
        zombieList = new List<GameObject>();
    }
    public void Update()
    {
        if (zombieList.Count == 0)
        {
            StartNextWave(waveCount);
        }
    }

    public void StartNextWave(int currentWave)
    {
        //set ui;
        ui.NextWave(currentWave);

        if (waveCount % 5 != 0) for (int i = 0; i < currentWave * 3; i++) SpawnZombie();
        else SpawnBoss();

        waveCount++;//increase wave count
    }
    public void SpawnZombie()
    {
        Vector3 randomSpawnPos = RandomPosGenerator(1f);

        int randomIndex = Random.Range(0, zombies.Length);

        GameObject zombie = Instantiate(zombies[randomIndex], randomSpawnPos, Quaternion.identity);

        zombieList.Add(zombie);//add zombie to the list
    }
    public void SpawnBoss()
    {
        Vector3 randomSpawnPos = RandomPosGenerator(.8f);
        GameObject zombie = Instantiate(bossZombie, randomSpawnPos, Quaternion.identity);
        zombieList.Add(zombie);//add zombie to the list


    }
    public void DeleteZombieFromList(GameObject zombie) => zombieList.Remove(zombie);
    public Vector3 RandomPosGenerator(float radius)
    {
        float ang = Random.value * 360;
        Vector2 pos = new Vector2(radius * Mathf.Sin(ang * Mathf.Deg2Rad), radius * Mathf.Cos(ang * Mathf.Deg2Rad));
        Vector3 spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(pos.x + 0.5f, pos.y + 0.5f, 10.0f));
        return spawnPos;
    }
}
