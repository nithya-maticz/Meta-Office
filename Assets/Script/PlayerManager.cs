using UnityEngine;
using Photon.Pun;
using System.IO;


public class PlayerManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    PhotonView PV;
    private Manager1 Manager;
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        Manager = FindAnyObjectByType<Manager1>();
        
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

        Debug.Log(Manager.yourAvatarId);
        switch(Manager.yourAvatarId)
        {
            case 1:
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController 1"), spawnPoint.position, spawnPoint.rotation);
                break;
            case 2:
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController 2"), spawnPoint.position, spawnPoint.rotation);
                break;
            case 3:
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController 3"), spawnPoint.position, spawnPoint.rotation);
                break;
            case 4:
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController 4"), spawnPoint.position, spawnPoint.rotation);
                break;
            case 5:
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController 5"), spawnPoint.position, spawnPoint.rotation);
                break;
            case 6:
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController 6"), spawnPoint.position, spawnPoint.rotation);
                break;
            case 7:
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController 7"), spawnPoint.position, spawnPoint.rotation);
                break;


        }

        // Debug.Log("Gamemanager set: " + GameManager.Instance.spawnPoint);
        // PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), GameManager.Instance.spawnPoint.position, Quaternion.identity);

        // GameObject player =  PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), spawnPoint.position, spawnPoint.rotation, Quaternion.identity);
        // FindAnyObjectByType<Manager1>().players.Add(player.GetComponent<PlayerController>());
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
