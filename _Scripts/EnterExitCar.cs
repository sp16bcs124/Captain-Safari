using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnterExitCar : MonoBehaviour {
    public Text whatToDo;
    GameObject fpsPlayer;
    GameManager GM;
    public bool ExitCar = false;
    public Transform [] Destination;
    int index = 0;
	// Use this for initialization
	void Start () {
        GM = FindObjectOfType<GameManager>();
        fpsPlayer = FindObjectOfType<TPSPlayer>().gameObject;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (fpsPlayer != null)
        {
            if (Vector3.Distance(transform.position, fpsPlayer.transform.position) < 5 && !GameManager.IsDriving)
            {
                if(!GM.TutorialInstructions.transform.parent.gameObject.activeInHierarchy)
                whatToDo.text = "Press E to Enter in Vehicle";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    this.GetComponent<Rigidbody>().isKinematic = false;
                    Destination[index].gameObject.SetActive(true);
                    switch (PlayerPrefs.GetString("Mode"))
                    {
                        case "eco":
                            GM.HelpTutorial.text = "Go to destination and pick garbage!";
                            break;
                        case "doctor":
                            GM.HelpTutorial.text = "Go to destination and heal animals!";
                            break;
                    }
                    this.GetComponent<bl_MiniMapItem>().enabled = false;
                    this.GetComponent<RCC_CarControllerV3>().canControl = true;
                    FindObjectOfType<bl_MiniMap>().m_Target = this.gameObject;
                    GameManager.IsDriving = true;
                    GM.EnterInCar();
                    FindObjectOfType<RCC_Camera>().playerCar = this.GetComponent<RCC_CarControllerV3>();
                }
            }
            else
                if (GameManager.IsDriving)
                {
                    if (this.GetComponent<RCC_CarControllerV3>().canControl && Vector3.Distance(transform.position, Destination[index].transform.position) < 20 )
                    {
                        this.GetComponent<RCC_CarControllerV3>().canControl = false;
                        this.GetComponent<Rigidbody>().isKinematic = true;
                        Invoke("DelayExit", 1f);
                        this.GetComponent<RCC_CarControllerV3>().canControl = false;
                        GameManager.IsDriving = false;
                        GM.HelpTutorial.text = "";
                        GM.FadeIn();
                        if (index < Destination.Length - 1)
                        {
                           // Debug.Log("wtf");
                            Destination[index].GetComponent<bl_MiniMapItem>().DestroyItem(true);
                            index = index + 1;
                        }
                        FindObjectOfType<bl_MiniMap>().m_Target = FindObjectOfType<TPSPlayer>().gameObject;
                        this.GetComponent<bl_MiniMapItem>().enabled = true;
                    }
                    GM.TutorialInstructions.text = "";
                    whatToDo.text = "Press E to Exit from Vehicle";
                    if (Input.GetKeyDown(KeyCode.E) || ExitCar)
                    {
                        FindObjectOfType<bl_MiniMap>().m_Target = FindObjectOfType<TPSPlayer>().gameObject;
                        this.GetComponent<bl_MiniMapItem>().enabled = true;
                        this.GetComponent<RCC_CarControllerV3>().canControl = false;
                        GameManager.IsDriving = false;
                        GM.ExitCar();
                    }
                }
                else
                {
                    whatToDo.text = "";
                }
        }
    }
    void DelayExit()
    {
        GM.ExitCar();
        fpsPlayer.transform.position = new Vector3(transform.position.x - 2, transform.position.y + 1, transform.position.z + 1);
    }
}
