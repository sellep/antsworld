using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionAreaProperty
{

    private static GameObject _Property;
    private static ConstructionAreaBuilding _Current;

    private static ProgressBehavior _Progress;
    private static Text _Builder;

    public static bool IsActiveFor(ConstructionAreaBuilding building)
    {
        if (!PropertyManager.IsActive(_Property))
            return false;

        return _Current == building;
    }

    public static void Activate(ConstructionAreaBuilding ca)
    {
        _Current = ca;
        InternalUpdateFarm(ca);
        PropertyManager.MakeActive(_Property);
    }

    public static void UpdateStats(ConstructionAreaBuilding ca)
    {
        if (!PropertyManager.IsActive(_Property))
            return;

        InternalUpdateFarm(ca);
    }

    private static void InternalUpdateFarm(ConstructionAreaBuilding ca)
    {
        _Progress.Value = ca.BuildingPercentage;
        _Builder.text = ca.BuilderAnts.ToString();
    }

    public static void Load()
    {
        if (_Property != null)
            return;

        _Property = UnityEngine.Object.Instantiate(
            (GameObject)Resources.Load("UI/property.construction_area", typeof(GameObject)),
            GameObject.Find("UI").transform);
        _Property.name = "Property.ConstructionArea";
        _Property.SetActive(false);

        _Progress = _Property.transform.Find("Progress").GetComponent<ProgressBehavior>();
        _Builder = _Property.transform.Find("Stats.BuilderAnts/Current").GetComponent<Text>();
    }
}