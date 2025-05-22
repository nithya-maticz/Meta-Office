using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEditor.VersionControl;
public class Chat : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TMP_InputField inputField;
    public GameObject message;
    public GameObject content;
    public void SendMessage()
    {
        GetComponent<PhotonView>().RPC("GetMessage", RpcTarget.All, (PhotonNetwork.NickName +" : " + inputField.text));
        inputField.text = "";
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [PunRPC]
    public void GetMessage(string ReceiveMessage)
    {
        Debug.Log("MESSSSSS" + ReceiveMessage);
        GameObject M = Instantiate(message, Vector3.zero, Quaternion.identity, content.transform);
        M.GetComponent<MsgChat>().message.text= ReceiveMessage; 
    }
}
