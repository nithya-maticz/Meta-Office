using UnityEngine;

public class wave : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Animator animatorRef;
    void Start()
    {
        animatorRef = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("t"))
        {
            animatorRef.Play("wave");
        }
    }
}
