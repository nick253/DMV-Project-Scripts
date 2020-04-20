using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLights : MonoBehaviour
{
    public Renderer[] greenLightMain;
    public Renderer[] yellowLightMain;
    public Renderer[] redLightMain;

    public Renderer[] greenLightMainAlternate;
    public Renderer[] yellowLightMainAlternate;
    public Renderer[] redLightMainAlternate;

    public Material redLightOn;
    public Material redLightOff;

    public Material greenLightOn;
    public Material greenLightOff;

    public Material yellowLightOn;
    public Material yellowLightOff;

    public bool stopSignal;

    // Start is called before the first frame update
    void Start()
    {
        GreenLightOn();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartRedLight(float redLightTime)
    {
        StartCoroutine(RedLightOn(redLightTime));
    }

    IEnumerator RedLightOn(float redLightTime)
    {
        YellowLightOn();
        yield return new WaitForSeconds(3f);
        RedLightOn();
        yield return new WaitForSeconds(redLightTime);
        GreenLightOn();
    }

    IEnumerator RedLightOnAlternate(float redLightTime)
    {
        YellowLightOn_Alt();
        yield return new WaitForSeconds(3f);
        RedLightOn_Alt();
        yield return new WaitForSeconds(redLightTime);
        GreenLightOn_Alt();
    }

    public void GreenLightOn()
    {
        for (int i = 0; i < greenLightMain.Length; i++)
        {
            greenLightMain[i].material = greenLightOn;
        }
        for (int i = 0; i < yellowLightMain.Length; i++)
        {
            yellowLightMain[i].material = yellowLightOff;
        }
        for (int i = 0; i < redLightMain.Length; i++)
        {
            redLightMain[i].material = redLightOff;
        }
    }

    public void YellowLightOn()
    {
        for (int i = 0; i < greenLightMain.Length; i++)
        {
            greenLightMain[i].material = greenLightOff;
        }
        for (int i = 0; i < yellowLightMain.Length; i++)
        {
            yellowLightMain[i].material = yellowLightOn;
        }
        for (int i = 0; i < redLightMain.Length; i++)
        {
            redLightMain[i].material = redLightOff;
        }
    }

    public void RedLightOn()
    {
        for (int i = 0; i < greenLightMain.Length; i++)
        {
            greenLightMain[i].material = greenLightOff;
        }
        for (int i = 0; i < yellowLightMain.Length; i++)
        {
            yellowLightMain[i].material = yellowLightOff;
        }
        for (int i = 0; i < redLightMain.Length; i++)
        {
            redLightMain[i].material = redLightOn;
        }
    }

    public void GreenLightOn_Alt()
    {
        for (int i = 0; i < greenLightMainAlternate.Length; i++)
        {
            greenLightMainAlternate[i].material = greenLightOn;
        }
        for (int i = 0; i < yellowLightMainAlternate.Length; i++)
        {
            yellowLightMainAlternate[i].material = yellowLightOff;
        }
        for (int i = 0; i < redLightMainAlternate.Length; i++)
        {
            redLightMainAlternate[i].material = redLightOff;
        }
    }

    public void YellowLightOn_Alt()
    {
        for (int i = 0; i < greenLightMainAlternate.Length; i++)
        {
            greenLightMainAlternate[i].material = greenLightOff;
        }
        for (int i = 0; i < yellowLightMainAlternate.Length; i++)
        {
            yellowLightMainAlternate[i].material = yellowLightOn;
        }
        for (int i = 0; i < redLightMainAlternate.Length; i++)
        {
            redLightMainAlternate[i].material = redLightOff;
        }
    }

    public void RedLightOn_Alt()
    {
        for (int i = 0; i < greenLightMainAlternate.Length; i++)
        {
            greenLightMainAlternate[i].material = greenLightOff;
        }
        for (int i = 0; i < yellowLightMainAlternate.Length; i++)
        {
            yellowLightMainAlternate[i].material = yellowLightOff;
        }
        for (int i = 0; i < redLightMainAlternate.Length; i++)
        {
            redLightMainAlternate[i].material = redLightOn;
        }
    }
}
