using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC.Hunting
{

public class CheckGround : MonoBehaviour
{
    public bool isGrounded;
    public bool IsGrounded => isGrounded;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}

}

