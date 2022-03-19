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
	float timeBetweenSpawns1 = 2f;
	[SerializeField]
	float timeBetweenSpawns2 = 2f;
	[SerializeField]
	float timeBetweenSpawns3 = 5f;
	[SerializeField]
	float timeBetweenWaves = 5f;
	[SerializeField]
	float nightAnimationTime;

	Animator anim;
	Text nightText;
	bool everythingKilled;
	bool waveDone;
	int waveNumber;
	int stillSpawning;
	#endregion

    void Start ()
	{
        waveNumber = 0;
		nightText = GameObject.Find ("NightText").GetComponent<Text> ();
		GameObject nightImage = GameObject.Find ("NightImage");
		waveDone = true;

		if(nightImage) {
			anim = nightImage.GetComponent<Animator> ();
		}
    }

	IEnumerator SpawnWaves()
	{
		// Increment Night
		waveDone = false;
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
		StartCoroutine(SpawnEnemy(enemy2, spawnPoints2, timeBetweenSpawns2, 2));
		StartCoroutine(SpawnEnemy(enemy3, spawnPoints3, timeBetweenSpawns3, 3));
	}

    IEnumerator SpawnEnemy (GameObject enemy, Transform[] spawnPoints, float timeBetweenSpawns, int startWave)
    {
		if (waveNumber < startWave) {
			stillSpawning--;
			yield break;
		}

		int amount = (int)System.Math.Pow(2, waveNumber - startWave + 1);

		for (int i = 0; i < amount; i++) {
			yield return new WaitForSeconds(timeBetweenSpawns);

			int spawnPointIndex = Random.Range (0, spawnPoints.Length);
			Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
			everythingKilled = false;
		}

		stillSpawning--;
    }

	void Update()
	{
		if (GameObject.FindWithTag ("Enemy") == null) {
			everythingKilled = true;
		}

		if (everythingKilled && stillSpawning == 0) {
			waveDone = true;
		}

		if (waveDone) {
			StartCoroutine(SpawnWaves());
		}
	}

	void AnimateNight()
	{
		anim.SetTrigger ("Night");
	}
}
