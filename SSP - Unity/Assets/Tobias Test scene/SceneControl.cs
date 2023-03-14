using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

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
            UpdateInteractor(0.02f, false);
        }
    }

    public void SceneChangeWithIndex(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }

    public void SceneChangeWithName(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

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