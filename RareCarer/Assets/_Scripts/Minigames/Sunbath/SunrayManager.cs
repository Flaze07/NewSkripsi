using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RC.Sunbath
{

public class SunrayManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> sunrayPrefab;
    [SerializeField]
    private float minSpeed;
    [SerializeField]
    private float maxSpeed;
    
    private List<Sunray> sunrays = new();

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Random.Range(0f, 1f) < spawningChance(sunrays.Count))
        {
            var randomLoc = RandomPosition();
            var sunray = Instantiate(sunrayPrefab[Random.Range(0,sunrayPrefab.Count)], new Vector3(randomLoc.x, randomLoc.y, -2), Quaternion.identity).GetComponent<Sunray>();
            var randomScale = Random.Range(1.3f, 2.1f);
            sunray.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            sunrays.Add(sunray);
            sunray.HandleDisappear += HandleSunrayDisappear;
            sunray.speed = Random.Range(minSpeed, maxSpeed);
                sunray.transform.eulerAngles = new Vector3(0, 0, Random.Range(0f, 359f));
        }
    }

    private Vector2 RandomPosition()
    {
        var x = Random.Range(spriteRenderer.bounds.min.x, spriteRenderer.bounds.max.x);
        var y = Random.Range(spriteRenderer.bounds.min.y, spriteRenderer.bounds.max.y);
        return new Vector2(x, y);
    }

    private void HandleSunrayDisappear(int instanceID)
    {
        sunrays.RemoveAll(sunray => sunray.GetInstanceID() == instanceID);
    }


    /// <summary>
    /// The chance for each sunray to spawn in the next frame.
    /// </summary>
    ///<param name="x">The number of sunrays currently in the scene.</param>
    /// <remarks>
    /// The chance is calculated based on the number of sunrays currently in the scene.
    /// The formula is 2 - (2.713^(0.13 * x^1.2)), where x is the number of sunrays.
    /// </remarks>
    private float spawningChance(float x)
    {
        // var temp = Mathf.Pow(x, 1.2f);
        // var temp2 = temp * 0.13f;
        // var temp3 = Mathf.Pow(2.713f, temp2);
        // return 2 - temp3;
        return 1 /(Mathf.Pow(x, 6f) + 1);
    }
}

}
