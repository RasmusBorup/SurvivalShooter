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
	public GameObject optionsPanel;
	Button optionsButton;
	ArrayList saveFileButtons;

	void Awake()
	{
		backButton = GameObject.Find("BackButton");
		backButton.SetActive(false);
		optionsButton = GameObject.Find("OptionsButton").GetComponent<Button>();
		optionsButton.onClick.AddListener(ShowOptions);
		optionsPanel.GetComponentInChildren<Button>().onClick.AddListener(SaveOptions);
		ShowMainMenu();
		saveFileButtons = new ArrayList();
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
		SceneManager.LoadScene ("Shop");
		Time.timeScale = 1;
	}

	public void Load()
	{
		buttons.SetActive(false);
		loadButtons.SetActive(true);
		backButton.SetActive(true);

		if (saveFileButtons.Count > 0) {
			return;
		}

		string[] saveFiles = Directory.GetFiles(Application.persistentDataPath + "/saves");
		int yPosition = 250;

		foreach(string file in saveFiles) {
			string saveName = Path.GetFileName(file);
			Button saveFileButton = Instantiate(buttonPrefab);
			saveFileButton.GetComponentInChildren<Text>().text = saveName;
			saveFileButton.transform.SetParent(loadButtons.transform);
			saveFileButton.transform.localPosition = new Vector3(0, yPosition, 0);
			saveFileButton.onClick.AddListener(() => StartGame(saveName));

			yPosition -= 50;
			saveFileButtons.Add(saveFileButton);
		}
	}

	void ShowOptions()
	{
		buttons.SetActive(false);
		backButton.SetActive(true);
		optionsPanel.SetActive(true);
		optionsPanel.transform.Find("MasterVolumeSlider").GetComponentInChildren<Slider>().value = PlayerPrefs.GetFloat("masterVolume");
		optionsPanel.transform.Find("MusicVolumeSlider").GetComponentInChildren<Slider>().value = PlayerPrefs.GetFloat("musicVolume");
		optionsPanel.transform.Find("GunVolumeSlider").GetComponentInChildren<Slider>().value = PlayerPrefs.GetFloat("gunVolume");
		optionsPanel.transform.Find("EnemyVolumeSlider").GetComponentInChildren<Slider>().value = PlayerPrefs.GetFloat("enemyVolume");
		optionsPanel.transform.Find("PlayerVolumeSlider").GetComponentInChildren<Slider>().value = PlayerPrefs.GetFloat("playerVolume");
	}

	void SaveOptions()
	{
		PlayerPrefs.SetFloat("masterVolume", optionsPanel.transform.Find("MasterVolumeSlider").GetComponentInChildren<Slider>().value);
		PlayerPrefs.SetFloat("musicVolume", optionsPanel.transform.Find("MusicVolumeSlider").GetComponentInChildren<Slider>().value);
		PlayerPrefs.SetFloat("gunVolume", optionsPanel.transform.Find("GunVolumeSlider").GetComponentInChildren<Slider>().value);
		PlayerPrefs.SetFloat("enemyVolume", optionsPanel.transform.Find("EnemyVolumeSlider").GetComponentInChildren<Slider>().value);
		PlayerPrefs.SetFloat("playerVolume", optionsPanel.transform.Find("PlayerVolumeSlider").GetComponentInChildren<Slider>().value);

		PlayerPrefs.Save();
	}

	public void ShowMainMenu()
	{
		buttons.SetActive(true);
		loadButtons.SetActive(false);
		newGame.SetActive(false);
		optionsPanel.SetActive(false);
		backButton.SetActive(false);
	}

	public void Quit()
	{
		Application.Quit ();
	}
}
