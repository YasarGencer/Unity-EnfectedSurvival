using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSummonning : MonoBehaviour
{
    private GameObject[] zombies;
    private GameObject player;
    public List<GameObject> zombieList;
    private WaveManager waveManager;
    private Animator animator;


    public float timeBetween = 1f;

    private float timeToSpawn;

    public void Start()
    {
        waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        animator = this.GetComponent<Animator>();
        player = GameObject.Find("Karakter");
        zombies = waveManager.zombies;
        zombieList = new List<GameObject>();

        timeToSpawn += Time.time + 5;
    }

    public void Update()
    {

        if (Time.time >= timeToSpawn)
        {
            timeToSpawn = Time.time + timeBetween;
            SummonZombies();
        }
    }

    public void SummonZombies()
    {
        for (int i = 0; i < 5; i++) SpawnZombie();

        //summoning anim
        animator.SetTrigger("Summon");   
    }

    public void SpawnZombie()
    {
        Vector3 randomSpawnPos = RandomPosGenerator(10f);
        int randomIndex = Random.Range(0, zombies.Length);
        GameObject zombie = Instantiate(zombies[randomIndex], randomSpawnPos, Quaternion.identity);
        zombieList.Add(zombie);//add zombie to the list
    }
    public Vector3 RandomPosGenerator(float radius)//selecting random point around player
    {
        float ang = Random.value * 360;
        Vector2 pos = new Vector2(radius * Mathf.Sin(ang * Mathf.Deg2Rad), radius * Mathf.Cos(ang * Mathf.Deg2Rad));
        Vector3 spawnPos = player.transform.position + new Vector3(pos.x, pos.y + 0);
        return spawnPos;
    }
    
}
