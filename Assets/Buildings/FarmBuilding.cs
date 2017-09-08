using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class FarmBuilding : NetworkBehaviour
{
    public readonly object _Sync = new object();

    public float FarmingSpeed = 0.01f;
    public int MaxWorkerAnts = 3;
    public int WorkerAnts = 0;
    public float Percentage = 0;
    public int Food = 0;

    void Start()
    {
        AntDispatcher.RequestWorker(this, MaxWorkerAnts);
    }

    void Update()
    {
        if (WorkerAnts == 0)
            return;

        Percentage += WorkerAnts * Time.deltaTime * FarmingSpeed;

        if (Percentage >= 1)
        {
            Percentage--;
            Food++;

            AntDispatcher.RequestCarrier(gameObject);
        }

        FarmProperty.UpdateStats(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        WorkerAnt worker = collision.gameObject.GetComponent<WorkerAnt>();
        if (worker != null)
        {
            HandleIncomingWorker(worker);
            return;
        }

        CarrierAnt carrier = collision.gameObject.GetComponent<CarrierAnt>();
        if (carrier != null)
        {
            HandleIncomingCarrier(carrier);
            return;
        }
    }

    private void HandleIncomingCarrier(CarrierAnt carrier)
    {
        if (carrier.Target != transform)
            return;

        lock (_Sync)
        {
            if (Food > 0)
            {
                carrier.Carry = Bearables.Food;
                Food--;
            }
        }

        carrier.Target = null;
    }

    private void HandleIncomingWorker(WorkerAnt worker)
    {
        if (worker.Target != transform)
            return;

        lock (_Sync)
        {
            if (WorkerAnts == MaxWorkerAnts)
            {
                AntDispatcher.ToBase(worker);
            }
            else
            {
                WorkerAnts++;
                AntDispatcher.Destroy(worker);
            }
        }
    }

    protected void OnMouseUp()
    {
        FarmProperty.Activate(this);
    }
}
