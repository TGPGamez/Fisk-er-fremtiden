using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Class to change scene and change the ray interactor distance on gameobjects
/// </summary>
public class SceneControl : MonoBehaviour
{
    [SerializeField] private SceneState state;
    [SerializeField] private List<GameObject> interactorObjects;


    private void Start()
    {
        if (state.Equals(SceneState.Menu))
        {
            UpdateInteractor(2f, true);
        } else
        {
            UpdateInteractor(0.06f, false);
        }
    }

    /// <summary>
    /// Load a scene out from index
    /// </summary>
    /// <param name="index">scene id</param>
    public void SceneChangeWithIndex(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }

    /// <summary>
    /// Method to update the ray interactor distance and disable/enable visual
    /// </summary>
    /// <param name="distance">new ray distance</param>
    /// <param name="visual">show visual or not</param>
    private void UpdateInteractor(float distance, bool visual)
    {
        foreach(GameObject interactorGameObject in interactorObjects)
        {
            XRRayInteractor interactor = interactorGameObject.GetComponent<XRRayInteractor>();
            if (interactor != null)
            {
                interactor.maxRaycastDistance = distance;
            }
            XRInteractorLineVisual visualLine = interactorGameObject.GetComponent<XRInteractorLineVisual>();
            if (visualLine != null)
            {
                visualLine.enabled = visual;
            }
        }
    }
}

public enum SceneState
{
    Game,
    Menu
}