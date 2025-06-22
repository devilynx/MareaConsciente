using UnityEngine;
using System.Collections.Generic;

public class PanelDisplayManager : MonoBehaviour
{
    [Header("Canvases de Información")]
    public List<GameObject> farCanvases;   // Canvases lejanos (antes era solo uno)
    public List<GameObject> nearCanvases;  // Canvases cercanos

    [Header("Configuración de Distancia")]
    public Transform playerTransform;
    public float farDistanceThreshold = 4.0f;
    public float nearDistanceThreshold = 2.0f;

    private bool isNear = false;

    void Start()
    {
        if (playerTransform == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
                playerTransform = playerObject.transform;
            else
            {
                Debug.LogError("No se encontró el jugador.");
                enabled = false;
                return;
            }
        }

        if (farCanvases == null || farCanvases.Count == 0 ||
            nearCanvases == null || nearCanvases.Count == 0)
        {
            Debug.LogError("Faltan referencias de canvases cercanos o lejanos.");
            enabled = false;
            return;
        }

        UpdatePanelVisibility();
    }

    void Update()
    {
        UpdatePanelVisibility();
    }

    void UpdatePanelVisibility()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= nearDistanceThreshold)
        {
            if (!isNear)
            {
                SetFarCanvasesVisibility(false);
                SetNearCanvasesVisibility(true);
                isNear = true;
            }
        }
        else
        {
            if (isNear || distanceToPlayer > farDistanceThreshold)
            {
                SetFarCanvasesVisibility(true);
                SetNearCanvasesVisibility(false);
                isNear = false;
            }
        }
    }

    void SetFarCanvasesVisibility(bool isVisible)
    {
        foreach (GameObject canvas in farCanvases)
        {
            if (canvas != null)
                canvas.SetActive(isVisible);
        }
    }

    void SetNearCanvasesVisibility(bool isVisible)
    {
        foreach (GameObject canvas in nearCanvases)
        {
            if (canvas != null)
                canvas.SetActive(isVisible);
        }
    }
}
