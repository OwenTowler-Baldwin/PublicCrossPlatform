using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopDownCombat : MonoBehaviour
{
    EnemyCP EnemyCP;


    public Animator animator;



    public Transform attackPoint; // The point from which the wepons range is calculated
    [Header("Main Attack")]
    public float mainAttackRange = 0.5f;// the range the wepon can attack up to
    public int mainAttackDamage = 1;// the players damage

    [Header("HUD Elements")]
    public HolyMeter holyMeter;
    public HealthBar healthBar;

    [Header("Menu Elements")]
    public GameObject pauseMenu;
    public GameObject optionsMenu;

    [Header("Enemy Layers")]
    public LayerMask minionLayers;// defines what an enemy is

    [Header("Ammo")]
    public int amoCountMax = 5; //players amo count 
    int amoCount = 0;//keeps track of the players current ammo count
    [Header("Health")]
    public int maxHealth = 15;//max health the player can have 
    [SerializeField] int currentHealth = 1;//the players current health


    private AudioSource audSrc;
    public AudioClip[] attackSounds;
    public AudioClip[] emptySounds;


    private void Start()
    {

        amoCount = amoCountMax;

        holyMeter.SetMaxWater(amoCountMax);

        currentHealth = maxHealth;

        healthBar.SetMaxHealth(maxHealth);

        audSrc = GetComponent<AudioSource>();
        int halfHealth = currentHealth / 2;
    }

    // Update is called once per frame
    void Update()
   {/* 
        if (pauseMenu.active == false)
        {
            if (optionsMenu.active == false)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))//triggers when left mouse click is clicked
                {
                    if (amoCount >= 0)
                    {
                        MainAttack();
                    }
                    else
                    {
                        //play empty ammo sound
                        audSrc.PlayOneShot(emptySounds[Random.Range(0, emptySounds.Length)]);
                        Debug.Log("Out of ammo");  //will play a ui element telling the player to reload
                    }
                }
            }
        }*/
    }

   public void MainAttack()// the mainAttack function 
   {
        if(amoCount <= 0)
            return;
        //play the attack animation, to be fully implemented once animator is ready
        animator.SetTrigger("MainAttack");

        // use up ammo
        amoCount--;
        holyMeter.SetWater(amoCount);//calling UI scripts
        // play attack sound
      //  audSrc.PlayOneShot(attackSounds[Random.Range(0, attackSounds.Length)]);


        //detect enemies in range
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, mainAttackRange, minionLayers);

        //damage them
        foreach (Collider enemy in hitEnemies)
        {
            //damage the enemies
            Debug.Log("Hit" + enemy.name);

            enemy.GetComponent<EnemyCP>().EnemyTakeDamage(mainAttackDamage);//calls the enemy script and allows damage to be done   
        }
    }

    public void Reload()
    {

        Debug.Log("Reloaded");//logs a reload
        amoCount = amoCountMax;//sets current amo = to max amo

        animator.SetTrigger("Reload");

        holyMeter.SetWater(amoCount);
    }

    public void PlayerTakeDamage(int Damage)
    {

        currentHealth -= Damage;// current health - damage of enemy

        //play the damaged animation if there is one
        animator.SetTrigger("TakeDamage");

        healthBar.SetHealth(currentHealth);

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

        Cursor.lockState = CursorLockMode.None;//unlocks the cursor to the center of the screen
        Cursor.visible = true;//make the mouse visable 

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + -1);

    }


    private void OnDrawGizmosSelected()//draws the main attacks range
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, mainAttackRange);
    }

   
}
