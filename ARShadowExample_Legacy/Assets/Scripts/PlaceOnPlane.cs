using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : MonoBehaviour
{
    #region Exposed Parameter
    [Header("Object Placed")]
    [SerializeField] protected GameObject m_PlacedPrefab;
    #endregion

    #region Private Prameter
    private GameObject m_SpawnedObject;
    public GameObject SpawnedObject
    {
        get
        {
            if (m_SpawnedObject)
            {
                return m_SpawnedObject;
            }
            else
            {
                return null;
            }
        }
        set
        {
            m_SpawnedObject = value;
        }
    }

    private ARRaycastManager m_RaycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    #endregion

    #region Monobehavior Functions
    private void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (!TouchPos(out Vector2 pos)) return;
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return;

        if(m_RaycastManager.Raycast(pos, hits, TrackableType.PlaneWithinBounds))
        {
            var hitPos = hits[0].pose;
            if(m_SpawnedObject == null)
            {
                m_SpawnedObject = Instantiate(m_PlacedPrefab, hitPos.position, Quaternion.identity);
            }
            else
            {
                m_SpawnedObject.transform.position = hitPos.position;
            }
        }
    }
    #endregion

    bool TouchPos(out Vector2 pos)
    {
        if(Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            pos = touch.position;
            return true;
        }
        pos = default;
        return false;
    }

    public void DestroySpawnedObject()
    {
        if (m_SpawnedObject)
        {
            Destroy(m_SpawnedObject);
        }
        else
        {
            return;
        }
    }
}
