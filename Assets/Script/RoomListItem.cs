using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomListItem : MonoBehaviour
{
    public TMP_Text text;
  
    public RoomInfo info;
    public string roomCode;



    public void SetUp(RoomInfo _info)
    {
        Debug.Log("Room Name : " + _info.Name);
        info = _info;

        // text.text = _info.Name;
        
        string fullRoomName = _info.Name;
        string trimmedRoomName = fullRoomName.Length > 4
             ? fullRoomName.Substring(0, fullRoomName.Length - 4)
             : fullRoomName;

        text.text = trimmedRoomName;

        string fullRoomcode = _info.Name;
        string lastFour = fullRoomName.Length >= 4
             ? fullRoomName.Substring(fullRoomName.Length - 4)
             : fullRoomName;

        roomCode = lastFour;
    }

    
    public void onClick()
    {
        Manager.Instance.info = info;
        FindAnyObjectByType<MenuManager>().OpenMenu("codemenu");
        Manager.Instance.currentRoomCode = roomCode;
    }
    
    
}