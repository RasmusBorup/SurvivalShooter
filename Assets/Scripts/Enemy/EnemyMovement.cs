using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform playerTransform;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;


    void Awake ()
	{
		enemyHealth = GetComponent <EnemyHealth> ();
		nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
    }

	void Start()
	{
	}


    void Update ()
	{
		playerTransform = gameObject.GetComponentInChildren<EnemyFocusManager>().FindClosestPlayer().transform;
		playerHealth = playerTransform.GetComponent <PlayerHealth> ();

        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination (playerTransform.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}
