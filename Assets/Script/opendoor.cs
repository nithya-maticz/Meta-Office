using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opendoor : MonoBehaviour
{
    public GameObject Instruction;
    public Animator doorOpenAnimation;
    public bool Action = false;

    void Start()
    {
        Instruction.SetActive(false);

    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("collider");
        if (collision.CompareTag("player"))
        {
            Debug.Log("inside collider");
            Instruction.SetActive(true);
            Action = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        Instruction.SetActive(false);
        Action = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("eeeeeeeee");
            if (Action == true)
            {
                Instruction.SetActive(false);
                doorOpenAnimation.SetTrigger("dooropen");
                Action = false;
            }
        }

    }
}
