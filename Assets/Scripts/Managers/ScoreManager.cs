using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    Text text;

    void Awake ()
    {
        text = GetComponent <Text> ();
    }


    void Update ()
    {
        text.text = "Score: " + StateManager.CurrentScore;
    }
}
