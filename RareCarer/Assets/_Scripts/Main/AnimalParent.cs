using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC
{

public class AnimalParent : MonoBehaviour
{
    public static AnimalParent instance;
    [SerializeField]
    private List<Animal> animals;

    public List<Animal> Animals => animals;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}

}

