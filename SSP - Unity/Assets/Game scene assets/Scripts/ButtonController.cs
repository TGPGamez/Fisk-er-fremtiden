using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    public SpawnController Spawner;

    // Start is called before the first frame update
    void Start()
    {
        Spawner.Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
