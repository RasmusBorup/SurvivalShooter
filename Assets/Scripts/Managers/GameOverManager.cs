using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public bool gameOver;

    PlayerHealth playerHealth;
    Animator anim;
    GameObject[] players;
    int playersAlive;
    //int levelPrefix = 0;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

	void Start()
	{
        gameOver = false;
        playersAlive = 0;
	}


    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        
        if(players.Length != 0)
        {
            playersAlive = players.Length;
            foreach (GameObject player in players)
            {
                if (player.GetComponentInChildren<PlayerHealth> ().currentHealth > 0) 
                {
                    playersAlive++;
                } 
                else 
                {
                    playersAlive--;
                }
            }
            if(playersAlive == 0)
            {
                gameOver = true;
            }
        }

        if (gameOver)
		{
            StateManager.Save();

			anim.SetTrigger("GameOver");	
			if(Input.GetKeyDown (KeyCode.R))
			{
                RestartLevel();
			}
			if(Input.GetKeyDown(KeyCode.Q))
			{
                SceneManager.LoadScene("Main Menu");
			}
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
