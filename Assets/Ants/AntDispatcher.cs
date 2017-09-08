using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class AntDispatcher
{
    private static readonly object _Sync = new object();

    private static int _AvailableBuilderAnts = 10;
    private static int _AvailableWorkerAnts = 10;
    private static int _AvailableCarrierAnts = 1;

    private static Dictionary<GameObject, int> _CarrierRequester = new Dictionary<GameObject, int>();

    public static void RequestBuilder(ConstructionAreaBuilding ca, int count = 3)
    {
        for (int i = 0; i < count; i++)
        {
            CreateBuilderAnt(ca.transform, null);
        }
    }

    public static void RequestWorker(FarmBuilding farm, int count)
    {
        for (int i = 0; i < count; i++)
        {
            CreateWorkerAnt(farm.transform, null);
        }
    }

    public static void RequestCarrier(GameObject go)
    {
        bool direct = false;

        lock (_Sync)
        {
            if (_AvailableCarrierAnts > 0)
            {
                _AvailableCarrierAnts--;
                direct = true;
            }
            else
            {
                _CarrierRequester.SafelyIncrement(go);
            }
        }

        if (direct)
        {
            CreateCarrierAnt(go.transform);
        }
    }

    public static void ProvideCarrier(CarrierAnt ant)
    {
        GameObject go;
        int requests;

        lock (_Sync)
        {
            _CarrierRequester.MaxOrDefault(out go, out requests);

            if (go == null || requests == 0)
            {
                _AvailableCarrierAnts++;

                Destroy(ant);
            }
            else
            {
                _CarrierRequester.SafelyDecrement(go);

                ant.Carry = Bearables.None;
                ant.Target = go.transform;
            }
        }
    }

    public static void ToBase(AntBase ant)
    {
        ant.Target = null;
    }

    public static void ToBase(Transform t, int count)
    {
        for (int i = 0; i < count; i++)
        {
            CreateBuilderAnt(null, t);
        }
    }

    private static void CreateCarrierAnt(Transform target)
    {
        GameObject go = new GameObject("Carrier", typeof(CarrierAnt));

        CarrierAnt ant = go.GetComponent<CarrierAnt>();
        ant.Target = target;
    }

    private static void CreateBuilderAnt(Transform target, Transform source)
    {
        GameObject go = new GameObject("Builder", typeof(BuilderAnt));

        BuilderAnt ant = go.GetComponent<BuilderAnt>();
        ant.Target = target;
        ant.Source = source;
    }

    private static void CreateWorkerAnt(Transform target, Transform source)
    {
        GameObject go = new GameObject("Worker", typeof(WorkerAnt));

        WorkerAnt ant = go.GetComponent<WorkerAnt>();
        ant.Target = target;
        ant.Source = source;
    }

    /*
     * Going to be private.
     * 
     * Only AntDispatcher decides about living or dying.
     */
    [Obsolete]
    public static void Destroy(AntBase ant)
    {
        UnityEngine.Object.Destroy(ant.gameObject);
    }
}