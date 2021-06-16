using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SceneController : MonoBehaviour
{
    [SerializeField] protected ARSession m_ARSession;
    [SerializeField] protected PlaceOnPlane m_PlaceOnPlane;

    public void ResetSession()
    {
        m_PlaceOnPlane.DestroySpawnedObject();
        m_ARSession.Reset();
    }
}
