using UnityEngine;
using Photon.Pun;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    PhotonView PV;
    public GameObject refSpawnTransform;
    public Transform spawnTransform;
   

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    void Start()
    {
        Debug.Log("START");
        if(PV.IsMine)
        {
            CreateController();
        }
    }


    void CreateController()
    {
        Transform spawnPoint = SpawnManager.Instance.GetSpawnpoint();
        Debug.Log("Spawn New Character " );
        Debug.Log("Gamemanager set: " + GameManager.Instance.spawnPoint);
        // PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), GameManager.Instance.spawnPoint.position, Quaternion.identity);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), spawnPoint.position, Quaternion.identity);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
