using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RC.Swinging
{

public class OrangUtanController : MonoBehaviour
{
    [SerializeField]
    private float pushForce = 10f;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float jumpForce = 10f;
    [SerializeField]
    private float slideDelay = 0.5f;
    private float slideDelayTimer = 0f;

    public Rigidbody2D rb;
    private HingeJoint2D hj;
    private CheckGround checkGround;

    public bool attached = false;
    private Vector2 moveInput;
    public Transform attachedTo;
    private GameObject disregard;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hj = GetComponent<HingeJoint2D>();
        checkGround = GetComponentInChildren<CheckGround>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(attached)
        {
            HandleMoveAttached(moveInput);
        }
        else
        {
            HandleMoveGround(moveInput);
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void HandleMoveAttached(Vector2 direction)
    {
        var horizontal = direction.x;

        rb.AddRelativeForce(new Vector2(horizontal, 0) * pushForce, ForceMode2D.Force);

        var vertical = direction.y;

        Slide(Mathf.RoundToInt(vertical));
    }

    private void HandleMoveGround(Vector2 direction)
    {
        var horizontal = direction.x;

        rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);
    }

    void OnJump()
    {
        if(attached)
        {
            Detach();
        }
        else
        {
            if(!checkGround.IsGrounded)
            {
                return;
            }
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(!attached)
        {
            if(col.gameObject.tag == "Rope")
            {
                if(attachedTo != col.gameObject.transform.parent)
                {
                    if(disregard == null || col.gameObject.transform.parent.gameObject != disregard)
                    {
                        Attach(col.gameObject.GetComponent<Rigidbody2D>());
                    }
                }
            }
        }
    }

    public void Attach(Rigidbody2D ropeBone)
    {
        ropeBone.gameObject.GetComponent<RopeSegment>().isPlayerAttached = true;
        hj.connectedBody = ropeBone;
        hj.enabled = true;
        attached = true;
        attachedTo = ropeBone.transform.parent;
    }

    void Detach()
    {
        hj.connectedBody.GetComponent<RopeSegment>().isPlayerAttached = false;
        hj.enabled = false;
        attached = false;
        hj.connectedBody = null;
    }

    public void Slide(int direction)
    {
        if(slideDelayTimer > 0)
        {
            slideDelayTimer -= Time.fixedDeltaTime;
            return;
        }
        RopeSegment connection = hj.connectedBody.GetComponent<RopeSegment>();
        GameObject newSeg = null;
        if(direction > 0)
        {
            if(connection.connectedAbove != null)
            {
                if(connection.connectedAbove.GetComponent<RopeSegment>() != null)
                {
                    newSeg = connection.connectedAbove;
                    slideDelayTimer = slideDelay;
                }
            }
        }
        else if(direction < 0)
        {
            if(connection.connectedBelow != null)
            {
                newSeg = connection.connectedBelow;
                slideDelayTimer = slideDelay;
            }
        }
        else
        {
            return;
        }

        if(newSeg != null)
        {
            transform.position = newSeg.transform.position;
            connection.isPlayerAttached = false;
            newSeg.GetComponent<RopeSegment>().isPlayerAttached = true;
            hj.connectedBody = newSeg.GetComponent<Rigidbody2D>();
        }
    }
}

}

