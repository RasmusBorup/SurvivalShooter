using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    Animator anim;
    GameObject focusedPlayer;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;


    void Awake ()
	{
		focusedPlayer = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = focusedPlayer.GetComponent <PlayerHealth> ();
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
        while(playerInRange && other.gameObject == focusedPlayer && focusedPlayer.GetComponent<PlayerHealth>().currentHealth > 0)
        {
            yield return playerInRange = true;
        }
        playerInRange = false;
    }

    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == focusedPlayer)
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
		playerHealth = focusedPlayer.GetComponent <PlayerHealth> ();
		timer = 0f;

        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage);
        }
    }
}
