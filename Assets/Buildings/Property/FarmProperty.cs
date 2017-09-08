using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public static class FarmProperty
{

    private static GameObject _Property;

    private static ProgressBehavior _Progress;
    private static Text _Worker;
    private static Text _Food;

    public static void Activate(FarmBuilding farm)
    {
        EnsurePrefab();

        InternalUpdateStats(farm);
        PropertyManager.MakeActive(_Property);
    }

    public static void UpdateStats(FarmBuilding farm)
    {
        EnsurePrefab();

        if (!PropertyManager.IsActive(_Property))
            return;

        InternalUpdateStats(farm);
    }

    private static void InternalUpdateStats(FarmBuilding farm)
    {
        _Progress.Value = farm.Percentage;
        _Worker.text = farm.WorkerAnts + "/" + farm.MaxWorkerAnts;
        _Food.text = farm.Food.ToString();
    }

    private static void EnsurePrefab()
    {
        if (_Property != null)
            return;

        _Property = UnityEngine.Object.Instantiate(
            (GameObject)Resources.Load("UI/property.farm", typeof(GameObject)),
            GameObject.Find("UI").transform);
        _Property.name = "Property.Farm";
        _Property.SetActive(false);

        _Progress = _Property.transform.Find("Progress").GetComponent<ProgressBehavior>();
        _Worker = _Property.transform.Find("Stats.WorkerAnts/Current").GetComponent<Text>();
        _Food = _Property.transform.Find("Stats.Food/Current").GetComponent<Text>();
    }
}