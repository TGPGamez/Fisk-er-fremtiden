using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonController : XRBaseInteractable
{
    public UnityEvent OnPress = null;

    private float zMin = 0.0f;
    private float zMax = 0.0f;
    private bool previousPress = false;

    private float handHeight = 0.0f;
    private XRBaseInteractor interactor = null;

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

    private void StartPress(HoverEnterEventArgs hoverEnter)
    {
        interactor = (XRBaseInteractor)hoverEnter.interactorObject;
        handHeight = GetLocalZPosition(hoverEnter.interactorObject.transform.position);
    }

    private void EndPress(HoverExitEventArgs hoverExit)
    {
        interactor = null;
        handHeight = 0.0f;

        previousPress = false;
        SetZPosition(zMax);
    }

    private void Start()
    {
        SetMinMax();
    }

    private void SetMinMax()
    {
        Collider collider = GetComponent<Collider>();
        zMin = transform.localPosition.z - (collider.bounds.size.z * 0.5f);
        zMax = transform.localPosition.z;
    }

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

    private float GetLocalZPosition(Vector3 position)
    {
        Vector3 localPosition = transform.root.InverseTransformPoint(position);
        return localPosition.z;
    }

    private void SetZPosition(float position)
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.z = Mathf.Clamp(position, zMin, zMax);
        transform.localPosition = newPosition;
    }

    private void CheckPress()
    {
        bool inPosition = InPosition();

        if (inPosition && inPosition != previousPress)
            OnPress.Invoke();

        previousPress = inPosition;
    }

    private bool InPosition()
    {
        float inRange = Mathf.Clamp(transform.localPosition.z, zMin, zMin + 0.01f);

        return transform.localPosition.z == inRange;
    }
}
