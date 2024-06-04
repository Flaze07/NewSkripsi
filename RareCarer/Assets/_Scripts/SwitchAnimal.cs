using System.Collections;
using System.Collections.Generic;
using RC;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchAnimal : MonoBehaviour
{
    [SerializeField]
    private Transform endpointLeft;
    [SerializeField]
    private Transform endpointRight;

    void Start()
    {
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) =>
        {
            FindEndpoint();
        };
    }

    private void FindEndpoint()
    {
        var gb = GameObject.Find("EndpointLeft");
        if (gb != null)
        {
            endpointLeft = gb.transform;
        }
        gb = GameObject.Find("EndpointRight");
        if (gb != null)
        {
            endpointRight = gb.transform;
        }
    }

    /// <summary>
    /// Switch the current animal with the next animal
    /// direction is determined by int with the value of 1 and -1
    /// </summary>
    public void Switch(int direction, Animal current, Animal next)
    {
        Transform target = direction == 1 ? endpointRight : endpointLeft;
        StartCoroutine(AnimateSwitch(target, current, next));
    }

    private IEnumerator AnimateSwitch(Transform target, Animal current, Animal next)
    {
        // next.Show();
        float duration = 1f;
        float time = 0;
        Vector3 start = current.transform.position;
        Vector3 end = target.position;
        
        Vector3 nextStart;
        if (target == endpointRight)
        {
            nextStart = endpointLeft.position;
        }
        else
        {
            nextStart = endpointRight.position;
        }
        Vector3 nextEnd = current.transform.position;

        while (time < duration)
        {
            time += Time.deltaTime;
            current.transform.position = Vector3.Lerp(start, end, time / duration);
            next.transform.position = Vector3.Lerp(nextStart, nextEnd, time / duration);
            yield return null;
        }

        GameManager.instance.IsSwitching = false;
        // current.Hide();
    }
}
