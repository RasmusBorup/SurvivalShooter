using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject music = GameObject.Find("BackgroundMusic");
        GameObject gun = GameObject.Find("GunBarrelEnd");
        GameObject player = GameObject.Find("Player");
        music.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("masterVolume") * PlayerPrefs.GetFloat("musicVolume");
        player.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("masterVolume") * PlayerPrefs.GetFloat("enemyVolume");

        if (gun) {
            gun.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("masterVolume") * PlayerPrefs.GetFloat("gunVolume");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
