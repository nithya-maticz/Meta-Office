using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using NUnit.Framework;
using System.Collections.Generic;

public class Manager : MonoBehaviourPunCallbacks
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] TMP_InputField roomnameInputField;
    [SerializeField] TMP_InputField playernameInputField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject playerListItemPrefab;
    [SerializeField] GameObject startGameButton;
    [SerializeField] GameObject errorMenu;
    public Manager1 managerref1;

    public static Manager Instance;
    private int i;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {


    }

    public void playGame()
    {
        print("inside playgae");
         if (string.IsNullOrEmpty(playernameInputField.text))
         {
            return;
         }

        PhotonNetwork.ConnectUsingSettings();
        MenuManager.Instance.OpenMenu("loading");
        print("Connecting");
    }

    public override void OnConnectedToMaster()
    {
        //base.OnConnectedToMaster();
        print("Connected");
        errorMenu.SetActive(false);
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene=true;

    }

    public override void OnJoinedLobby()
    {

        print("Joined");

        print("OnJoined Lobby");
        MenuManager.Instance.OpenMenu("avatar");
        // MenuManager.Instance.OpenMenu("tittle");
        managerref1.AvatarUIData();
        //  PhotonNetwork.NickName = "Player" + Random.Range(0, 1000).ToString("0000");
        PhotonNetwork.NickName = playernameInputField.text.ToString();
        print("NAME    " + PhotonNetwork.NickName);



    }

   
    // Update is called once per frame
    void Update()
    {

    }

    public  void AvatarSelect()
    {
        Debug.Log("avatar Select");
        MenuManager.Instance.OpenMenu("tittle");
    }

    public override void OnCreatedRoom()
    {
        print("on created Room");
        if (string.IsNullOrEmpty(roomnameInputField.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomnameInputField.text);

        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnJoinedRoom()
    {
        print("onJoinedRoom");
        MenuManager.Instance.OpenMenu("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        Player[] players = PhotonNetwork.PlayerList;
        foreach(Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }
        for (i = 0; i < players.Length; i++)
        {
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);

        }
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("Error");
        errorText.text = "Room Creation Failed" + message;
        MenuManager.Instance.OpenMenu("error");
    }
    public void StartGame()
    {
        MenuManager.Instance.OpenMenu("loading");
        PhotonNetwork.LoadLevel(1);
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }
    public void JoinRoom(RoomInfo info)
    {
        print("JoinRoom");
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("loading");
    }
    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("tittle");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        print("OnRoomListUpdate");
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            print("Room ID : " + i);
            if (roomList[i].RemovedFromList)
            continue;
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        print("OnPlayerEnteredRoom");
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        errorText.text = "DisConnected " + cause;
        PhotonNetwork.Reconnect();
        errorMenu.SetActive(true);
    }

    public override void OnConnected()
    {
        Debug.Log("OnConnect");
        errorMenu.SetActive(false);
    }


}
