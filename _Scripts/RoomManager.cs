using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class RoomManager : MonoBehaviour
{
    [SerializeField]
    Text ConnectionText;
    [SerializeField]
    GameObject ServerWindow;
    [SerializeField]
    InputField UserName;
    [SerializeField]
    InputField RoomName;
    [SerializeField]
    InputField RoomList;
    PhotonView photonView;
    [SerializeField]
    GameObject NoInternet;
    [SerializeField]
    GameObject LoadingImage;
    ///////////
    ExitGames.Client.Photon.Hashtable EnemiesProp, RoomProps;
    // Use this for initialization
    public static bool NotConnected = false;
   void OnEnable()
    {
        PhotonNetwork.autoJoinLobby = true;  
        // EnemiesProp = new ExitGames.Client.Photon.Hashtable();
        RoomProps = new ExitGames.Client.Photon.Hashtable();
        //EnemiesProp["Kills"] = 0;
        //EnemiesProp["Deaths"] = 0;
        //PhotonNetwork.player.SetCustomProperties(EnemiesProp);
        //  photonView = gameObject.GetComponent<PhotonView>();
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
        PhotonNetwork.ConnectUsingSettings("0.1");
        StartCoroutine("UpdateConnectionString");
    }
   public void QuitMulti()
   {
       StopAllCoroutines();
       ConnectionText.text = "";
       PhotonNetwork.Disconnect();
   }
    // Update is called once per frame
    void Update()
    {
        if (NotConnected)
        {
            NoInternet.gameObject.SetActive(true);
            NotConnected = false;
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator UpdateConnectionString()
    {
        while (true)
        {
         //   Debug.Log("wow");
            ConnectionText.text = PhotonNetwork.connectionStateDetailed.ToString();
            yield return null;
        }
    }
    //public virtual void OnConnectedToMaster()
    //{
    //    Debug.Log("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRandomRoom();");
    //    PhotonNetwork.JoinRandomRoom();
    //}
    void OnJoinedLobby()
    {
        Debug.Log("hmmm");
        ServerWindow.SetActive(true);
        ConnectionText.transform.parent.gameObject.SetActive(false);
        RoomName.text = PlayerPrefs.GetString("RoomName", RoomName.text);
        UserName.text = PlayerPrefs.GetString("PlayerName", UserName.text);
        //MapsList.captionText.text = PlayerPrefs.GetString("MapName", MapsList.captionText.text);
        //if (MapsList.captionText.text == "Stalingrad")
        //{
        //    MapsList.value = 0;
        //}
        //else
        //    MapsList.value = 1;
    }
    void OnReceivedRoomListUpdate()
    {
        RoomList.text = "";
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();
        foreach (RoomInfo room in rooms)
        {
            RoomList.text += room.name + "          " + room.playerCount + "          " + room.maxPlayers + "\n";
        }
    }
    public void JoinRoom()
    {
        PhotonNetwork.player.name = UserName.text;
        RoomOptions ro = new RoomOptions()
        {
            isVisible = true,
            maxPlayers = 10
        };
        PhotonNetwork.JoinOrCreateRoom(RoomName.text.ToLower(), ro, TypedLobby.Default);
        PlayerPrefs.SetString("PlayerName", UserName.text);
        PlayerPrefs.SetString("RoomName", RoomName.text.ToLower());
        //PlayerPrefs.SetString("MapName", MapsList.captionText.text);
        //if (MapsList.value == 0)
        //    PlayerPrefs.SetInt("Map", 3);
        //else
        //    PlayerPrefs.SetInt("Map", 2);
    }

    void OnJoinedRoom()
    {
     //   UiFunctions.multiPlayer = true;
        if (PhotonNetwork.isMasterClient)
        {
          //  RoomProps["Map"] = PlayerPrefs.GetInt("Map", 3);
            PhotonNetwork.room.SetCustomProperties(RoomProps);
            LoadingImage.gameObject.SetActive(true);
            PhotonNetwork.LoadLevel(1);
           // PhotonNetwork.LoadLevel(PhotonNetwork.room.customProperties["Map"].ToString());
        }
        else
        {
            LoadingImage.gameObject.SetActive(true);
            PhotonNetwork.LoadLevel(1);
            //PhotonNetwork.LoadLevel(PhotonNetwork.room.customProperties["Map"].ToString());
        }
 
    }
}
