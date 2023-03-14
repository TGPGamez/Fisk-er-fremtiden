using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonController : XRBaseInteractable
{
    /// <summary>
    /// When the button is pressed the event activete
    /// </summary>
    public UnityEvent OnPress = null;

    //
    private float zMin = 0.0f;
    private float zMax = 0.0f;
    private bool previousPress = false;

    //
    private float handHeight = 0.0f;
    
    //XRBaseIntertactor is a interactor like a controller
    private XRBaseInteractor interactor = null;

    /// <summary>
    /// 
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        hoverEntered.AddListener(StartPress);
        hoverExited.AddListener(EndPress);
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnDestroy()
    {
        hoverEntered.RemoveListener(StartPress);
        hoverExited.RemoveListener(EndPress);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hoverEnter"></param>
    private void StartPress(HoverEnterEventArgs hoverEnter)
    {
        interactor = (XRBaseInteractor)hoverEnter.interactorObject;
        handHeight = GetLocalZPosition(hoverEnter.interactorObject.transform.position);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hoverExit"></param>
    private void EndPress(HoverExitEventArgs hoverExit)
    {
        interactor = null;
        handHeight = 0.0f;

        previousPress = false;
        SetZPosition(zMax);
    }
    
    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        SetMinMax();
    }

    /// <summary>
    /// 
    /// </summary>
    private void SetMinMax()
    {
        Collider collider = GetComponent<Collider>();
        zMin = transform.localPosition.z - (collider.bounds.size.z * 0.25f);
        zMax = transform.localPosition.z;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="updatePhase"></param>
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (interactor)
        {
            float newHandHeigth = GetLocalZPosition(interactor.transform.position);
            float handDifference = handHeight - newHandHeigth;
            handHeight = newHandHeigth;

            float newPosition = transform.localPosition.z - handDifference;
            SetZPosition(newPosition);

            CheckPress();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    private float GetLocalZPosition(Vector3 position)
    {
        Vector3 localPosition = transform.root.InverseTransformPoint(position);
        return localPosition.z;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    private void SetZPosition(float position)
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.z = Mathf.Clamp(position, zMin, zMax);
        transform.localPosition = newPosition;
    }

    /// <summary>
    /// 
    /// </summary>
    private void CheckPress()
    {
        bool inPosition = InPosition();

        if (inPosition && inPosition != previousPress)
        {
            Debug.Log("Button Pressed");
            OnPress.Invoke();
        }

        previousPress = inPosition;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private bool InPosition()
    {
        float inRange = Mathf.Clamp(transform.localPosition.z, zMin, zMin + 0.01f);

        return transform.localPosition.z == inRange;
    }
}
