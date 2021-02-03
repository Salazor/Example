using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    public int speed;
    Animator anim;
    public Transform attackPos;
public LayerMask isPlayer;
public float attackRange;
    GameObject player;
        bool run = false;
    private bool facingRight = true;
    public int range = 2;
    int dis = 10;
    private Vector2 enemyPos;
    private Vector2 startPos;
    public float health;
    public GameObject effect;
    
    public GameObject SoulDrop;
    public Transform instSoul;
    GameObject effectCl;


    bool attack = true;
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    private int damageTP;





    // Start is called before the first frame update
   private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
        startPos = transform.position;
        

    }

    // Update is called once per frame
    private void Update()
    {
        
        if(health <= 0)
        {
            
            Instantiate(SoulDrop, instSoul.position, instSoul.rotation);
            Destroy(this.gameObject);
        }
    }
    private void FixedUpdate()
    {
      
     if(player != null) 
     {  
    if(Vector2.Distance(transform.position,player.transform.position) <= dis && Vector2.Distance(transform.position,player.transform.position) > range && run == false)
    {
        anim.SetBool("walk", true);
       enemyPos = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.fixedDeltaTime);
       if (facingRight == true && transform.position.x > player.transform.position.x)
       {
           Flip();
       }
      
       else if (facingRight == false && transform.position.x < player.transform.position.x)
       {
           Flip();
        
       }
       
    
    }    
    else if (Vector2.Distance(transform.position,player.transform.position) <= dis && Vector2.Distance(transform.position,player.transform.position) > range && run == true)
    {
    anim.SetBool("walk", true);
       enemyPos = Vector2.MoveTowards(transform.position, player.transform.position, -speed * Time.fixedDeltaTime);
       if (facingRight == true && transform.position.x > player.transform.position.x)
       {
           Flip();

       }
       
       else if (facingRight == false && transform.position.x < player.transform.position.x)
       {
            Flip();

       }
    } 
     else if(Vector2.Distance(transform.position,player.transform.position) <= range)
     OnEnemyAttack();

     else if(Vector2.Distance(transform.position,player.transform.position) > dis)
     {
         anim.SetBool("walk", true);
       enemyPos = Vector2.MoveTowards(transform.position, startPos, speed * Time.fixedDeltaTime);
       if (facingRight == true && transform.position.x > startPos.x)
       {
           Flip();

       }
       
       else if (facingRight == false && transform.position.x < startPos.x)
       {
            Flip();

       }
     }
    transform.position = new Vector2(enemyPos.x, transform.position.y);
    if (Vector2.Distance(transform.position,player.transform.position) > dis && transform.position.x == startPos.x)
    anim.SetBool("walk", false);
     }
else
anim.SetBool("walk", false);
    }
    
    
    
    public void Damage(int damage)
    {
      Instantiate(effect, transform.position, Quaternion.identity);
        health -= damage;
    }
  
  
  
  
  void Flip()
  {
facingRight = !facingRight;
Vector3 Scaler = transform.localScale;
Scaler.x *= -1;
transform.localScale = Scaler;
  }
  


  public void OnEnemyAttack()
    {
        if(attack == true)
      {
          
             attack = false;
              anim.SetTrigger("attack");

          
               
               
        Instantiate(effect, player.transform.position, Quaternion.identity);
        Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, isPlayer);
              for (int i = 0; i < playerToDamage.Length; i++)
              {
                  playerToDamage[i].GetComponent<PlayerController>().Damage(1);
              }
        Invoke("AttackReset", 1);
      }
               
    }
void AttackReset()
{
    attack = true;
    effectCl = GameObject.Find("PretaDamageEffect(Clone)");
     Destroy(effectCl);  
}

    private void OnDrawGizmosSelected()
    {
Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
