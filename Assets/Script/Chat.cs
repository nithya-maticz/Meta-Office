using UnityEngine;

using Photon.Pun;

using TMPro;

public class Chat : MonoBehaviour

{

    public TMP_InputField inputField;

    public GameObject myMessage;        // Prefab for your own message

    public GameObject othersMessage;    // Prefab for others' messages

    public GameObject content;          // Scroll content holder

    PhotonView PV;

    void Start()

    {

        PV = GetComponent<PhotonView>();

    }

    public void SendMessage()

    {

        if (!string.IsNullOrEmpty(inputField.text))

        {

            // Format: senderID|nickname: message

            string fullMessage = PhotonNetwork.LocalPlayer.ActorNumber + "|" + PhotonNetwork.NickName + " : " + inputField.text;

            PV.RPC("GetMessage", RpcTarget.All, fullMessage);

            inputField.text = "";

        }

    }

    [PunRPC]

    public void GetMessage(string rawMessage, PhotonMessageInfo info)

    {

        Debug.Log("Received Raw Msg: " + rawMessage);

        // Parse: "senderID|message"

        string[] parts = rawMessage.Split('|');

        //if (parts.Length < 2)

        //{

        //    Debug.LogWarning("Malformed message received.");

        //    return;

        //}

        int senderId = int.Parse(parts[0]);

        string messageContent = parts[1];
        Debug.Log(senderId);
        Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber);

        GameObject prefabToUse = (senderId == PhotonNetwork.LocalPlayer.ActorNumber)

            ? myMessage

            : othersMessage;

        GameObject messageObj = Instantiate(prefabToUse, Vector3.zero, Quaternion.identity, content.transform);

        MsgChat msgChat = messageObj.GetComponent<MsgChat>();

        if (msgChat != null && msgChat.message != null)

        {

            msgChat.message.text = messageContent;

        }

        else

        {

            Debug.LogError("MsgChat or message field is not assigned properly in prefab.");

        }

    }

}

