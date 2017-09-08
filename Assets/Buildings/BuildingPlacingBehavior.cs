using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class BuildingPlacingBehavior : NetworkBehaviour
{
    private Sprite _SpriteOk;
    private Sprite _SpriteNot;
    private Transform _Buildings;

    public bool Ok = true;
    public Buildings Building;

    private void Awake()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0;

        _SpriteOk = Resources.Load<Sprite>("Sprites/building_place_ok");
        _SpriteNot = Resources.Load<Sprite>("Sprites/building_place_not");
        _Buildings = GameObject.Find("Buildings").transform;
        transform.SetParent(_Buildings);
    }

    private void Start()
    {
        SetOk();

        CircleCollider2D collider = gameObject.GetComponent<CircleCollider2D>();
        collider.radius = 0.3f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SetNotOk();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SetOk();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (Ok)
            {
                GameObject building = CreateBuilding();

                /*
                 * Fully debug
                 */
                if (building.GetComponent<BaseBuilding>() != null)
                {
                    Building = Buildings.Farm;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.localPosition = new Vector3(newPos.x, newPos.y, -1);
        }
    }

    private GameObject CreateBuilding()
    {
        GameObject prefab, building;

        if (Building == Buildings.Farm)
        {
            prefab = Resources.Load<GameObject>("UI/building.construction");
            building = Instantiate(prefab, _Buildings);
            building.GetComponent<ConstructionAreaBuilding>().BuildingPrefab = Resources.Load<GameObject>("UI/building.farm");
        }
        else
        {
            prefab = Resources.Load<GameObject>("UI/building.base");
            building = Instantiate(prefab, _Buildings);
        }

        building.name = prefab.name;

        Vector2 pos = transform.localPosition;
        building.transform.localPosition = new Vector3(pos.x, pos.y, 0);

        return building;
    }

    private void SetOk()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = _SpriteOk;
        Ok = true;
    }

    private void SetNotOk()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = _SpriteNot;
        Ok = false;
    }
}