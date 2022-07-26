using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie1Control : MonoBehaviour
{
    //RIGIDBODY 
    Rigidbody2D ridigbody2D;

    //TRANSFORMS
    Transform player;
    Transform zombie;

    //VARIABLES
    public float speed;
    public int range;
    int select;
    public float distance;
    public int zombieDamage;
    public int reboundPlayer;
    public int reboundZombie;

    //AN�MATOR 
    private Animator animator;

    //SCR�PTS
    public Health health;

    //GAMEOBJECTS

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>(); //player's transform  
        ridigbody2D = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        //StartCoroutine(Point());
    }

    
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < range && Vector2.Distance(transform.position, player.position) > distance && player) //follow range
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed*Time.deltaTime); //zombie player movememnt
            
            //look to player
            Vector3 dir = player.position - transform.position; 
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; 
            transform.rotation = Quaternion.AngleAxis(angle -90, Vector3.forward);

            //play animation
            animator.SetBool("Walking", true);
        }
        else
        {
            Vector3 randomPos = new Vector3(Random.Range(0,10), Random.Range(0, 10));
            transform.position = Vector2.MoveTowards(transform.position, randomPos, speed * Time.deltaTime); //zombie random movememnt

            //look to player
            Vector3 dir = randomPos - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);   
            if(gameObject.transform.position == randomPos)
            {
                //start animation
                animator.SetBool("Walking", false);
            }
            else
            {
                //stop animation
                animator.SetBool("Walking", true);
            } 
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Bullet")
        {
            //range = range * 2; //increase range

            health.DecreaseHealth(col.gameObject.GetComponent<Bullet>().damage); //get bullet damage and decrease health

            //hit anim
            animator.SetTrigger("GetHit");
        }else if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * reboundPlayer); //player rebound
            col.gameObject.GetComponent<Health>().DecreaseHealth(zombieDamage); //decrease health of player
        }else if(col.gameObject.tag == "Mine")
        {
            ridigbody2D.AddForce(-transform.position * reboundZombie); //zombie rebound
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            Invoke("Ac", 0.5f);
        }
    }
    /*
    IEnumerator Point()
    {
        select = Random.Range(0, randomPoints.Length);
        yield return new WaitForSeconds(Random.Range(5f, 10f));
        StartCoroutine(Point()); //re-read
       
    }
    */
    void Ac(){
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
