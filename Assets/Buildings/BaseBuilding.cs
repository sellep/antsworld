using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(NetworkTransform))]
public class BaseBuilding : NetworkBehaviour
{
    public const int INIT_ANTS = 3;

    public float SpawnSpeed = 0.25f;
    public float AntSpeed = 1f;
    public float CurrentAntSpawn = 0;

    public int TotalAnts = INIT_ANTS;
    public int FreeAnts = INIT_ANTS;
    public int BuilderAnts = 0;
    public int WorkerAnts = 0;
    public int CarrierAnts = 0;

    void Start()
    {
        BaseProperty.Load();
    }

    void Update()
    {
        CurrentAntSpawn += Time.deltaTime * SpawnSpeed;

        if (CurrentAntSpawn >= 1)
        {
            FreeAnts++;
            TotalAnts++;

            //_FreeSlider.maxValue++;
            //_FreeSlider.value++;
            //_FreeCurrent.text = (_FreeSlider.value).ToString();

            //_BuilderSlider.maxValue++;

            CurrentAntSpawn -= 1;
        }

        //if (_ProgressBar != null)
        //{
        //    _ProgressBar.Value = CurrentAntSpawn;
        //}
    }

    protected void OnMouseUp()
    {
        BaseProperty.Activate(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BuilderAnt builder = collision.gameObject.GetComponent<BuilderAnt>();
        if (builder != null)
        {
            HandleIncomingBuilder(builder);
            return;
        }

        CarrierAnt carrier = collision.gameObject.GetComponent<CarrierAnt>();
        if (carrier != null)
        {
            HandleIncomingCarrier(carrier);
            return;
        }
    }

    private void HandleIncomingBuilder(BuilderAnt builder)
    {
        if (builder.Target == transform || builder.Target == null)
        {
            /*
             * The AntDispatcher.Destroy call is obsolete
             * 
             * Implement:
             * AntDispatcher.ProvideBuilder(builder);
             * 
             * This method cares about sending builder to next construction or destroys the go.
             */
            AntDispatcher.Destroy(builder);
        }
    }

    private void HandleIncomingCarrier(CarrierAnt carrier)
    {
        if (carrier.Target == transform || carrier.Target == null)
        {
            AntDispatcher.ProvideCarrier(carrier);
        }
    }
}
