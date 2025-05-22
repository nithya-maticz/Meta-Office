using UnityEngine;
using Photon.Pun;
using TMPro;

public class UserName : MonoBehaviour
{
    [SerializeField] PhotonView PV;
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject imageRef;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // if (!PV.IsMine)
          //  return;
        text.text = PV.Owner.NickName;
        if (PV.IsMine)
        {
            print("NOTTTTT");
            imageRef.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
