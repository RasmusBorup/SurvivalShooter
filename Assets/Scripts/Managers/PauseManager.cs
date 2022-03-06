using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseManager : MonoBehaviour 
{
	public Image fader;
	public Text pauseText;
	public Button resumeButton;
	public Button quitToMainButton;
	public Button quitToDesktopButton;
	bool paused;

	// Use this for initialization
	void Start () 
	{
		paused = false;
		DisablePauseGameObjects ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
	   	{
            GetComponent<NetworkView>().RPC("PauseToggle", RPCMode.All);
		}
	}

    [RPC]
	void PauseToggle()
	{
		if (paused) 
		{
			Time.timeScale = 1;
			DisablePauseGameObjects();
		} 
		else 
		{
			Time.timeScale = 0;
			EnablePauseGameObjects();
		}
		paused = !paused;
	}

	void DisablePauseGameObjects()
	{
		fader.gameObject.SetActive(false);
		pauseText.enabled = false;
		resumeButton.gameObject.SetActive (false);
//		resumeButton.enabled = false;
		quitToMainButton.gameObject.SetActive (false);
//		quitToMainButton.enabled = false;
		quitToDesktopButton.gameObject.SetActive (false);
//		quitToDesktopButton.enabled = false;
	}

	void EnablePauseGameObjects()
	{
        fader.gameObject.SetActive (true);
		pauseText.enabled = true;
		resumeButton.gameObject.SetActive (true);
		quitToMainButton.gameObject.SetActive (true);
		quitToDesktopButton.gameObject.SetActive (true);
	}



	public void Resume()
    {
        GetComponent<NetworkView>().RPC("PauseToggle", RPCMode.All);
	}

	public void QuitToMain()
	{
        GetComponent<NetworkView>().RPC("PauseToggle", RPCMode.All);
        Network.Disconnect();
	}

	public void QuitToDesktop()
    {
        GetComponent<NetworkView>().RPC("PauseToggle", RPCMode.All);
        Network.Disconnect();
		Application.Quit ();
	}
}
