using UnityEngine;
using System.Collections;

public class EnemyFocusManager : MonoBehaviour 
{

	GameObject[] players;
	GameObject focusedPlayer;
	float distanceToTarget = -1;
	// Use this for initialization
	void Start () 
	{
	}

	void Awake()
	{
		players = GameObject.FindGameObjectsWithTag ("Player");
		if (players.Length > 0) 
		{
			FindClosestPlayer();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	void OnPlayerConnected()
	{
        FindClosestPlayer();
	}

	public GameObject FindClosestPlayer()
	{
		players = GameObject.FindGameObjectsWithTag ("Player");
		distanceToTarget = -1;
		foreach (GameObject player in players) 
		{
			float health = player.GetComponentInChildren<PlayerHealth>().currentHealth;
			float distance = (transform.position - player.transform.position).magnitude;
			if (health > 0)
			{
				if(distance < distanceToTarget || distanceToTarget == -1)
				{
					focusedPlayer = player;
					distanceToTarget = (transform.position - player.transform.position).magnitude;
				}
				else if(focusedPlayer.GetComponentInChildren<PlayerHealth>().currentHealth <= 0)
				{
					focusedPlayer = player;
					distanceToTarget = (transform.position - player.transform.position).magnitude;
				}
			}
			else if(health > 0 && distanceToTarget == -1)
			{
				focusedPlayer = player;
				distanceToTarget = (transform.position - player.transform.position).magnitude;
			}
		}
		return focusedPlayer;
	}
}
