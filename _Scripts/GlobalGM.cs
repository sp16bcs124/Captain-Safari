using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGM : MonoBehaviour
{
    public GameObject LocalModesStuff;
    public GameObject MultiModeStuff;
    // Start is called before the first frame update
    void Awake()
    {
        if (PlayerPrefs.GetString("Mode")=="multi")
        {
            LocalModesStuff.SetActive(false);
            MultiModeStuff.SetActive(true);
        }
        else
        {
            LocalModesStuff.SetActive(true);
            MultiModeStuff.SetActive(false);
        } 
        
    }
 
}
