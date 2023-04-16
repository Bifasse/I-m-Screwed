using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateUI : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Canvas canvas;
    private Vector2 originalPos;
    private float wait=0.5f;

    RectTransform m_RectTransform;

    // Start is called before the first frame update
    private void Start()
    {
        //canvasGroup = GetComponent<CanvasGroup>();
        m_RectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        canvasGroup.alpha = 0;
        wait = 0.5f;
    }

    private void Update()
    {
        if (canvasGroup.alpha < 1)
            canvasGroup.alpha += Time.deltaTime*4;
        if(wait >= 0)
            m_RectTransform.transform.Translate(Vector2.up * Time.deltaTime*2);
        wait -= Time.deltaTime;
    }

    private void OnDisable()
    {
        canvasGroup.alpha = 0;
    }

}
