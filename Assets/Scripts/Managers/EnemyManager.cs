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
			Debug.Log("NightImage Found");
			anim = nightImage.GetComponent<Animator> ();
		}
		if(anim)
			Debug.Log("Animator Found");
		if (Network.isServer) 
		{
			StartCoroutine (Waves1());
			StartCoroutine (Waves2());
			StartCoroutine (Waves3 ());

			gameOver = false;
		}
    }

	void Update()
	{
		if (Network.isServer) 
		{
			if (GameObject.FindGameObjectsWithTag ("Enemy").Length == 0) 
            {
				everythingKilled = true;
			}
            else 
            {
				everythingKilled = false;
			}
		}
        gameOver = GameObject.Find("GameOver").GetComponent<GameOverManager>().gameOver;
	}

    void SpawnEnemy1 ()
    {
//        if(gameOver)
//        {
//            return;
//        }

        int spawnPointIndex1 = Random.Range (0, spawnPoints1.Length);

		Network.Instantiate (enemy1, spawnPoints1[spawnPointIndex1].position, spawnPoints1[spawnPointIndex1].rotation, 0);
    }
	
	void SpawnEnemy2 ()
	{
//        if(gameOver)
//		{
//			return;
//		}

		int spawnPointIndex2 = Random.Range (0, spawnPoints2.Length);

		Network.Instantiate (enemy2, spawnPoints2[spawnPointIndex2].position, spawnPoints2[spawnPointIndex2].rotation, 0);
	}
	
	void SpawnEnemy3 ()
	{
//        if(gameOver)
//		{
//			return;
//		}

		int spawnPointIndex3 = Random.Range (0, spawnPoints3.Length);

		Network.Instantiate (enemy3, spawnPoints3[spawnPointIndex3].position, spawnPoints3[spawnPointIndex3].rotation, 0);
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
				networkView.RPC ("AnimateNight", RPCMode.All);
				yield return new WaitForSeconds(nightAnimationTime);
				for(int i = 0; i < amount; i++)
				{
					Invoke("SpawnEnemy1", timeBetweenSpawns1);
					yield return new WaitForSeconds(timeBetweenSpawns1);
                }
                if(Network.isServer)
                {
                    networkView.RPC("UpdateNight", RPCMode.All);
                }
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

    [RPC]
    void UpdateNight()
    {
        waveNumber++;
        nightText.text = "Night " + waveNumber;
    }

	[RPC]
	void AnimateNight()
	{
		anim.SetTrigger ("Night");
	}
}
