using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;


    void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent<EnemyHealth>();
		anim = GetComponent <Animator> ();
    }

	void Start()
	{
	}

    //Should be able to make this less messy
    IEnumerator OnTriggerEnter (Collider other)
    {
        playerInRange = true;
        while(playerInRange && other.gameObject == player && playerHealth.currentHealth > 0) {
            yield return playerInRange = true;
        }
        playerInRange = false;
    }

    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
			Attack();
        }

        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger ("PlayerDead");
			playerInRange = false;
        }
    }

    void Attack ()
    {
		timer = 0f;

        if(playerHealth.currentHealth > 0) {
            playerHealth.TakeDamage (attackDamage);
        }
    }
}
