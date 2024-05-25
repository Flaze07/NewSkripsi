using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RC.Swimming
{

public class KomodoController : MonoBehaviour
{
    public static KomodoController instance;

    [SerializeField]
    private float swimVerticalVelocity = 1;
    [SerializeField]
    private float maxVerticalVelocity;
    [SerializeField]
    private float verticalVelocity = 0;
    [SerializeField]
    private float downwardAcceleration;

    private Rigidbody2D rb;
    public bool CanSwim { get; set; } = true;

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
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        verticalVelocity -= downwardAcceleration * Time.fixedDeltaTime;
        if(verticalVelocity < -maxVerticalVelocity)
        {
            verticalVelocity = -maxVerticalVelocity;
        }

        rb.MovePosition(rb.position + new Vector2(0, verticalVelocity) * Time.fixedDeltaTime);
    }

    void OnSwim()
    {
        if(CanSwim)
        {
            verticalVelocity = swimVerticalVelocity;
        }
    }
}

}

