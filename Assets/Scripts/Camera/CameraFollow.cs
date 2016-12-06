using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	public float smoothing = 5f;

	public Transform target;
	Vector3 offset;
	bool playerFound;

	void Start()
	{
		playerFound = false;
	}

	void FixedUpdate()
	{
		if (!playerFound) 
		{
			GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
			foreach (GameObject player in players) 
			{
				Debug.Log("Found " + players.Length + " players");
				if(player.networkView.isMine)
				{
					target = player.GetComponent<Transform>();
					offset = transform.position - target.position;
					Debug.Log ("Found My Player To Follow");
					playerFound = true;
				}
			}
		}

		if(playerFound)
		{
			Vector3 targetCamPos = target.position + offset;
			transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
		}
	}
}
