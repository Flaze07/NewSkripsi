using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

namespace RC.Swinging
{

public class Rope : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D hook;
    [SerializeField]
    private GameObject[] prefabRopeSegs;
    [SerializeField]
    private int numLinks = 5;
    // Start is called before the first frame update
    void Start()
    {
        GenerateRope();
    }

    private void GenerateRope()
    {
        Rigidbody2D prevBod = hook;
        for(int i = 0; i < numLinks; ++i)
        {
            int idx = Random.Range(0, prefabRopeSegs.Length);
            GameObject newSeg = Instantiate(prefabRopeSegs[idx]);
            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;
            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = prevBod;

            if(prevBod != hook)
            {
                hj.limits = new JointAngleLimits2D
                {
                    min = -60,
                    max = 60
                };

                hj.useLimits = true;           
            }
    
            prevBod = newSeg.GetComponent<Rigidbody2D>();
        }

    }
}

}

