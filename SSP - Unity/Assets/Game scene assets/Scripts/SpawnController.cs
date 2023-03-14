using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

    public GameObject SpawnObject;
    public Vector3 offsetPos;
    public Quaternion rotation;

    public void Spawn()
    {
        Vector3 pos = transform.position;
        pos = pos + offsetPos;
        Instantiate(SpawnObject, pos, rotation);
    }

    public void debugTest()
    {
        Debug.Log("Tester");
    }
}
