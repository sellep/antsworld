using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class PropertyManager
{

    private static GameObject _Current = null;

    public static bool IsActive(GameObject property)
    {
        return _Current == property;
    }

    public static void MakeActive(GameObject property)
    {
        if (_Current == null)
        {
            property.SetActive(true);
            _Current = property;
        }
        else if (_Current != property)
        {
            _Current.SetActive(false);
            property.SetActive(true);
            _Current = property;
        }
    }

    public static void OpenFor(GameObject go)
    {
        FarmBuilding farm = go.GetComponent<FarmBuilding>();
        if (farm != null)
        {
            FarmProperty.Activate(farm);
        }
    }
}