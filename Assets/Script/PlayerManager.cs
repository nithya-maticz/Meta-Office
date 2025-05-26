using UnityEngine;
using Photon.Pun;
using System.IO;


public class PlayerManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    PhotonView PV;
    

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
       // Debug.Log("Gamemanager set: " + GameManager.Instance.spawnPoint);
        // PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), GameManager.Instance.spawnPoint.position, Quaternion.identity);
       GameObject player =  PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), spawnPoint.position, Quaternion.identity);
       // FindAnyObjectByType<Manager1>().players.Add(player.GetComponent<PlayerController>());
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
