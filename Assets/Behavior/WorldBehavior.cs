using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class WorldBehavior : MonoBehaviour
{
    public Transform Layer;
    public Vector2? Position;

    private void Awake()
    {
        Layer = GameObject.Find("Layer").transform;
    }

    private void Start()
    {
        GameObject go = new GameObject("New", typeof(BuildingPlacingBehavior));
        go.GetComponent<BuildingPlacingBehavior>().Building = Buildings.Base;

        if (!NetworkClient.active)
        {
            Debug.Log("Network inactive!");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Position = null;
        }
        else if (Position.HasValue)
        {
            TranslateWorld();
        }
    }

    private void TranslateWorld()
    {
        Vector2 now = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 diff = Position.Value - now;

        if (diff == Vector2.zero)
            return;

        Vector3 @new = Layer.localPosition - (Vector3)diff;
        @new.z = Layer.localPosition.z;
        Layer.localPosition = @new;
        Position = now;
    }
}
