using UnityEngine;

public class billboard : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Camera cam;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cam==null)
        {
            cam = FindObjectOfType<Camera>();
        }
        if(cam ==null)
        {
            return;
        }
        transform.LookAt(cam.transform);
        transform.Rotate(Vector3.up * 180);
    }
}
