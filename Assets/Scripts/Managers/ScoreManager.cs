using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score;


    Text text;


    void Awake ()
    {
        text = GetComponent <Text> ();
        score = StateManager.CurrentScore;
    }


    void Update ()
    {
        text.text = "Score: " + score;
        StateManager.CurrentScore = score;
    }
}
