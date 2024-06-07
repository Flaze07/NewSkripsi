using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC.Hunting
{

public class AjagController : MonoBehaviour
{
    [SerializeField]
    private GameObject standing;
    [SerializeField]
    private GameObject crouching;
    private Rigidbody2D rb;
    public bool crouchState;
    [SerializeField]
    private CheckGround checkGround;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        if(checkGround.IsGrounded)
        {
            rb.AddForce(new Vector2(0, HuntingManager.instance.JumpForce), ForceMode2D.Force);
        }
    }

    void Update()
    {
        if(checkGround.IsGrounded)
        {
            if(crouchState)
            {
                crouching.SetActive(true);
                standing.SetActive(false);
            }
        }
        if(!crouchState || !checkGround.IsGrounded)
        {
            crouching.SetActive(false);
            standing.SetActive(true);
        }
    }

    public void Crouch()
    {
        // standing.SetActive(false);
        // crouching.SetActive(true); 
        crouchState = true;
    }

    public void Stand()
    {
        crouchState = false;
        // standing.SetActive(true);
        // crouching.SetActive(false);
    }
    
}

}

