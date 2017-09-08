using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//[RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D))]
public class ConstructionAreaBuilding : NetworkBehaviour
{
    public const float BUILDING_TIME_SECS = 100;
    public float BuildingTimeSecs = 0;
    public float BuildingPercentage = 0;

    public GameObject BuildingPrefab;
    public int BuilderAnts = 0;

    void Start()
    {
        ConstructionAreaProperty.Load();

        AntDispatcher.RequestBuilder(this, 3);
    }

    void Update()
    {
        if (BuilderAnts == 0)
            return;

        BuildingTimeSecs += BuilderAnts * Time.deltaTime;
        BuildingPercentage = BuildingTimeSecs / BUILDING_TIME_SECS;

        if (BuildingPercentage >= 1)
        {
            SetupNewBuilding();
        }
        else
        {
            ConstructionAreaProperty.UpdateStats(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BuilderAnt ant = collision.gameObject.GetComponent<BuilderAnt>();

        if (ant == null)
            return;

        if (ant.Target != transform)
            return;

        BuilderAnts++;

        AntDispatcher.Destroy(ant);

        ConstructionAreaProperty.UpdateStats(this);
    }

    protected void OnMouseUp()
    {
        ConstructionAreaProperty.Activate(this);
    }

    private void SetupNewBuilding()
    {
        GameObject go = Instantiate(BuildingPrefab, transform.parent);
        go.transform.position = transform.position;

        FarmBuilding farm = go.GetComponent<FarmBuilding>();
        if (farm != null)
        {
            farm.enabled = true;
        }

        if (ConstructionAreaProperty.IsActiveFor(this))
        {
            PropertyManager.OpenFor(go);
        }

        Destroy(gameObject);

        AntDispatcher.ToBase(transform, BuilderAnts);
    }
}
