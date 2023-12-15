using UnityEngine;
using TMPro;
using Photon.Pun;

public class Collectible : MonoBehaviourPun
{
    public TMP_Text text;
    static int Count;

    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Update()
    {
            transform.localRotation = Quaternion.Euler(90f, Time.time * 100f, 0);
    }

     void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Increment the Count variable locally for the player
            Count++;
            Debug.Log(Count);
           photonView.RPC("DestroyCollectibleRPC", RpcTarget.All);
            // if (photonView.IsMine)
            // {
            // // Destroy the object for the owner and all others
            //     PhotonNetwork.Destroy(gameObject);
            // }
            // else
            // {
            // // If not the owner, locally destroy the object without network synchronization
            //     Destroy(gameObject);
            // }
            display(Count);
        }
    }
    void display(int Count){
            text.text=Count.ToString();
    }
    
    [PunRPC]
    void DestroyCollectibleRPC()
    {
        // Destroy the collectible on all clients
        Destroy(gameObject);
        }
}
