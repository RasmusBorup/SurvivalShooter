using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour 
{
	[SerializeField]
	GameObject buttons;
	[SerializeField]
	GameObject playerPrefab;
	[SerializeField]
	GameObject loadButtons;
	[SerializeField]
	GameObject newGame;
	[SerializeField]
	GameObject newGameNameInput;
	[SerializeField]
	Button newGameStartButton;
	[SerializeField]
	Button buttonPrefab;
	GameObject backButton;

	void Awake()
	{
		backButton = GameObject.Find("BackButton");
		backButton.SetActive(false);
	}

	public void NewGame()
	{
		buttons.SetActive(false);
		newGame.SetActive(true);
		backButton.SetActive(true);
	}

	public void StartNewGame()
	{
		string saveName = newGameNameInput.GetComponentInChildren<Text>().text;

		StateManager.CreateGame(saveName);
		StartGame(saveName);
	}

	public void StartGame(string saveName)
	{
		StateManager.Load(saveName);
		SceneManager.LoadScene ("Level 01");
		Time.timeScale = 1;
	}

	public void Load()
	{
		string[] saveFiles = Directory.GetFiles(Application.persistentDataPath + "/saves");
		buttons.SetActive(false);
		loadButtons.SetActive(true);
		backButton.SetActive(true);
		int yPosition = 250;

		foreach(string file in saveFiles) {
			string saveName = Path.GetFileName(file);
			Button saveFileButton = Instantiate(buttonPrefab);
			saveFileButton.GetComponentInChildren<Text>().text = saveName;
			saveFileButton.transform.SetParent(loadButtons.transform);
			saveFileButton.transform.localPosition = new Vector3(0, yPosition, 0);
			saveFileButton.onClick.AddListener(() => StartGame(saveName));

			yPosition -= 50;
		}
	}

	public void ShowMainMenu()
	{
		buttons.SetActive(true);
		loadButtons.SetActive(false);
		newGame.SetActive(false);
		backButton.SetActive(false);
	}

	public void Quit()
	{
		Application.Quit ();
	}
}
