using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public static class BaseProperty
{
    private static GameObject _Property;

    private static ProgressBehavior _ProgressBar;
    private static Slider _FreeSlider;
    private static Text _FreeCurrent;
    private static Slider _BuilderSlider;
    private static Text _BuilderCurrent;

    public static void Activate(BaseBuilding b)
    {
        UpdateStats(b);
        PropertyManager.MakeActive(_Property);
    }

    public static void Load()
    {
        if (_Property != null)
            return;

        //GameObject prefab = Resources.Load<GameObject>("UI/property.base");
        //Transform parent = GameObject.Find("UI").transform;

        //_Property = UnityEngine.Object.Instantiate(prefab, parent);
        //_Property.name = "Property.Base";
        //_Property.SetActive(false);

        //_ProgressBar = _Property.transform.Find("Progress").GetComponent<ProgressBehavior>();
        //_FreeCurrent = _Property.transform.Find("Free/Current").GetComponent<Text>();
        //_FreeSlider = _Property.transform.Find("Free/Slider").GetComponent<Slider>();
        //_BuilderCurrent = _Property.transform.Find("Builder/Current").GetComponent<Text>();
        //_BuilderSlider = _Property.transform.Find("Builder/Slider").GetComponent<Slider>();

        //_BuilderSlider.onValueChanged.AddListener(OnBuilderSliderMove);
    }

    public static void UpdateStats(BaseBuilding b)
    {
        _FreeCurrent.text = (b.TotalAnts - b.BuilderAnts - b.WorkerAnts - b.CarrierAnts).ToString();
        _FreeSlider.maxValue = b.TotalAnts;
        _FreeSlider.value = b.FreeAnts;
        _FreeSlider.enabled = false;

        _BuilderCurrent.text = b.BuilderAnts.ToString();
        _BuilderSlider.maxValue = b.FreeAnts;
        _BuilderSlider.value = b.BuilderAnts;

        //transform.Find("Stats.SpawnSpeed/Current").GetComponent<Text>().text = SpawnSpeed.ToString() + "x";
        //transform.Find("Stats.SpawnSpeed/Upgrade").GetComponent<Text>().text = (SpawnSpeed + 0.125f).ToString() + "x";
        //transform.Find("Stats.AntSpeed/Current").GetComponent<Text>().text = AntSpeed.ToString() + "x";
        //transform.Find("Stats.AntSpeed/Upgrade").GetComponent<Text>().text = (AntSpeed + 0.125f).ToString() + "x";
    }

    /*private static void OnBuilderSliderMove(float val)
    {
        int free = TotalAnts - (int)_BuilderSlider.value;

        _FreeCurrent.text = free.ToString();
        _FreeSlider.value = free;

        _BuilderCurrent.text = ((int)_BuilderSlider.value).ToString();
    }*/
}