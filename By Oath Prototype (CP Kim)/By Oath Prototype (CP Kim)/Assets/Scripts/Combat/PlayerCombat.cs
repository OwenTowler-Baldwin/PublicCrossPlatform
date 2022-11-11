using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    Enemy myEnemy;
    BossBasic bossBasic;

    //  public Animator animator;

    public Transform attackPoint; //The point from which the wepons range is calculated
    public float mainAttackRange = 0.5f;// the range the weapon can attack up to
    public float seccondAttackRange = 1.75f;//seccondary attacks range
    public LayerMask enemyLayers;// defines what an enemy is
    public LayerMask bossLayer;//defines the boss

    public int mainAttackDamage = 1;// the players damage
    public int seccondAttackDamage = 10;//seccondarys attack damage
    public int amoCountMax = 5; //players amo count 
    int amoCount = 0;//keeps track of the players current ammo count
    public int maxHealth = 15;//max health the player can have 
    int currentHealth = 1;//the players current health

    private void Start()
    {
        amoCount = amoCountMax;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))//triggers when left mouse click is clicked
        {
            if (amoCount >= 0)
            {
                MainAttack();
            }
            else
                Debug.Log("Out of amo");  //will play a ui element telling the player to reload
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))//secondary attack
        {
            if (amoCount == amoCountMax)//only attacks if player has max ammo
            {
                SecondAttack();
            }
            else
                Debug.Log("Not enough ammo to powwer attack");//will play a ui element telling the player they dont have enough ammo

        }
    }

    void MainAttack()// the mainAttack function 
    {
        //play the attack animation, to be fully implemented once animator is ready
        // animator.SetTrigger("Attack"); // name of the trigger will go in the brakets


        //detect enemies in range

        amoCount--;

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, mainAttackRange, enemyLayers);


        //damage them
        foreach (Collider enemy in hitEnemies)
        {
            //damage the enemies
            Debug.Log("Hit" + enemy.name);

            enemy.GetComponent<Enemy>().EnemyTakeDamage(mainAttackDamage);//calls the enemy script and allows damage to be done   
        }


        Collider[] hitBoss = Physics.OverlapSphere(attackPoint.position,mainAttackRange, bossLayer);//detects any hit bosses

        foreach ( Collider boss in hitBoss)//loops over hit bosses
        {
            Debug.Log("Hit" + boss.name);
            boss.GetComponent<BossBasic>().BossTakeDamage(mainAttackDamage);//damages the boss
        }

    }

    void SecondAttack()//seccondary attack, right mouse click
    {
        //play the attack animation, to be fully implemented once animator is ready
        // animator.SetTrigger("Attack"); // name of the trigger will go in the brakets


        //detect enemies in range

        amoCount = 0;

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, seccondAttackRange, enemyLayers);


        //damage them
        foreach (Collider enemy in hitEnemies)//loops over hit enemys
        {
            //damage the enemies
            Debug.Log("Hit" + enemy.name);

            enemy.GetComponent<Enemy>().EnemyTakeDamage(seccondAttackDamage);//calls the enemy script and allows damage to be done 
        }

        Collider[] hitBoss = Physics.OverlapSphere(attackPoint.position, seccondAttackRange, bossLayer);//detects any hit bosses


        //damage them
        foreach (Collider boss in hitBoss)//loops over hit bosses
        {
            //damage the enemies
            Debug.Log("Hit" + boss.name);
            boss.GetComponent<BossBasic>().BossTakeDamage(seccondAttackDamage);//damages the boss
        }
    }

    public void Reload()
    {

        Debug.Log("Reloaded");//logs a reload
        amoCount = amoCountMax;//sets current amo = to max amo
    }

    public void PlayerTakeDamage(int Damage)
    {
        currentHealth -= Damage;// current health - damage of enemy

        //play the damaged animation if there is one

        if (currentHealth <= 0)//if health is less then or equal to 0 call die
        {
            PlayerDie();
        }
    }

    void PlayerDie()//die function 
    {
        Debug.Log("player is dead");
        //death animation??

        //Play death screen       
    }


    private void OnDrawGizmosSelected()//draws the main attacks range
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, mainAttackRange);
    }

    private void OnDrawGizmos()//draws the seccondary attacks range
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, seccondAttackRange);
    }
}
