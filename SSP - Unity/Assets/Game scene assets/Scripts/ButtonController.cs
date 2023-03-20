using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonController : XRBaseInteractable
{
    /// <summary>
    /// When the button is pressed the event activate
    /// </summary>
    public UnityEvent OnPress = null;

    private float yMin = 0.0f;
    private float yMax = 0.0f;
    private bool previousPress = false;

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
        handHeight = GetLocalYPosition(hoverEnter.interactorObject.transform.position);
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
        SetYPosition(yMax);
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
        yMin = transform.localPosition.y - (collider.bounds.size.y * 0.25f);
        yMax = transform.localPosition.y;
    }

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="updatePhase"></param>
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (interactor)
        {
            float newHandHeigth = GetLocalYPosition(interactor.transform.position);
            float handDifference = handHeight - newHandHeigth;
            handHeight = newHandHeigth;

            float newPosition = transform.localPosition.y - handDifference;
            SetYPosition(newPosition);

            CheckPress();
        }
    }

    //public override void ProcessInteractor(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    //{
    //    if (interactor)
    //    {
    //        float newHandHeigth = GetLocalYPosition(interactor.transform.position);
    //        float handDifference = handHeight - newHandHeigth;
    //        handHeight = newHandHeigth;

    //        float newPosition = transform.localPosition.y - handDifference;
    //        SetYPosition(newPosition);

    //        CheckPress();
    //    }
    //}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    private float GetLocalYPosition(Vector3 position)
    {
        Vector3 localPosition = transform.root.InverseTransformPoint(position);
        return localPosition.y;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    private void SetYPosition(float position)
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.y = Mathf.Clamp(position, yMin, yMax);
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
        float inRange = Mathf.Clamp(transform.localPosition.y, yMin, yMin + 0.01f);

        return transform.localPosition.y == inRange;
    }
}
