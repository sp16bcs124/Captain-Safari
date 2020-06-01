using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuFunctions : MonoBehaviour {
    public GameObject MusicOn,MusicOff;
    public GameObject LowGraphics, HighGraphics;
    public GameObject LoadingImage;
    public GameObject MultiRoom;
	void Awake () {
        ////for music on/off
        if (PlayerPrefs.GetInt("Music", 0) == 0)
        {
            AudioListener.volume = 1;
            MusicOn.gameObject.SetActive(true);
            MusicOff.gameObject.SetActive(false);
        }
        else
        {
            AudioListener.volume = 0;
            MusicOn.gameObject.SetActive(false);
            MusicOff.gameObject.SetActive(true);
        }
        ///for checking quality-settings
        if (PlayerPrefs.GetInt("Quality", 0) == 0)
        {
            HighGraphics.SetActive(true);
            LowGraphics.SetActive(false);
            QualitySettings.SetQualityLevel(6);
        }
        else
        {
            HighGraphics.SetActive(false);
            LowGraphics.SetActive(true);
            QualitySettings.SetQualityLevel(5);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void NewGame()
    {
      // int previouscore= PlayerPrefs.GetInt("score",0);
        //if(score>previouscore)
        //{

        //    PlayerPrefs.SetInt("score", score);
        //}
        PlayerPrefs.DeleteAll();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartGame(string mode)
    {
        PlayerPrefs.SetString("Mode", mode);
        if (mode == "multi")
        {
            MultiRoom.SetActive(true);
        }
        else
        {
            LoadingImage.SetActive(true);
            Application.LoadLevel(1);
        }
    }
    public void ChangeQuality()
    {
        if (PlayerPrefs.GetInt("Quality", 0) == 0)
        {
            HighGraphics.SetActive(false);
            LowGraphics.SetActive(true);
            PlayerPrefs.SetInt("Quality", 1);
            QualitySettings.SetQualityLevel(5);
        }
        else
        {
            HighGraphics.SetActive(true);
            LowGraphics.SetActive(false);
            PlayerPrefs.SetInt("Quality", 0);
            QualitySettings.SetQualityLevel(6);
        }
    }
    public void ToggleMusic()
    {
        if (PlayerPrefs.GetInt("Music", 0) == 0)
        {
            AudioListener.volume = 0;
            PlayerPrefs.SetInt("Music", 1);
            MusicOn.gameObject.SetActive(false);
            MusicOff.gameObject.SetActive(true);
        }
        else
        {
            AudioListener.volume = 1;
            PlayerPrefs.SetInt("Music", 0);
            MusicOn.gameObject.SetActive(true);
            MusicOff.gameObject.SetActive(false);
        }

    }
}
