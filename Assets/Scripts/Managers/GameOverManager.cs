using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    Animator anim;
    GameObject player;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

	void Start()
	{
        player = GameObject.FindGameObjectWithTag("Player");
	}

    void Update()
    {
        if (player.GetComponentInChildren<PlayerHealth> ().currentHealth <= 0) 
        {
            StateManager.Save();
			anim.SetTrigger("GameOver");

			if(Input.GetKeyDown (KeyCode.R)) {
                WakeUp();
			}

			if(Input.GetKeyDown(KeyCode.Q))	{
                SceneManager.LoadScene("Main Menu");
			}
        }
    }

    void WakeUp()
    {
        SceneManager.LoadScene("Shop");
    }
}
