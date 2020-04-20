using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;

public class TrainController : MonoBehaviour
{

    public splineMove train;
    public PathChange car;
    public bool hasFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartTrain ()
    {
        train.StartMove();
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "TrainEndTrigger")
        {
            hasFinished = true;

            // Here I am telling the car to continue forward because the train has finished.
            car.CallCarGo();
        }
    }


    }
