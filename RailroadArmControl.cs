using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailroadArmControl : MonoBehaviour
{
    public float xAngle, yAngle, zAngle;

    // Railraod arm should be rotated down if true
    public bool rotateDown = false;

    // Railroad Crossing Arm
    public GameObject arm;

    // The object whose rotation we want to match.
    public Transform target;
    public Transform targetUp;

    // Angular speed in degrees per sec.
    public float speed;

    public int timer = 0;
    public int timeLimit = 5000;

    public Vector3 rotationTarget;

    void Awake()
    {

    }

    void FixedUpdate()
    {
        // The step size is equal to speed times frame time.
        //var step = speed * Time.deltaTime;

        if (rotateDown == true)
        {
            //if (timer > timeLimit)
            //{
            //    rotateDown = false;
            //}
            //var step = speed * Time.deltaTime;
            //timer = timer + 1;
            // Rotate our transform a step closer to the target's.
            StartCoroutine(ArmRotator2(.2f));


            //arm.transform.localRotation = Quaternion.Euler(-75, 0, 0);
            //arm.transform.localRotation = Quaternion.Euler(-55, 0, 0);
            //arm.transform.localRotation = Quaternion.Euler(-35, 0, 0);
            //arm.transform.localRotation = Quaternion.Euler(-15, 0, 0);//RotateTowards(transform.rotation, target.rotation, step);
            //arm.transform.localRotation = Quaternion.Euler(-1, 0, 0);
        }

        if  (rotateDown == false)
        {
            StartCoroutine(ArmRotator(.2f));
            //var step = speed * Time.deltaTime;


            //print("rotateDown is now false");
            //arm.transform.localRotation = Quaternion.Euler(-15, 0, 0);
            //arm.transform.localRotation = Quaternion.Euler(-35, 0, 0);
            //arm.transform.localRotation = Quaternion.Euler(-55, 0, 0);
            //arm.transform.localRotation = Quaternion.Euler(-85, 0, 0);//AngleAxis(-85, arm.x);//RotateTowards(transform.rotation, targetUp.rotation, step);
        }
    }

    //void OnTriggerEnter(Collider collision)
    //{
    //    //Make sure we are colliding with a QuestionTrigger.
    //    if (collision.gameObject.tag == "RailroadCrossingTrigger")
    //    {
    //        rotateDown = true;
    //    }
    //}

    IEnumerator ArmRotator (float waitTime)
    {
        //This is a coroutine
        //Car.Pause();

        arm.transform.localRotation = Quaternion.Euler(-15, 0, 0);
        yield return new
            WaitForSeconds(waitTime);
        arm.transform.localRotation = Quaternion.Euler(-35, 0, 0);
        yield return new
            WaitForSeconds(waitTime);
        arm.transform.localRotation = Quaternion.Euler(-55, 0, 0);
        yield return new
            WaitForSeconds(waitTime);
        arm.transform.localRotation = Quaternion.Euler(-85, 0, 0);
        //StopAllCoroutines();

            // How many seconds the car will stop at a stop sign

        //Car.Resume();

    }
    IEnumerator ArmRotator2(float waitTime)
    {
        //This is a coroutine
        //Car.Pause();

        // Rotate our transform a step closer to the target's.
        arm.transform.localRotation = Quaternion.Euler(-75, 0, 0);
        yield return new WaitForSeconds(waitTime);
        arm.transform.localRotation = Quaternion.Euler(-55, 0, 0);
        yield return new WaitForSeconds(waitTime);
        arm.transform.localRotation = Quaternion.Euler(-35, 0, 0);
        yield return new WaitForSeconds(waitTime);
        arm.transform.localRotation = Quaternion.Euler(-15, 0, 0);
        yield return new WaitForSeconds(waitTime);
        arm.transform.localRotation = Quaternion.Euler(-1, 0, 0);
        //StopAllCoroutines();

        // How many seconds the car will stop at a stop sign

        //Car.Resume();

    }
}


