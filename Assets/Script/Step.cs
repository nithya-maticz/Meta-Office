
using UnityEngine;

public class Step : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform spawnPoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("collider");
        if (collision.CompareTag("player"))
        {
            collision.transform.position = spawnPoint.position;
        }
    }
}
