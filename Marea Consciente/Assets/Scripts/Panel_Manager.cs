using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PanelDisplayManager : MonoBehaviour
{
    [Header("Canvases de Información")]
    public List<GameObject> farCanvases;
    public List<GameObject> nearCanvases;

    [Header("Configuración de Distancia")]
    public Transform playerTransform;
    public float farDistanceThreshold = 30.0f;
    public float nearDistanceThreshold = 15.0f;

    [Header("Transición")]
    public float fadeDuration = 2.0f; // Duración del fade en segundos

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

        if (farCanvases == null || farCanvases.Count == 0 || nearCanvases == null || nearCanvases.Count == 0)
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
                StartCoroutine(FadeCanvasGroupList(farCanvases, false));
                StartCoroutine(FadeCanvasGroupList(nearCanvases, true));
                isNear = true;
            }
        }
        else
        {
            if (isNear || distanceToPlayer > farDistanceThreshold)
            {
                StartCoroutine(FadeCanvasGroupList(farCanvases, true));
                StartCoroutine(FadeCanvasGroupList(nearCanvases, false));
                isNear = false;
            }
        }
    }

    IEnumerator FadeCanvasGroupList(List<GameObject> canvasList, bool fadeIn)
    {
        foreach (GameObject canvas in canvasList)
        {
            if (canvas == null) continue;

            CanvasGroup cg = canvas.GetComponent<CanvasGroup>();
            if (cg == null)
            {
                Debug.LogWarning("El canvas no tiene un componente CanvasGroup: " + canvas.name);
                continue;
            }

            float startAlpha = fadeIn ? 0f : 1f;
            float endAlpha = fadeIn ? 1f : 0f;

            canvas.SetActive(true); // Asegura que esté activo antes del fade

            float elapsed = 0f;
            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / fadeDuration;
                cg.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
                yield return null;
            }

            cg.alpha = endAlpha;

            // Solo desactivar el objeto si es fadeOut
            if (!fadeIn)
                canvas.SetActive(false);
        }
    }
}
