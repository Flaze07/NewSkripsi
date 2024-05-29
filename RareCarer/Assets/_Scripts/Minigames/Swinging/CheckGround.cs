using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC.Swinging
{

public class CheckGround : MonoBehaviour
{
    public bool IsGrounded { get; private set; }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            IsGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            IsGrounded = false;
        }
    }
}

}

