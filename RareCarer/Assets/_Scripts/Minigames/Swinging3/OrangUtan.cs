using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;
using UnityEngine.UIElements;

namespace RC.Swinging3
{

public class OrangUtan : MonoBehaviour
{
    [SerializeField]
    private float maxRotateSpeed = 30;
    [SerializeField]
    private float rotateAcceleration = 1;
    [SerializeField]
    private float jumpSpeed = 5;
    [SerializeField]
    private float radiusValue = 0.02f;
    private float currentRotateAcceleration;
    private float currentRotateSpeed;
    private bool attached = false;

    private Vector2 velocity;

    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentRotateAcceleration = rotateAcceleration;   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!attached)
        {
            UpdateUnattached();
            return;
        }
        UpdateAttached();
    }

    private void UpdateAttached()
    {
        if(currentRotateAcceleration != rotateAcceleration)
        {
            currentRotateAcceleration = rotateAcceleration;
            transform.rotation = Quaternion.Euler(0, 0, -60);
            currentRotateSpeed = 0;
        }

        if(transform.rotation.z < 0)
        {
            currentRotateSpeed += rotateAcceleration * Time.fixedDeltaTime;
        }
        else if(transform.rotation.z > 0)
        {
            currentRotateSpeed -= rotateAcceleration * Time.fixedDeltaTime;
        }

        // transform.Rotate(new Vector3(0, 0, currentRotateSpeed * Time.deltaTime));
        rb.MoveRotation(rb.rotation + currentRotateSpeed * Time.fixedDeltaTime);
    }

    private void UpdateUnattached()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

        velocity.y -= SwingingManager.instance.GravityValue * Time.fixedDeltaTime;
        
        float rotation;

        if(velocity.x < 0)
        {
            rotation = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg + 180;
        } 
        else
        {
            rotation = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        }

        // rotation = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        rb.MoveRotation(rotation);
    }

    void OnJump()
    {
        attached = false;
        float newVel = currentRotateSpeed * radiusValue;
        var horizontalSpeed = newVel * Mathf.Cos(Mathf.Deg2Rad * rb.rotation);
        var verticalSpeed = newVel * Mathf.Sin(Mathf.Deg2Rad * rb.rotation);

        velocity = new Vector2(horizontalSpeed, verticalSpeed);
    }

    public void Attach(GameObject target)
    {
        var hangPlace = target.transform.Find("HangPlace");
        transform.position = hangPlace.position;
        transform.rotation = Quaternion.Euler(0, 0, -60);
        currentRotateSpeed = 0;
        velocity = Vector2.zero;

        attached = true;
    }
}

}

