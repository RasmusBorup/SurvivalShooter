using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public bool gameOver;

    PlayerHealth playerHealth;
    Animator anim;
    GameObject[] players;
    int playersAlive;
    int levelPrefix = 0;

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
			anim.SetTrigger("GameOver");	
			if(Input.GetKeyDown (KeyCode.R))
			{
                GetComponent<NetworkView>().RPC("RestartLevel", RPCMode.All);
			}
			if(Input.GetKeyDown(KeyCode.Q))
			{
                Network.Disconnect();
			}
        }
    }

    [RPC]
    void RestartLevel()
    {
        foreach (GameObject player in players)
        {
            if(player.GetComponent<NetworkView>().isMine)
            {
                Network.RemoveRPCs(player.GetComponent<NetworkView>().viewID);
            }
        }
        Application.LoadLevel(Application.loadedLevel);
    }
}
