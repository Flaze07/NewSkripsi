using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC.Hunting
{

public class ChangeAjagButton : MonoBehaviour
{
    [SerializeField]
    private AjagController ajag;

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
