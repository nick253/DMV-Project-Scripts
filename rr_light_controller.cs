using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rr_light_controller : MonoBehaviour
{

    //public GameObject light1;
    //public GameObject light2;
    //public GameObject light3;
    //public GameObject light4;
    //public GameObject light5;

    //public GameObject light6;
    //public GameObject light7;
    //public GameObject light8;
    //public GameObject light9;
    //public GameObject light10;

    public Renderer lightRenderer1;
    public Renderer lightRenderer2;
    public Renderer lightRenderer3;
    public Renderer lightRenderer4;
    public Renderer lightRenderer5;

    public Renderer lightRenderer6;
    public Renderer lightRenderer7;
    public Renderer lightRenderer8;
    public Renderer lightRenderer9;
    public Renderer lightRenderer10;


    public Material redLightOn;
    public Material redLightOff;
    public bool Signaling;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Signaling == true)
        {
            //StartCoroutine(FlashingSignal(.5f));
            //LightsOn();
            SignalFlashing();
        }

    }

    private bool isCoroutineExecuting = false;

    IEnumerator FlashingSignal(float waitTime)
    {

        if (isCoroutineExecuting)
            yield break;

        isCoroutineExecuting = true;
        //LightsOn();
        yield return new WaitForSeconds(waitTime);
        LightsOff();

        // Code to execute after the delay

        isCoroutineExecuting = false;
        //This is a coroutine
        //while (Signaling == true)
        //{
        //    LightsOn();
        //    yield return new
        //    WaitForSeconds(waitTime);    // How many seconds the car will stop at a stop sign
        //    LightsOff();
        //}
    }

    public double timer;
    public bool onOff;

    public void SignalFlashing()
    {
        if (Time.time > timer)
        {
            timer = Time.time + .4;
            onOff = !onOff;
            LightsOn(onOff);

        }
        //StartCoroutine(FlashingSignal(.5f));
    }



    public void LightsOn(bool onOff)
    {
        if (onOff)
        {
            lightRenderer1.material = redLightOn;
            lightRenderer2.material = redLightOn;
            lightRenderer3.material = redLightOn;
            lightRenderer4.material = redLightOn;
            lightRenderer5.material = redLightOn;
            lightRenderer6.material = redLightOn;
            lightRenderer7.material = redLightOn;
            lightRenderer8.material = redLightOn;
            lightRenderer9.material = redLightOn;
            lightRenderer10.material = redLightOn;
        }
        else
        {
            lightRenderer1.material = redLightOff;
            lightRenderer2.material = redLightOff;
            lightRenderer3.material = redLightOff;
            lightRenderer4.material = redLightOff;
            lightRenderer5.material = redLightOff;
            lightRenderer6.material = redLightOff;
            lightRenderer7.material = redLightOff;
            lightRenderer8.material = redLightOff;
            lightRenderer9.material = redLightOff;
            lightRenderer10.material = redLightOff;
        }
    }

    public void LightsOff()
    {
        lightRenderer1.material = redLightOff;
        lightRenderer2.material = redLightOff;
        lightRenderer3.material = redLightOff;
        lightRenderer4.material = redLightOff;
        lightRenderer5.material = redLightOff;
        lightRenderer6.material = redLightOff;
        lightRenderer7.material = redLightOff;
        lightRenderer8.material = redLightOff;
        lightRenderer9.material = redLightOff;
        lightRenderer10.material = redLightOff;
    }
}
