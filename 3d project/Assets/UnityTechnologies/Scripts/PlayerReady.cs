using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerReady : MonoBehaviourPunCallbacks
{
    
    [SerializeField]
    public bool isReady = false;
    PhotonView photonview;
    public GameObject Playerprefab;
    
    public static PlayerReady Instance;
    public void Awake()
    {
        Instance=this;
    }
    private void Start(){
        GameObject playerInstance = PhotonNetwork.Instantiate(Playerprefab.name, Vector3.zero, Quaternion.identity);
        photonview = playerInstance.GetComponent<PhotonView>();
    }
   
    public void SetReadyStatus(bool ready)
    {
        
        //Players = GameObject.FindGameObjectsWithTag("Player");
        //Debug.Log(Players.Length);
        //foreach(GameObject player in Players)
        //{
        
           
            Debug.Log(photonview);
            if (photonview!=null)
            {
                Debug.Log("set");
                Debug.Log("photonView.Owner: " + photonView.Owner);
                Debug.Log("PhotonNetwork.LocalPlayer: " + PhotonNetwork.LocalPlayer);
                photonview.RPC("RpcSetReadyStatus", RpcTarget.All,ready); 
            }
            else Debug.Log("not");
        //}
       
        
        /*if (photon.IsMine && photon!=null)
        {
            Debug.Log("set");
            photon.RPC("RpcSetReadyStatus", RpcTarget.All,ready);
        }*/
    }
    public void onclick(){
        
        SetReadyStatus(true);
    }
    [PunRPC]
    private void RpcSetReadyStatus(bool ready)
    {
        isReady = ready;

        // You can implement logic here to update UI or perform actions
        // when the ready status of a player changes.
        Debug.Log("Player ready status changed: " + isReady);
    }

    // You might want to add other methods or logic as needed.
}

