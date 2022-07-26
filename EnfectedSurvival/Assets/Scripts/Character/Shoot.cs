using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    //TRANSFORMS
    public Transform firePoint;
    public Transform leavePoint;
    public Transform cameraPoint;

    //GAMEOBJECTS
    [Header("GAMEOBJECTS-PREFABS")]
    public GameObject effectPrefab, magPrefab;

    //SCRIPTS
    [Header("SCRIPTS")]
    private UI ui;
    //DATABASE
    [Header("DATABASES")]
    [SerializeField] WeaponDatabase weaponDatabase;

    //ANIMATOR
    Animator animator;

    //VARIABLES
    [Header("VARIABLES")]
    int weaponId; //weapon id
    Weapon weapon;
    private int shootCount = 0;

    //BOOLS
    public bool ableToShoot = true;
    private bool ableToReaload = true;
    public bool ableToMine = true;
    private bool autoOn;


    public void Start()
    {
        ui = GameObject.Find("UIManager").GetComponent<UI>();
        animator = this.gameObject.GetComponent<Animator>(); //animator component

        GetWeaponID();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) autoOn = true; //auto
        else if (Input.GetButtonUp("Fire1")) autoOn = false;

        if (Input.GetButtonDown("Fire1") && ableToShoot)
        {
            if (weapon.weaponName != ("Mine")) FireMethod(); //gun
            else if (weapon.weaponName == ("Mine")) MineMethod(); //explosive
        }
        
        if (Input.GetKeyDown("r")) Reload();
    }

    void FireMethod()
    {
        if (weapon.currentAmmo > 0)
        {
            shootCount = 0;//restarts count for auto and rifle
            //Fire Options
            StartCoroutine(Fire());
            Effect();
            //UI güncelle
            ui.BulletAmount(weapon.currentAmmo, weapon.magCapasite, weapon.totalAmmo);
            ui.MineAmount(weapon.currentAmmo);
            //Cam Effect
        }
        else
            Reload();
    }
    IEnumerator Fire()
    {
        CreateBullet();
        CameraShootShake();
        yield return new WaitForSeconds(0.1f);

        if (weaponId == 2 && autoOn)//auto 
            FireMethod();

        if (weaponId == 3 && shootCount < 2) //triple shoot with rifle
        {
            shootCount++;
            ableToShoot = false;
            StartCoroutine(Fire());
        }
        else
        {
            shootCount = 0;
            ableToShoot = true;
        }
            
    }
    void MineMethod()
    {
        if (Input.GetButtonDown("Fire1") && weapon.currentAmmo > 0 && ableToMine) // press "T" for leave a mine
        {
            GameObject minePrefab = weapon.bulletPrefab;
            GameObject mine = Instantiate(minePrefab, leavePoint.position, leavePoint.rotation); //leave mine

            weapon.currentAmmo--; //decrease mine counts
            ui.MineAmount(weapon.currentAmmo); //send mine count to UI Script
        }
    }
    void Effect()
    {
        GameObject effect = Instantiate(effectPrefab, firePoint.position, firePoint.rotation); //Create effect
        effect.GetComponent<Transform>().localScale = new Vector3(.5f, .5f, .5f); //Shrink the effect
    }
    void Reload()
    {
        if (ableToReaload && weapon.totalAmmo > 0)
        {
            ableToShoot = false;
            ableToReaload = false;
            //Reload
            StartCoroutine(ReloadC());
            //Reload Anim
            animator.SetTrigger("Reload");
            //Drop Mag
            if (weapon.weaponName != "Crossbow")
            {
                Vector3 magPos = this.transform.position + new Vector3(Random.Range(-.5f, +.5f), Random.Range(-.5f, +.5f), Random.Range(-.5f, +.5f));
                GameObject mag = Instantiate(magPrefab, magPos, firePoint.rotation);
                Destroy(mag, 10f);
            } 
        }
    }
    IEnumerator ReloadC()
    {
        ableToShoot = false;
        //check ammo count from database
        if (weapon.currentAmmo < weapon.magCapasite && weapon.totalAmmo > 0)
        {
            weapon.currentAmmo++; //add bullet
            weapon.totalAmmo--; //Decrease total bullet amount
            ui.BulletAmount(weapon.currentAmmo,weapon.magCapasite, weapon.totalAmmo);
            if (weapon.weaponName == "Auto")
                yield return new WaitForSeconds(0.01f);
            else if (weapon.weaponName == "Rifle")
                yield return new WaitForSeconds(0.05f);
            else
                yield return new WaitForSeconds(0.1f);
            StartCoroutine(ReloadC()); //re-read
        }
        else
            ableToShoot = true;
    }
    public void GetWeaponID()
    {
        weaponId = PlayerPrefs.GetInt("selectedWeapon", 0); //get current weapon id 
        weapon = weaponDatabase.GetWeapon(weaponId);
    }
    public void CreateBullet()
    {
        GameObject bulletPrefab = weapon.bulletPrefab;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); //Create bullet
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); //Get bullets Rb

        bullet.transform.localScale = weapon.bulletSize;//adjust size
        int bulletSpeed = weapon.bulletSpeed; //Give speed
        rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
        weapon.currentAmmo--; //Decrease bullet 
        ableToReaload = true;
    }
    public void CameraShootShake()
    {
        Animator animator = GameObject.FindWithTag("MainCamera").GetComponent<Animator>();
        animator.SetTrigger("shootShake");
    }
}
