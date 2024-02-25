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
        if (player.GetComponentInChildren<PlayerHealth>().currentHealth <= 0)
        {
            StateManager.Save();
            anim.SetTrigger("GameOver");
            Invoke("WakeUp", 4);
        }
    }

    void WakeUp()
    {
        SceneManager.LoadScene("Shop");
    }
}
