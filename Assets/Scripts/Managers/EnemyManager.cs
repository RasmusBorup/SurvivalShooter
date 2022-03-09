using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
	#region Fields
	[SerializeField]
	GameObject enemy1;
	[SerializeField]
	GameObject enemy2;
	[SerializeField]
	GameObject enemy3;

	[SerializeField]
	Transform[] spawnPoints1;
	[SerializeField]
	Transform[] spawnPoints2;
	[SerializeField]
	Transform[] spawnPoints3;

	[SerializeField]
	int[] waveAmounts1;
	[SerializeField]
	int[] waveAmounts2;
	[SerializeField]
	int[] waveAmounts3;

	[SerializeField]
	float timeBetweenSpawns1 = 2f;
	[SerializeField]
	float timeBetweenSpawns2 = 2f;
	[SerializeField]
	float timeBetweenSpawns3 = 5f;

	[SerializeField]
	float timeBetweenWaves = 5f;
	[SerializeField]
	float nightAnimationTime;
	[SerializeField]
	bool everythingKilled;

	Animator anim;
	Text nightText;
	int waveNumber;
	GameObject[] players;
	bool gameOver;
	#endregion

    void Start ()
	{
        everythingKilled = true;
        waveNumber = 1;
		nightText = GameObject.Find ("NightText").GetComponent<Text> ();
		GameObject nightImage = GameObject.Find ("NightImage");
		if(nightImage)
		{
			anim = nightImage.GetComponent<Animator> ();
		}

		StartCoroutine (Waves1());
		StartCoroutine (Waves2());
		StartCoroutine (Waves3 ());

		gameOver = false;
    }

	void Update()
	{
		if (GameObject.FindGameObjectsWithTag ("Enemy").Length == 0) 
		{
			everythingKilled = true;
		}
		else 
		{
			everythingKilled = false;
		}
		
        gameOver = GameObject.Find("GameOver").GetComponent<GameOverManager>().gameOver;
	}

    void SpawnEnemy1 ()
    {
       if(gameOver)
       {
           return;
       }

        int spawnPointIndex = Random.Range (0, spawnPoints1.Length);
		Instantiate (enemy1, spawnPoints1[spawnPointIndex].position, spawnPoints1[spawnPointIndex].rotation);
    }
	
	void SpawnEnemy2 ()
	{
       if(gameOver)
		{
			return;
		}

		int spawnPointIndex = Random.Range (0, spawnPoints2.Length);
		Instantiate (enemy2, spawnPoints2[spawnPointIndex].position, spawnPoints2[spawnPointIndex].rotation);
	}
	
	void SpawnEnemy3 ()
	{
       if(gameOver)
		{
			return;
		}

		int spawnPointIndex = Random.Range (0, spawnPoints3.Length);
		Instantiate (enemy3, spawnPoints3[spawnPointIndex].position, spawnPoints3[spawnPointIndex].rotation);
	}

	IEnumerator Waves1()
	{
		foreach(int amount in waveAmounts1)
		{
			yield return new WaitForSeconds(timeBetweenSpawns1 + 1);

			while(!everythingKilled)
			{
				yield return new WaitForSeconds(1);
			}

			if(everythingKilled)
			{
				yield return new WaitForSeconds(timeBetweenWaves);
				AnimateNight();
				yield return new WaitForSeconds(nightAnimationTime);

				for(int i = 0; i < amount; i++)
				{
					Invoke("SpawnEnemy1", timeBetweenSpawns1);
					yield return new WaitForSeconds(timeBetweenSpawns1);
                }

                UpdateNight();
			}
		}
	}
	
	IEnumerator Waves2()
	{
		foreach(int amount in waveAmounts2)
		{
			yield return new WaitForSeconds(timeBetweenSpawns1 + 1);

			while(!everythingKilled)
			{
				yield return new WaitForSeconds(1);
			}

			if(everythingKilled)
			{
				yield return new WaitForSeconds(timeBetweenWaves);
				yield return new WaitForSeconds(nightAnimationTime);

				for(int i = 0; i < amount; i++)
				{
					Invoke("SpawnEnemy2", timeBetweenSpawns2);
					yield return new WaitForSeconds(timeBetweenSpawns2);
				}
			}
		}
	}
	
	IEnumerator Waves3()
	{
		foreach(int amount in waveAmounts3)
		{
			yield return new WaitForSeconds(timeBetweenSpawns1 + 1);

			while(!everythingKilled)
			{
				yield return new WaitForSeconds(1);
			}

			if(everythingKilled)
			{
				yield return new WaitForSeconds(timeBetweenWaves);
				yield return new WaitForSeconds(nightAnimationTime);
				for(int i = 0; i < amount; i++)
				{
					Invoke("SpawnEnemy3", timeBetweenSpawns3);
					yield return new WaitForSeconds(timeBetweenSpawns3);
				}
			}
		}
	}

    void UpdateNight()
    {
        waveNumber++;
        nightText.text = "Night " + waveNumber;
    }

	void AnimateNight()
	{
		anim.SetTrigger ("Night");
	}
}
