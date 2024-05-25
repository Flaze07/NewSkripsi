using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC.Swimming
{
public class SkyTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerInner"))
        {
            KomodoController.instance.CanSwim = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerInner"))
        {
            KomodoController.instance.CanSwim = true;
        }
    }
}
}
