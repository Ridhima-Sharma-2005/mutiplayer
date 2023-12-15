using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class spawn : MonoBehaviourPunCallbacks
{
    public GameObject JohnLemon;
    public GameObject Image;
   
    void Start()
    {
     PhotonNetwork.Instantiate(JohnLemon.name,new Vector3(Random.Range(-12f,-7f),0.073f,Random.Range(-5f,-1f)),Quaternion.identity);
    }
    public void quit(){
        Application.Quit();
    }
    public void closechat(){
       if (Image.activeSelf){
        Image.SetActive(false);
       }
       else Image.SetActive(true);

    }
   
}
