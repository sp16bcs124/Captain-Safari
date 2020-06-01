using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public class PlayerNNetworkMover : Photon.MonoBehaviour
{
    public delegate void Respawn(float time);
    public event Respawn RespawnMe;
    public delegate void SendMessage(string MessageOverlay);
    public event SendMessage SendNetworkMessage;

    [TooltipAttribute("Object with animation component to animate.")]
    public GameObject objectWithAnims;
    private Animator AnimationComponent;
    private InputControl Inputs;
    private FPSRigidBodyWalker FpsWalker;
    private Ironsights IronSightsComponent;
    private TPSPlayer Tps;

    Vector3 position;
    Quaternion rotation;
    float smoothing = 10f;

    bool GameOver = false;
    bool once = false;


    ExitGames.Client.Photon.Hashtable ItemsPickProp;
    // Use this for initialization
    void Start()
    {
        //   Fps.SendMessage("Die");
        ItemsPickProp = new ExitGames.Client.Photon.Hashtable();
        if (photonView.isMine)
        {
            PhotonNetwork.player.AddScore(0);
           // objectWithAnims.gameObject.SetActive(false);
            transform.Find("root").gameObject.SetActive(true);
            GetComponent<PlayerCharacter>().enabled = true;
            FindObjectOfType<bl_MiniMap>().m_Target = GameObject.FindGameObjectWithTag("Player").transform.gameObject;
            //Inputs = GameObject.Find("TPS Player").GetComponent<InputControl>();
            //Tps = GameObject.Find("TPS Player").GetComponent<TPSPlayer>();
           // Tps.hitPoints = 100;
           // health = Tps.hitPoints;
            //FpsWalker = GameObject.Find("TPS Player").GetComponent<FPSRigidBodyWalker>();
            //IronSightsComponent = GameObject.Find("TPS Player").GetComponent<Ironsights>();
            //GameObject.Find("NPC Manager").GetComponent<NPCRegistry>().enabled = true;
            //GameObject.Find("Object Pooling Manager").GetComponent<AzuObjectPool>().enabled = true;
            //HealthText = GameObject.Find("HealthText").GetComponent<Text>();
            //healthimage = GameObject.Find("Health").GetComponent<Image>();
        }
        //else
        //{
        //    GetComponent<PlayerCharacter>().enabled = true;
        //   // StartCoroutine("UpdateData");
        //}

    }
    //[PunRPC]
    //public void UpdateScore(string name)
    //{
    //    for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
    //    {
    //        if (PhotonNetwork.playerList[i].name == name)
    //        {
    //            int ItemsPick = int.Parse(PhotonNetwork.playerList[i].customProperties["ItemsPick"].ToString());
    //            ItemsPick = ItemsPick + 1;
    //            Debug.Log(ItemsPick);
    //            Debug.Log(name);
    //            ItemsPickProp["ItemsPick"] = ItemsPick;
    //            PhotonNetwork.playerList[i].SetCustomProperties(ItemsPickProp);
    //        }
    //    }
    //}
    //IEnumerator UpdateData()
    //{
    //    while (true)
    //    {
    //        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * smoothing);
    //        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * smoothing);

    //        yield return null;
    //    }
    //}
    //void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.isWriting)
    //    {
    //        stream.SendNext(transform.position);
    //        stream.SendNext(transform.rotation);
    //       // stream.SendNext(AnimationComponent);
    //        //stream.SendNext(health);
    //    }
    //    else
    //    {
    //        position = (Vector3)stream.ReceiveNext();
    //        rotation = (Quaternion)stream.ReceiveNext();

    //        //health = (float)stream.ReceiveNext();
    //    }
    //}

    void RemovePlayer()
    {
        PhotonNetwork.Destroy(gameObject);
    }
    // Update is called once per frame
    //void Update()
    //{
    //    if (photonView.isMine && !GameOver)
    //    {
    //        //if (Tps != null)
    //        //{
    //        //    float temp = Tps.hitPoints;
    //        //    healthimage.fillAmount = temp / 100f;
    //        //    HealthText.text = Mathf.Floor(temp).ToString();
    //        //}
    //    }
    //}
}
