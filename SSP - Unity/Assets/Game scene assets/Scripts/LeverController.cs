using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverController : MonoBehaviour
{


    public Quaternion StartRotation;

    public UnityEvent LeverDown;
    public UnityEvent LeverUp;

    private HingeJoint joint;

    private bool down = false;

    private void Start()
    {
        joint = GetComponent<HingeJoint>();
        Debug.Log(joint.transform.rotation.z);
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(joint.transform.rotation.z);

        if (transform.rotation.y >= 3)
        {
            //Debug.Log(Joint.transform.rotation.z);
        }
    }
}
