using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("max" + spriteRenderer.bounds.max);
        Debug.Log("min" + spriteRenderer.bounds.min);
        Debug.Log("Size" + spriteRenderer.bounds.size.y);

        Vector2 distanceVector = spriteRenderer.bounds.min - spriteRenderer.bounds.max;
        var distanceMagnitude = distanceVector.magnitude;
        Debug.Log("Distance" + distanceMagnitude);
    } 
}
