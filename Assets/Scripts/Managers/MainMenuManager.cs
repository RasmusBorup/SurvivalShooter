using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour 
{
	[SerializeField]
	GameObject buttons;
	[SerializeField]
	GameObject servers;
	[SerializeField]
	GameObject playerPrefab;


	public void Quit()
	{
		Application.Quit ();
	}

	public void StartGame()
	{
		Application.LoadLevel ("Level 01");
	}
	
	public void FindServers()
	{
		buttons.SetActive (false);
		servers.SetActive (true);
	}
}
