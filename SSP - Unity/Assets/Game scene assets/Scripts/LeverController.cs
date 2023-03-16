using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class LeverController : MonoBehaviour
{


    [Tooltip("Only Select One")]
    public bool3 axis;
    public float detectionAngel;


    public UnityEvent LeverDown;
    public UnityEvent LeverUp;


    private HingeJoint joint;
    [SerializeField] private float startRotation;

    private bool grabed = false;
    private bool down = false;


    private void Start()
    {
        joint = GetComponent<HingeJoint>();
        
        if (axis.x)
            startRotation = joint.transform.rotation.x;
        else if (axis.y)
            startRotation = joint.transform.rotation.y;
        else if (axis.z)
            startRotation = joint.transform.rotation.z;
        
        if (startRotation <= 0)
        {
            startRotation = math.abs(startRotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (grabed) 
        {
            float rota = 0;

            if (axis.x)
                rota = joint.transform.rotation.x;
            else if (axis.y)
                rota = joint.transform.rotation.y;
            else if (axis.z)
                rota = joint.transform.rotation.z;

            if (rota <= 0)
            {
                rota = math.abs(rota);
            }

            if (rota >= detectionAngel && !down) 
            {
                down = true;
                LeverDown.Invoke();
                Debug.Log("Down");
            }
            else if (rota <= detectionAngel && down)
            {
                down = false;
                LeverUp.Invoke();
                Debug.Log("Up");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Taggen");
        }
    }

    public void LeverGrabed()
    {
        if (!grabed)
        {
            grabed = true;
            Debug.Log("Grabed");
        }
    }

    public void LeverReleased()
    {
        if (grabed)
        {
            grabed = false;
            Debug.Log("Released");
        }
    }
}
