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
	float timeBetweenSpawns1 = 1f;
	float timeBetweenSpawns2 = 4f;
	float timeBetweenSpawns3 = 16f;
	public int enemiesAlive = 0;
	float timeBetweenWaves = 3f;
	float nightAnimationTime = 2f;

	Animator anim;
	Text nightText;
	int waveNumber;
	int stillSpawning;
	#endregion

    void Start ()
	{
        waveNumber = 0;
		nightText = GameObject.Find ("NightText").GetComponent<Text> ();
		anim = GameObject.Find ("NightImage").GetComponent<Animator> ();
    }

	IEnumerator SpawnWaves()
	{
		// Increment Night
		waveNumber++;
		nightText.text = "Night " + waveNumber;
		stillSpawning = 3;

		// Animate Night
		yield return new WaitForSeconds(timeBetweenWaves);
		AnimateNight();
		StateManager.Save();
		yield return new WaitForSeconds(nightAnimationTime);

		// Spawn new wave
		StartCoroutine(SpawnEnemy(enemy1, spawnPoints1, timeBetweenSpawns1, 1));
		StartCoroutine(SpawnEnemy(enemy2, spawnPoints2, timeBetweenSpawns2, 3));
		StartCoroutine(SpawnEnemy(enemy3, spawnPoints3, timeBetweenSpawns3, 5));
	}

    IEnumerator SpawnEnemy (GameObject enemy, Transform[] spawnPoints, float timeBetweenSpawns, int startWave)
    {
		if (waveNumber < startWave) {
			stillSpawning--;
			yield break;
		}

		int amount = (int)System.Math.Pow(2, waveNumber - startWave + 2);

		for (int i = 0; i < amount; i++) {
			yield return new WaitForSeconds((timeBetweenSpawns + enemiesAlive) / 10);

			int spawnPointIndex = Random.Range (0, spawnPoints.Length);
			Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		}

		stillSpawning--;
    }

	void Update()
	{
		if (enemiesAlive == 0 && stillSpawning == 0) {
			StartCoroutine(SpawnWaves());
		}
	}

	void AnimateNight()
	{
		anim.SetTrigger ("Night");
	}
}
