using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC.Swinging3
{

public class AttachCollider : MonoBehaviour
{
    private OrangUtan orangUtan;

    void Start()
    {
        orangUtan = GetComponentInParent<OrangUtan>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("SwingObj"))
        {
            orangUtan.Attach(col.gameObject);
        }
    }
}

}
