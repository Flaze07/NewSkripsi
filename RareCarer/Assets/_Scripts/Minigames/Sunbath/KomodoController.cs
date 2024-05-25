using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RC.Sunbath
{

public class KomodoController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private Rigidbody2D rb;

    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var alteredSpeed = moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + moveInput * alteredSpeed);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}

}

