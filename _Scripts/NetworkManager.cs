using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class NetworkManager : MonoBehaviour {
    [SerializeField]
    Text ConnectionText;
    [SerializeField]
    Transform [] spawnPoints;
    [SerializeField]
    GameObject ServerWindow;
    [SerializeField]
    InputField UserName;
    [SerializeField]
    InputField RoomName;
    [SerializeField]
    InputField RoomList;
    [SerializeField]
    InputField MessageWindow;
    [SerializeField]
    GameObject GameOverStats;
    [SerializeField]
    InputField PlayerNamesList;

    [SerializeField]
    Camera SceneCamera;
    GameObject player;
    [SerializeField]

    Queue<string> messages;
    const int messagesCount = 5;
    PhotonView photonView;
    public GameObject MiniMAP;
    ///////////
    ExitGames.Client.Photon.Hashtable EnemiesProp,RoomProps;
    /////////////
    bool Proceed = false;
	// Use this for initialization
	void Start () {
        //EnemiesProp = new ExitGames.Client.Photon.Hashtable();
        ////RoomProps = new ExitGames.Client.Photon.Hashtable();
        //EnemiesProp["ItemsPick"] = 0;
       // EnemiesProp["Deaths"] = 0;
        //PhotonNetwork.player.SetCustomProperties(EnemiesProp);
        photonView = gameObject.GetComponent<PhotonView>();
        messages = new Queue<string>(messagesCount);
        // PhotonNetwork.logLevel = PhotonLogLevel.Full;
        // PhotonNetwork.ConnectUsingSettings("0.1");
        //StartCoroutine("UpdateConnectionString");
        StartSpawnProcess(0f);
	}
	
    void StartSpawnProcess(float respawnTime)
    {
        SceneCamera.enabled = true;
        StartCoroutine("SpawnPlayer", respawnTime);
    }
     IEnumerator SpawnPlayer(float respawnTime)
    {
        yield return new WaitForSeconds(respawnTime);
        int index = Random.Range(0, spawnPoints.Length);
        player = PhotonNetwork.Instantiate("TP Multi", spawnPoints[index].position, spawnPoints[index].rotation, 0);
        player.GetComponent<PlayerNNetworkMover>().RespawnMe += StartSpawnProcess;
        //player.GetComponent<PlayerNNetworkMover>().SendNetworkMessage += Add_Message;
        //Add_Message("Spawned player: " + PhotonNetwork.player.name);
        SceneCamera.enabled = false;
        MiniMAP.SetActive(true);
    }
    //void Add_Message(string message)
    // {
    //     photonView.RPC("Add_Message_RPC", PhotonTargets.All, message);
    // }
    [PunRPC]
    void Add_Message_RPC(string message)
     {
         MessageWindow.text = "";
         messages.Enqueue(message);
        if(messages.Count>messagesCount)
        {
            messages.Dequeue();
        }
        foreach(string m in messages)
        {
            MessageWindow.text += m + "\n";
        }
     }
   
    public void PopGameOver()
    {
        Proceed = false;
       // Time.timeScale = 0f;
       // Deaths = 0;
        PlayerNamesList.text = "";
        for(int i=0;i<PhotonNetwork.playerList.Length;i++)
        {
            PlayerNamesList.text += PhotonNetwork.playerList[i].name + "                            " + PhotonNetwork.playerList[i].GetScore().ToString() + "\n";        
        }
        MessageWindow.text = "";
        GameOverStats.gameObject.SetActive(true);
       // GameObject.Find("CF").SetActive(false);
        Invoke("Continue", 10f);
        
    }
    //void AutoContinue()
    //{
    //    if (!Proceed)
    //    {
    //        Continue();
    //    }
    //}
    public void LeaveRoom()
    {
        PlayerPrefs.SetInt("MultiPlayer", 0);
        Proceed = true;
        for (int i = 0; i < messages.Count; i++)
        {
            messages.Dequeue();
        }   
        GameOverStats.gameObject.SetActive(false);
        //ExitGames.Client.Photon.Hashtable EnemiesProp = new ExitGames.Client.Photon.Hashtable();
        //EnemiesProp["ItemsPick"] = 0;
        //PhotonNetwork.player.SetCustomProperties(EnemiesProp);
        Destroy(GameObject.Find("FPS Weapons").gameObject);
        Destroy(GameObject.Find("FPS Camera").gameObject);
        Destroy(GameObject.Find("FPS Effects").gameObject);
        Destroy(GameObject.Find("FPS Player").gameObject);
        Destroy(GameObject.Find("Crosshair(Clone)").gameObject);
        Destroy(GameObject.Find("Hitmarker(Clone)").gameObject);
        Destroy(GameObject.Find("AmmoText(Clone)").gameObject);
      //  PhotonNetwork.Destroy(gameObject);
        SceneCamera.enabled = true;
        PhotonNetwork.LeaveRoom();
    }
    void OnLeftRoom()
    {
        //Add_Message(PhotonNetwork.player.name + " Left the game");
        PhotonNetwork.LoadLevel(0);
    }

    public void Continue()
    {
        if (!Proceed)
        {
            GameOverStats.gameObject.SetActive(false);
            SceneCamera.enabled = true;
            //Destroy(GameObject.Find("TPS Weapons").gameObject);
            //Destroy(GameObject.Find("TPS Camera").gameObject);
            //Destroy(GameObject.Find("TPS Effects").gameObject);
            //Destroy(GameObject.Find("TPS Player").gameObject);
            //Destroy(GameObject.Find("Hitmarker(Clone)").gameObject);
            //Destroy(GameObject.Find("AmmoText(Clone)").gameObject);
           // PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.player);
            //if (GameObject.FindGameObjectsWithTag("Flesh") != null)
            //{
            //    foreach(GameObject flesh in PlayerComponents)
            //    {
                    
            //    }
            //}
            PhotonNetwork.LeaveRoom();
           // StartSpawnProcess(0f);
        }
    }
    public void QuitMultiPlayer()
    {
        //Add_Message(PhotonNetwork.player.name+" Left the game");
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel(0);
    }
}
