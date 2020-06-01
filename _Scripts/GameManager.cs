using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject[] LevelsEco;
    public GameObject[] LevelsDoc;
    public GameObject[] LevelsMulti;
    public GameObject InGameUI;
    public Text LevelNo;
    public TPSPlayer tpsPlayer;
    public Text TutorialInstructions;
    public Text HelpTutorial;
    int TutorialId = 0;
    public GameObject MissionDonePanel;
    public GameObject MissionFailedPanel;
    public GameObject CarCamera;
    public static bool IsDriving = false;
    float TimeLeft;
    public Text TimeText;
    bool FinalPartOfMission = false;
    public int ThingsToPick;
    public int AnimalsToHeal;
    public GameObject FadeInOut;
    public void FadeIn()
    {
        Invoke("DisableFade", 2f);
        FadeInOut.SetActive(true);
        FadeInOut.GetComponent<Animation>().Play();
    }
    void DisableFade()
    {
        FadeInOut.SetActive(false);
        FadeInOut.GetComponent<Animation>().Stop();
    }
    void Awake()
    {
       // PlayerPrefs.DeleteAll();
        switch(PlayerPrefs.GetString("Mode"))
        {
            case "eco":
                if (PlayerPrefs.GetInt("LevelNoEco", 1) > LevelsEco.Length)
                {
                    PlayerPrefs.SetInt("LevelNoEco", LevelsEco.Length);
                }
                LevelsEco[PlayerPrefs.GetInt("LevelNoEco", 1) - 1].SetActive(true);
                LevelNo.text = "MISSION " + PlayerPrefs.GetInt("LevelNoEco", 1);
        switch (PlayerPrefs.GetInt("LevelNoEco", 1))
        {
            case 1:
                PlayerPrefs.SetInt("part", 1);
                TutorialInstructions.transform.parent.gameObject.SetActive(true);
                PlayerPrefs.SetInt("part", 1);
                ThingsToPick = 3;
                Invoke("StartTutorial", 4f);
                break;
            case 2:
                PlayerPrefs.SetInt("part1", 1);
                ThingsToPick = 6;
                TimeLeft = 400;
                Timer();
                TutorialInstructions.transform.parent.gameObject.SetActive(true);
                TutorialInstructions.text = "Pick Garbage from 2 different \nspots!\nUse vehicle & map to navigate!";
                HelpTutorial.text = "Find vehicle using map and go to destination to pick trash";
                Invoke("StartTutorial", 0f);
                break;
            case 3:
                PlayerPrefs.SetInt("part2", 1);
                TutorialInstructions.text = "Pick Garbage from 3 different \nspots!\nUse vehicle & map to navigate!";
                ThingsToPick = 6;
                TimeLeft = 500;
                Timer();
                HelpTutorial.text = "Find vehicle using map and go to destination to pick trash";
                TutorialInstructions.transform.parent.gameObject.SetActive(true);
                Invoke("StartTutorial", 0f);
                break;

        }
                break;
            case "doctor":
                 if (PlayerPrefs.GetInt("LevelNoDoctor", 1) > LevelsDoc.Length)
                {
                    PlayerPrefs.SetInt("LevelNoDoctor", LevelsDoc.Length);
                }
                 LevelsDoc[PlayerPrefs.GetInt("LevelNoDoctor", 1) - 1].SetActive(true);
                 LevelNo.text = "MISSION " + PlayerPrefs.GetInt("LevelNoDoctor", 1);
        switch (PlayerPrefs.GetInt("LevelNoDoctor", 1))
        {
            case 1:
                PlayerPrefs.SetInt("part", 1);
                TutorialInstructions.transform.parent.gameObject.SetActive(true);
                PlayerPrefs.SetInt("part", 1);
                ThingsToPick = 3;
                Invoke("StartTutorial", 4f);
                break;
            case 2:      
                TutorialInstructions.transform.parent.gameObject.SetActive(true);
                TutorialInstructions.text = "Heal animals from 3 different \nspots!\nUse vehicle & map to navigate!";
                Debug.Log("wtf");
                PlayerPrefs.SetInt("part1", 1);
                ThingsToPick = 0;
                AnimalsToHeal = 1;
                TimeLeft = 400;
                Timer();
                HelpTutorial.text = "Find vehicle using map and go to destination to heal animal";
                Invoke("StartTutorial", 0f);
                break;
            case 3:
                PlayerPrefs.SetInt("part2", 1);
                TutorialInstructions.text = "Heal animals from 2 different \nspots!\nYou may encounter deadly animal's\nUse tranquilizer to neutralize\nanimal!";
                ThingsToPick = 0;
                AnimalsToHeal = 1;
                TimeLeft = 500;
                Timer();
                HelpTutorial.text = "Find vehicle using map and go to destination to pick trash";
                TutorialInstructions.transform.parent.gameObject.SetActive(true);
                Invoke("StartTutorial", 0f);
                break;
        }
                break;
            case "multi":
                 if (PlayerPrefs.GetInt("LevelNoMulti", 1) > LevelsMulti.Length)
                {
                    PlayerPrefs.SetInt("LevelNoMulti", LevelsMulti.Length);
                }
                 LevelsMulti[PlayerPrefs.GetInt("LevelNoMulti", 1) - 1].SetActive(true);
                break;

        }
        tpsPlayer = FindObjectOfType<TPSPlayer>();
        ////for music on/off
        if (PlayerPrefs.GetInt("Music", 0) == 0)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
        ///for checking quality-settings
        if (PlayerPrefs.GetInt("Quality", 0) == 0)
        {
            QualitySettings.SetQualityLevel(0);
        }
        else
        {
            QualitySettings.SetQualityLevel(1);
        }
        /////////
        //if (PlayerPrefs.GetInt("Introduction", 0) == 1)
        //{
        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;
        //    IntroUi.SetActive(true);
        //    //IntroLevel.SetActive(true);
        //}
        //else
        //{
        tpsPlayer.crosshairEnabled = false;
        FindObjectOfType<SmoothMouseLook>().enabled = false;
        FindObjectOfType<InputControl>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        InGameUI.SetActive(true);
       
        //}

    }
    public void TaskDone()
    {
        if (PlayerPrefs.GetString("Mode") == "eco")
        {
            switch (PlayerPrefs.GetInt("LevelNoEco", 1))
            {
                case 1:
                    if (PlayerPrefs.GetInt("part", 1) == 1)
                    {
                        FinalPartOfMission = true;
                        PlayerPrefs.SetInt("part", 2);
                        ThingsToPick = 6;
                        TimeLeft = 180;
                        Timer();
                        HelpTutorial.text = "Find vehicle using map and go to destination";
                        FindObjectOfType<EnterExitCar>().enabled = true;
                    }
                    break;
                case 2:
                    if (PlayerPrefs.GetInt("part1", 1) == 1)
                    {
                        Debug.Log("hi");
                        FindObjectOfType<RCC_CarControllerV3>().GetComponent<Rigidbody>().isKinematic = false;
                        FinalPartOfMission = true;
                        ThingsToPick = 6;
                        PlayerPrefs.SetInt("part1", 2);
                        HelpTutorial.text = "Please Enter In Vehicle to go to destination!";
                    }
                    break;
                case 3:
                    if (PlayerPrefs.GetInt("part2", 1) == 1)
                    {
                        FindObjectOfType<RCC_CarControllerV3>().GetComponent<Rigidbody>().isKinematic = false;
                        ThingsToPick = 6;
                        PlayerPrefs.SetInt("part2", 2);
                        HelpTutorial.text = "Please Enter In Vehicle to go to destination!";
                    }
                    if (PlayerPrefs.GetInt("part2", 1) == 2)
                    {
                        FindObjectOfType<RCC_CarControllerV3>().GetComponent<Rigidbody>().isKinematic = false;
                        FinalPartOfMission = true;
                        ThingsToPick = 6;
                        PlayerPrefs.SetInt("part2", 3);
                        HelpTutorial.text = "Please Enter In Vehicle to go to destination!";
                    }
                    break;

            }
        }
        else
            if (PlayerPrefs.GetString("Mode") == "doctor")
            {
                switch (PlayerPrefs.GetInt("LevelNoDoctor", 1))
                {
                    case 1:
                        if (PlayerPrefs.GetInt("part", 1) == 1)
                        {
                            FinalPartOfMission = true;
                            PlayerPrefs.SetInt("part", 2);
                            ThingsToPick = 0;
                            AnimalsToHeal = 1;
                            TimeLeft = 180;
                            Timer();
                            HelpTutorial.text = "Find vehicle using map and go to destination";
                            FindObjectOfType<EnterExitCar>().enabled = true;
                        }
                        break;
                    case 2:
                        if (PlayerPrefs.GetInt("part1", 1) == 1)
                        {
                            Debug.Log("hi");
                            FindObjectOfType<RCC_CarControllerV3>().GetComponent<Rigidbody>().isKinematic = false;
                            FinalPartOfMission = true;
                            ThingsToPick = 0;
                            AnimalsToHeal = 1;
                            PlayerPrefs.SetInt("part1", 2);
                            HelpTutorial.text = "Please Enter In Vehicle to go to destination!";
                        }
                        if (PlayerPrefs.GetInt("part1", 1) == 2)
                        {
                            FindObjectOfType<RCC_CarControllerV3>().GetComponent<Rigidbody>().isKinematic = false;
                            FinalPartOfMission = true;
                            ThingsToPick = 0;
                            AnimalsToHeal = 1;
                            PlayerPrefs.SetInt("part1", 3);
                            HelpTutorial.text = "Please Enter In Vehicle to go to destination!";
                        }
                        break;
                    case 3:
                        if (PlayerPrefs.GetInt("part2", 1) == 1)
                        {
                            FindObjectOfType<RCC_CarControllerV3>().GetComponent<Rigidbody>().isKinematic = false;
                            ThingsToPick = 0;
                            AnimalsToHeal = 1;
                            PlayerPrefs.SetInt("part2", 2);
                            HelpTutorial.text = "Please Enter In Vehicle to go to destination!";
                        }
                     
                        break;

                }
            }
            else
            {
               if (PlayerPrefs.GetString("Mode") == "multi")
               {

               }
            }
    }
    void Timer()
    {
        if (TimeLeft > 0 )
        {
            if (ThingsToPick == 0 &&  AnimalsToHeal == 0 && !MissionDonePanel.activeInHierarchy && FinalPartOfMission)
            {
                FinalPartOfMission = false;
                MissionCompleted();
            }
            Invoke("Timer", 1f);
            TimeLeft = TimeLeft - 1;
            TimeText.text = "Time Left: " + (int)TimeLeft / 60 + ":" + TimeLeft % 60;
        }
        else
        {
            if(!MissionFailedPanel.activeInHierarchy)
            GameIsOver();
        }
    }
    public void CLoseInstruction()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        tpsPlayer.crosshairEnabled = true;
        FindObjectOfType<SmoothMouseLook>().enabled = true;
        FindObjectOfType<InputControl>().enabled = true;
    }
    public void EnterInCar()
    {
        Cursor.lockState = CursorLockMode.None;
        FindObjectOfType<CameraControl>().GetComponent<Camera>().farClipPlane = 0;
        CarCamera.gameObject.SetActive(true);
        FindObjectOfType<InputControl>().enabled = false;
        tpsPlayer.crosshairEnabled = false;
    }
    public void ExitCar()
    {
        Cursor.lockState = CursorLockMode.Locked;
        FindObjectOfType<CameraControl>().GetComponent<Camera>().farClipPlane = 200;
        tpsPlayer.crosshairEnabled = true;
        FindObjectOfType<InputControl>().enabled = true;
        tpsPlayer.transform.position = FindObjectOfType<RCC_CarControllerV3>().transform.position;
        CarCamera.gameObject.SetActive(false);
    }
    void StartTutorial()
    {
        PlayerPrefs.SetInt("InitialTutorial", 1);
    }
    void Update()
    {
        
       
    }
    public void GameIsOver()
    {
        tpsPlayer.crosshairEnabled = false;
        Cursor.lockState = CursorLockMode.None;
        MissionFailedPanel.SetActive(true);
        FindObjectOfType<SmoothMouseLook>().enabled = false;
    }
    public void MissionCompleted()
    {
       // Debug.Log("hfj");
        tpsPlayer.crosshairEnabled = false;
        Cursor.lockState = CursorLockMode.None;
        FindObjectOfType<SmoothMouseLook>().enabled = false;
        switch (PlayerPrefs.GetString("Mode"))
        {
            case "eco":
                PlayerPrefs.SetInt("LevelNoEco", PlayerPrefs.GetInt("LevelNoEco", 1) + 1);
                break;
            case "doctor":
                PlayerPrefs.SetInt("LevelNoDoctor", PlayerPrefs.GetInt("LevelNoDoctor", 1) + 1);
                break;
            case "multi":
                PlayerPrefs.SetInt("LevelNoMulti", PlayerPrefs.GetInt("LevelNoMulti", 1) + 1);
                break;
        }
        MissionDonePanel.SetActive(true);
    }
    public void Next()
    {
        tpsPlayer.levelLoadFadeObj.GetComponent<LevelLoadFade>().FadeAndLoadLevel(Color.black, 1.2f, false);
    }
}
