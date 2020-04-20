using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;

public class AICarStop : MonoBehaviour
{

    public rr_light_controller signalLightController;
    public splineMove aiCar;

    public int fullSpeed = 20;
    public int slowSpeed = 5;

    public TrainController train;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (train.hasFinished == true)
        {
            StartCoroutine(ResumeTravel(2));
        }
    }

    void OnTriggerEnter(Collider collision)
    {

        if (signalLightController.Signaling == true)
        {
            if (collision.gameObject.CompareTag("AICarStop"))
            {
                StartCoroutine(SlowToStop(2));
            }
        }
    }
    IEnumerator SlowToStop(int timeToSlow)
    {
        StartCoroutine(CarDeccelerate());
        yield return new WaitForSeconds(timeToSlow);
        aiCar.Pause();
    }

    IEnumerator ResumeTravel (int timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        aiCar.Resume();
        StartCoroutine(CarAccelerate());
    }


    IEnumerator CarDeccelerate()
    {
        if (aiCar.speed > slowSpeed)
        {
            aiCar.ChangeSpeed(aiCar.speed - 1);
            yield return new WaitForSeconds(.5f);
            StartCoroutine(CarDeccelerate());
        }
        else
        {
            StopCoroutine(CarDeccelerate());
            //StopAllCoroutines();
        }
    }

    IEnumerator CarAccelerate()
    {
        if (aiCar.speed < fullSpeed)
        {
            aiCar.ChangeSpeed(aiCar.speed + 1);
            yield return new WaitForSeconds(.5f);
            StartCoroutine(CarAccelerate());
        }
        else
        {
            StopCoroutine(CarAccelerate());
            //StopAllCoroutines();
        }
    }
}
