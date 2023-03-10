using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{



    public void ItemGrabed()
    {
        Collider collider= GetComponent<Collider>();
        collider.enabled = false;
        Debug.Log("Grabed");
    }

    public void ItemDroped()
    {
        Collider collider = GetComponent<Collider>();
        collider.enabled = true;
        Debug.Log("Droped");
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            Debug.Log("entered Water");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
