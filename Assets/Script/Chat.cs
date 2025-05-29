using UnityEngine;
using Photon.Pun;
using TMPro;

public class Chat : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TMP_InputField inputField;
    public GameObject myMessage;
    public GameObject othersMessage;
    public GameObject content;
    PhotonView PV;
    //public Manager Manager;
    public void SendMessage()
    {
        

        GetComponent<PhotonView>().RPC("GetMessage", RpcTarget.All, (PhotonNetwork.NickName +" : " + inputField.text));
        inputField.text = "";
    }
    void Start()
    {
        PV = GetComponent<PhotonView>();
        print("Name : " + myMessage.name + " " + othersMessage.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [PunRPC]
    public void GetMessage(string ReceiveMessage)
    {
        Debug.Log("MESSSSSS" + ReceiveMessage);
        if (PV.IsMine)
        {

            GameObject M = Instantiate(myMessage, Vector3.zero, Quaternion.identity, content.transform);
            M.GetComponent<MsgChat>().message.text = ReceiveMessage;

        }
        else
        {
            Debug.Log("Receive Message : " + ReceiveMessage);
            GameObject M1 = Instantiate(othersMessage, Vector3.zero, Quaternion.identity, content.transform);
            M1.GetComponent<MsgChat>().message.text = ReceiveMessage;

        }
    }

    

}
