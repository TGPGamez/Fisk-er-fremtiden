using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.Composites;

public class VRButton : MonoBehaviour
{
    public GameObject Button;
    public UnityEvent OnPress;
    public UnityEvent OnRelease;
    private GameObject presser;
    private bool ispressed;
    private Vector3 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = Button.transform.localPosition;
        ispressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!ispressed)
        {
            Vector3 temp = originalPosition;
            temp.z = temp.z + 0.05f;
            Button.transform.localPosition = temp;
            presser = other.gameObject;
            OnPress.Invoke();
            ispressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser)
        {
            Button.transform.localPosition = originalPosition;
            OnRelease.Invoke();
            ispressed = false;
        }
    }

}
