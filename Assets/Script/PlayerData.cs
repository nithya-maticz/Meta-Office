using Photon.Pun;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public Transform geometry;
    public Animator charAvatar;
    public Manager1 manager;

    Rigidbody rb;
    PhotonView PV;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        manager = FindAnyObjectByType<Manager1>();
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();

        charAvatar.avatar = manager.selectedAvatar.avatar;
        GameObject mesh = Instantiate(manager.selectedAvatar.mesh.gameObject, geometry);
        mesh.transform.localPosition = Vector3.zero;

        charAvatar.gameObject.SetActive(false);
        charAvatar.gameObject.SetActive(true);
    }
}
