using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC.Swinging3
{

public class SwingingManager : MonoBehaviour
{
    public static SwingingManager instance;

    [SerializeField]
    private float gravityValue;

    public float GravityValue
    {
        get
        {
            return gravityValue;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

}

