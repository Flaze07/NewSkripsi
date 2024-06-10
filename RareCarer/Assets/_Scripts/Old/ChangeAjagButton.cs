using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC.Hunting.Old
{

public class ChangeAjagButton : MonoBehaviour
{
    [SerializeField]
    private AjagController ajag;
    [SerializeField]
    private float switchTime;
    void OnMouseDown()
    {
        HuntingManager.instance.ChangeMainAjag(ajag);
    }

    void Update()
    {
        var currentPos = transform.position;
        transform.position = new Vector3(ajag.transform.position.x, currentPos.y, currentPos.z);
    }
}

}
