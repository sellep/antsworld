using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ProgressBehavior : MonoBehaviour
{

    public float Value;

    private RectTransform _OuterRect;
    private RectTransform _InnerRect;

    private void Start()
    {
        _OuterRect = GetComponent<RectTransform>();
        _InnerRect = transform.Find("Bar").GetComponent<RectTransform>();
    }

    private void Update()
    {
        float width = _OuterRect.rect.width;
        float progress = width * Value;
        _InnerRect.anchoredPosition = new Vector2(progress / 2, 0);
        _InnerRect.sizeDelta = new Vector2(progress, _OuterRect.sizeDelta.y);
    }
}
