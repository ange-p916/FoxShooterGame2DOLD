using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour {

    public Slider soundSlider;

    public static MusicManager Instance;
    public bool shouldGameMusicPlay = false;
    public bool DoNotDestroyOnLoad = true;

    bool playNext = true;

    public AudioSource menuclip;
    public AudioSource jumpsound;
    public AudioSource shotsound;
    public AudioSource hitsound;
    public AudioSource explosionsound;
    public AudioSource[] gameplayclips;

    AudioSource tempMusic;

    public AudioSource chargeUpSound;
    public AudioSource fireChargedShotSound;

    Player input;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }

        if(DoNotDestroyOnLoad)
        {
            DontDestroyOnLoad(this);
        }

        if(!menuclip.isPlaying && SceneManager.GetActiveScene().buildIndex < 3 && shouldGameMusicPlay)
        {
            menuclip.PlayDelayed(0f);
        }
        
        input = ReInput.players.GetPlayer(0);

        tempMusic = gameplayclips[0];
        gameplayclips[0].Stop();
    }

    void Update()
    {
        //if (soundSlider == null)
        //{
        //    if (SceneManager.GetActiveScene().name == "Options")
        //    {
        //        soundSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
        //    }
        //}

        //AudioListener.volume = soundSlider.value;

        //if (SceneManager.GetActiveScene().name == "Options")
        //{
        //    AudioListener.volume = soundSlider.value;
        //}

        if (SceneManager.GetActiveScene().buildIndex >= 3)
        {
            menuclip.Stop();
            
            if(!tempMusic.isPlaying)
            {
                tempMusic = gameplayclips[Random.Range(0, gameplayclips.Length)] as AudioSource;
                tempMusic.Play();
            }
                       

            if (input.GetButtonDown("Jump"))
            {
                if(!jumpsound.isPlaying)
                    jumpsound.Play();
            }
            if (input.GetButtonDown("Shoot"))
            {
                shotsound.Play();
            }
        }
    }

}
