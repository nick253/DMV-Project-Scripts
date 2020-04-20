using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;

public class BusController : MonoBehaviour
{

    public splineMove bus;
    public splineMove car;
    public bool isStopped = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider collision)
    {
        //Make sure we are colliding with a BusTrigger.
        if (collision.gameObject.tag == "BusStop")
        {
            StartCoroutine(StopBus());
        }
    }

    IEnumerator StopBus()
    {
        bus.Pause();
        isStopped = true;
        // enable the bus stop sign
        yield return new WaitForSeconds(8f);
        //disable the bus stop sign
        bus.Resume();
        isStopped = false;
    }
}
