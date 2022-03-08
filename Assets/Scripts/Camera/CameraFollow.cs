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
			GameObject player = GameObject.Find("Player");
			target = player.GetComponent<Transform>();
			offset = transform.position - target.position;
			playerFound = true;
		}

		if(playerFound)
		{
			Vector3 targetCamPos = target.position + offset;
			transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
		}
	}
}
