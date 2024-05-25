using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RC.Sunbath
{

    public class WallSunrayTrigger : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Sunray"))
            {
                other.gameObject.GetComponent<Sunray>().SetDeathChance();
            }
        }
    }

}

