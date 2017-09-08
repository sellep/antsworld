using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PropertyCloseBehavior : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject go = GameObject.FindGameObjectWithTag("property");
        if (go != null)
        {
            go.SetActive(false);
        }
    }
}
