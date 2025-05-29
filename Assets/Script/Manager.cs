using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Collections.Generic;

using System.IO;
using UnityEngine.UI;
using Unity.VisualScripting;
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
    [SerializeField] GameObject chatMenu;
    public Manager1 managerref1;
    public GameObject bgImage;
    public GameObject cameras;
    public GameObject canvas;
    public GameObject canvasGame;
    public GameObject chatButton;
    public RoomInfo info;
    [SerializeField] TMP_Text hostName;
    public string nickName;
    [SerializeField] TMP_Text roomCodeText;
    public string currentRoomCode;
    [SerializeField] TMP_InputField roomCodeInput1;
    public AudioSource bgSound;
    public GameObject muteBtn;
    
    public Button myBut;
    public Sprite muteImage;
    public Sprite unMuteImage;
    public Image butImage;
    public AudioSource clickSound;
    public bool mute;
    public bool muteMick;
    public Button myMike;
    public Sprite muteMickImage;
    public Sprite unMuteMickImage;
    public Image muteMikeImage;
    public GameObject voiceRef;
    public GameObject CanvasRef;





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
        clickSound.Play();
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
      //  nickName.text = PhotonNetwork.NickName;
        print("NAME    " + PhotonNetwork.NickName);
        nickName = PhotonNetwork.NickName;
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
        PhotonNetwork.CreateRoom(roomnameInputField.text + Random.Range(1000, 9999));
        roomnameInputField.text = "";
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnJoinedRoom()
    {
        print("onJoinedRoom");
      
        MenuManager.Instance.OpenMenu("room");
       // roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        LobbyDetails();

        string fullRoomName = PhotonNetwork.CurrentRoom.Name;
        string trimmedRoomName = fullRoomName.Length > 4
            ? fullRoomName.Substring(0, fullRoomName.Length - 4)
            : fullRoomName;

        Debug.Log(fullRoomName+ "      "+trimmedRoomName);
        roomNameText.text = trimmedRoomName;
       
        print("Room Name : " + PhotonNetwork.CurrentRoom.Name);

        string fullRoomcode = PhotonNetwork.CurrentRoom.Name;
        string lastFour = fullRoomName.Length >= 4
            ? fullRoomName.Substring(fullRoomName.Length - 4)
            : fullRoomName;

        roomCodeText.text = "Room Code: " + lastFour;
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        LobbyDetails();
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("Error");
        errorText.text = "Room Creation Failed" + message;
        MenuManager.Instance.OpenMenu("error");
    }
    public void CallStartGame()
    {

        photonView.RPC("StartGame",RpcTarget.All);
    }
    [PunRPC]
    void StartGame()
    {
      //  bgImage.GetComponent<Image>.Disable(false);
        MenuManager.Instance.OpenMenu("loading");
        cameras.SetActive(false);
        canvas.SetActive(false);
        canvasGame.SetActive(true);
        bgSound.Play();
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
        // PhotonNetwork.LoadLevel(1);
    }
    public void LeaveRoom()
    {
        CanvasRef.SetActive(true);
        bgSound.volume = 0;
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

    public void LobbyDetails()
    {
        Player[] players = PhotonNetwork.PlayerList;
        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }

        for (i = 0; i < players.Length; i++)
        {
            if (players[i] == PhotonNetwork.MasterClient)
            {
                print("Host Name : " + players[i].NickName);
                hostName.text = players[i].NickName;
            }
            else
            {
                Debug.Log("Not Master Client");
                Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
            }
        }
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public void JoinFun()
    {
        if(roomCodeInput1.text==currentRoomCode)
        {
            JoinRoom(info);
        }
    }

    public void MuteFun()
    {
        butImage = myBut.GetComponent<Image>();
       

        if (mute==false)
        {
            butImage.sprite = muteImage;
            mute = true;
            bgSound.volume = 0;
           

        }
        else
        {
            butImage.sprite = unMuteImage;
            mute = false;
            bgSound.volume = 1;
           
        }
       
    }
    
   
   
    public void MuteMickFun()
    {
       
        muteMikeImage = myMike.GetComponent<Image>();

        if (muteMick == false)
        {
            muteMikeImage.sprite = muteMickImage;
            muteMick = true;
            voiceRef.SetActive(false);
            Debug.Log("false");
           // bgSound.volume = 0;
        }
        else
        {
            muteMikeImage.sprite = unMuteMickImage;
            muteMick = false;
            voiceRef.SetActive(true);
            Debug.Log("true");

        }
    }
    public void Reset()
    {
        bgSound.volume = 1;
        muteMick = false;
        mute = false;


    }
}
