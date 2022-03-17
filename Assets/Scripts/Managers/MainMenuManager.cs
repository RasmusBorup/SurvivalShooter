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
	Button buttonPrefab;

	public void StartGame()
	{
		SceneManager.LoadScene ("Level 01");
		Time.timeScale = 1;
	}

	public void Load()
	{
		string[] saveFiles = Directory.GetFiles(Application.persistentDataPath);
		buttons.SetActive(false);
		loadButtons.SetActive(true);
		int yPosition = 250;

		foreach(string file in saveFiles) {
			Button saveFileButton = Instantiate(buttonPrefab);
			saveFileButton.GetComponentInChildren<Text>().text = Path.GetFileName(file);
			saveFileButton.transform.SetParent(loadButtons.transform);
			saveFileButton.transform.localPosition = new Vector3(0, yPosition, 0);
			saveFileButton.onClick.AddListener(() => StateManager.Load(Path.GetFileName(file)));
			saveFileButton.onClick.AddListener(StartGame);

			yPosition -= 50;
		}
	}

	public void Quit()
	{
		Application.Quit ();
	}
}
