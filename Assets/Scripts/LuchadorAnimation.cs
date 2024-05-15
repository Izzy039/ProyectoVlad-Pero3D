using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuchadorAnimation : MonoBehaviour
{
    public Animator animator;
    public bool isMoving;
    public bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>() ;
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
        bool jumpPressed = Input.GetKey(KeyCode.Space);

        if (forwardPressed)
        {
            animator.SetBool("isMoving", true);
            isMoving = true;
        }
        else
        {
            animator.SetBool("isMoving", false);
            isMoving = false;
        }

        if (jumpPressed)
        {
            animator.SetBool("isJumping", true);
            isJumping = true;

        }
        if (!jumpPressed)
        {
            animator.SetBool("isJumping", false);
            isJumping = false;
        }
    }
}
