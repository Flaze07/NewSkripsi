using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RC.Hunting
{

public class HuntingManager : MonoBehaviour
{
    public static HuntingManager instance;
    [SerializeField]
    private float jumpForce;
    public float JumpForce => jumpForce;
    [SerializeField]
    private AjagController mainAjag;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
    }

    void OnJump2()
    {
        mainAjag.Jump();
    }

    void OnCrouch(InputValue value)
    {
        if(value.isPressed)
        {
            mainAjag.Crouch();
        }
        else
        {
            mainAjag.Stand();
        }
    }
}

}

