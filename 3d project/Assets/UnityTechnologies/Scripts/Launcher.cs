using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq; 
using ExitGames.Client.Photon;
public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;
    public TMP_InputField RoomNameInput;
    public TMP_InputField InputField;
    public TMP_Text RoomName;
    public TMP_Text errorText;
    public Transform PlayerListContent;
    public GameObject PlayerListPrefab;
    public Transform roomListContent;
    public GameObject roomListItemPrefab;
    public GameObject startMenuButton;
    public GameObject Ready;
    public GameObject JohnLemon;
    public TMP_InputField inputroom;
    public TMP_Text JoinFailed;
    GameObject[] Players;
    int count=0;
    
    bool hasLeft = false;
    private void Awake(){
        Instance=this;
    }
    public void Start()
    {
        MenuManager.Instance.OpenMenu("Loading_menu");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("ConnectedtoMaster");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        if(!hasLeft)
        MenuManager.Instance.OpenMenu("Username");
        
    }
    public void EnterUsername(){
        if(!string.IsNullOrEmpty(InputField.text)){

        
        MenuManager.Instance.OpenMenu("Title_Menu");
        }
    }
    public void Create_Room(){
        MenuManager.Instance.OpenMenu("Create Room");

    }
    public void Create()
    {
        if(!string.IsNullOrEmpty(RoomNameInput.text)){

        
        MenuManager.Instance.OpenMenu("Loading_menu");
        PhotonNetwork.CreateRoom(RoomNameInput.text);
        }
    }
    public override void OnCreateRoomFailed(short returnCode,string message){
        MenuManager.Instance.OpenMenu("Error");
        errorText.text="Room Creation Failed"+ message;
    }
    public void Back()
    {
        MenuManager.Instance.OpenMenu("Title_Menu");
    }
    public void Backc(){
        MenuManager.Instance.OpenMenu("Create Room");
    }
    public override void OnJoinedRoom()
    {
        startMenuButton.SetActive(false);
        PhotonNetwork.AutomaticallySyncScene=true;
        MenuManager.Instance.OpenMenu("Room Menu");
        RoomName.text = PhotonNetwork.CurrentRoom.Name;

        Player[] player = PhotonNetwork.PlayerList;
        count =player.Count();
        foreach(Transform child in PlayerListContent)
        {
            Destroy(child.gameObject);
        }
        for(int i =0; i< player.Count(); i++)
        {
            Instantiate(PlayerListPrefab, PlayerListContent).GetComponent<PlayerListItem>().SetUp(player[i]);
        }
        
        //startMenuButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnPlayerEnteredRoom(Player player){
        Instantiate(PlayerListPrefab,PlayerListContent).GetComponent<PlayerListItem>().SetUp(player);
        count=count+1;

    }
    /*public void ready(){
        //Players = GameObject.FindGameObjectsWithTag("Player");
        //Debug.Log(Players.Length);
        //PhotonView view=GetComponent<PhotonView>();
        //if (view== null) Debug.Log("yessssssssss");
        PlayerReady.Instance.onclick();
        StartGameIfAllPlayersReady();
    }*/
    /*public void StartGameIfAllPlayersReady()
    {
        
        // Get all PlayerReady components in the scene
        PlayerReady[] players = FindObjectsOfType<PlayerReady>();
        Debug.Log(players.All(player => player.isReady));
        // Check if all players are ready
        if (players.All(player => player.isReady))
        {
            // Start the game
          startMenuButton.SetActive(PhotonNetwork.IsMasterClient);
        }
    }*/
   
    /*public void ready(){
        clen=clen+1;
        Ready.SetActive(false);
        all_ready();
    }
    public void all_ready(){
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Player");
        int c=objectsWithTag.Length;
        Debug.Log(count);
        if (clen==count){
            
            startMenuButton.SetActive(PhotonNetwork.IsMasterClient);
        }
    }*/
    
public void OnClickReady()
    {
        if (PhotonNetwork.IsConnected)
        {
            // Set the custom property to indicate the player is ready
            Player localPlayer = PhotonNetwork.LocalPlayer;
            ExitGames.Client.Photon.Hashtable playerProps = new ExitGames.Client.Photon.Hashtable() { { "IsReady", true } };
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerProps);
            
            
        }
        
        //Ready.SetActive(false);
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps){
        StartGameIfAllPlayersReady();
    }

    public void StartGameIfAllPlayersReady()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // Get all players in the room
            Player[] players = PhotonNetwork.PlayerList;
            
            // Check if all players are ready
            bool allPlayersReady = CheckAllPlayersReady(players);

            if (allPlayersReady)
            {
                // Start the game (replace this with your game start logic)
                Debug.Log("All players are ready. Starting the game!");
                startMenuButton.SetActive(PhotonNetwork.IsMasterClient);
            }
            else
            {
                Debug.Log("Not all players are ready.");
            }
        }
    }

    private bool CheckAllPlayersReady(Player[] players)
    {
        foreach (Player player in players)
        {
            
            if (!player.CustomProperties.ContainsKey("IsReady") || !(bool)player.CustomProperties["IsReady"])
            {
                Debug.Log("yes");
                // Player is not ready
                return false;
            }
        }
        Debug.Log("out");

        // All players are ready
        return true;
    }





    public override void OnMasterClientSwitched(Player newMasterClient){
    startMenuButton.SetActive(PhotonNetwork.IsMasterClient);
 }
    public void JoinRoom(RoomInfo info)
 {
     PhotonNetwork.JoinRoom(info.Name);
     MenuManager.Instance.OpenMenu("Loading_menu");
 }
public void join(){
    
     if(!string.IsNullOrEmpty(inputroom.text)){
        
    PhotonNetwork.JoinRoom(inputroom.text);
     MenuManager.Instance.OpenMenu("Loading_menu");
}
       
}
public override void OnJoinRoomFailed(short returnCode, string message)
{
    //if (message=="Game does not exist"){
    MenuManager.Instance.OpenMenu("ResultNotFound");
        
    JoinFailed.text="Join Room Failed:\n"+message;
}

 public override void OnLeftRoom()
 {
    hasLeft = true;
    MenuManager.Instance.OpenMenu("Title_Menu");
    Debug.Log("Left");
 }
 public void openfind()
 {
    MenuManager.Instance.OpenMenu("Find Menu");
 }

 public override void OnRoomListUpdate(List<RoomInfo> roomList)
 {
    foreach(Transform trans in roomListContent)
    {
        Destroy(trans.gameObject);
    }

    for (int i = 0; i < roomList.Count; i++)
    {
        if (roomList[i].RemovedFromList)
            continue;
        Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
    }
}
 
public void StartGame()
{
    
    PhotonNetwork.LoadLevel(2);
    
}

public void LeaveRoom()
{

    PhotonNetwork.LeaveRoom();
    MenuManager.Instance.OpenMenu("Loading_menu");
}
    
public void Quit(){
    Application.Quit();
    }
}

