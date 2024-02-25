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
    GameObject buttonPrefab;
    GameObject backButton;
    public GameObject optionsPanel;
    Button optionsButton;
    int saveButtonCount = 0;

    void Awake()
    {
        backButton = GameObject.Find("BackButton");
        backButton.SetActive(false);
        optionsButton = GameObject.Find("OptionsButton").GetComponent<Button>();
        optionsButton.onClick.AddListener(ShowOptions);
        optionsPanel.GetComponentInChildren<Button>().onClick.AddListener(SaveOptions);
        ShowMainMenu();
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
        StartGame(saveName, true);
    }

    public void StartGame(string saveName, bool newGame)
    {
        StateManager.Load(saveName);
        if (newGame)
        {
            SceneManager.LoadScene("Level 01");
        }
        else
        {
            SceneManager.LoadScene("Shop");
        }
        Time.timeScale = 1;
    }

    public void Load()
    {
        buttons.SetActive(false);
        loadButtons.SetActive(true);
        backButton.SetActive(true);

        if (saveButtonCount > 0)
        {
            return;
        }

        string[] saveFiles = Directory.GetFiles(Application.persistentDataPath + "/saves");
        int yPosition = 250;

        foreach (string file in saveFiles)
        {
            string saveName = Path.GetFileName(file);
            GameObject saveFileButtonSet = Instantiate(buttonPrefab);
            saveFileButtonSet.transform.SetParent(loadButtons.transform);
            saveFileButtonSet.transform.localPosition = new Vector3(0, yPosition, 0);

            Button saveFileButton = saveFileButtonSet.transform.Find("LoadButton").GetComponent<Button>();
            saveFileButton.GetComponentInChildren<Text>().text = saveName;
            saveFileButton.onClick.AddListener(() => StartGame(saveName, false));

            Button deleteFileButton = saveFileButtonSet.transform.Find("DeleteButton").GetComponent<Button>();
            deleteFileButton.onClick.AddListener(() => DeleteGame(file, saveFileButtonSet));

            yPosition -= 50;
            saveButtonCount++;
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

    public void DeleteGame(string fileName, GameObject buttons)
    {
        File.Delete(fileName);
        Destroy(buttons);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
