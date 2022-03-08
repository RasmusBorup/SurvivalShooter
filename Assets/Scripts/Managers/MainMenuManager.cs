using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuManager : MonoBehaviour 
{
	[SerializeField]
	GameObject buttons;
	[SerializeField]
	GameObject playerPrefab;


	public void Quit()
	{
		Application.Quit ();
	}

	public void StartGame()
	{
		SceneManager.LoadScene ("Level 01");
	}
}
