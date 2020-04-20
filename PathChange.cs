using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using SWS;
using Valve.VR;
using System.Linq;
using UnityEngine.SceneManagement;

public class PathChange : MonoBehaviour
{

    public AudioClip idle;
    public AudioClip driveSound;
    public AudioClip startUp;
    public AudioClip TurnSignal;
    public AudioClip intro;
    public AudioClip questionOptions;

    public AudioSource carAudioSource;
    public AudioSource LeftBlinkerSource;
    public AudioSource RightBlinkerSource;

    public splineMove Car;
    public RailroadArmControl rr_Control;
    public RailroadArmControl rr_Control2;
    public rr_light_controller signalLightController;
    public TrafficLights trafficLightsController;

    Animator armAnimation;
    Animator armAnimation2;
    public GameObject rr_signal;
    public GameObject rr_signal2;
    public string secondDrivingScene;

    public TrainController train;

    int carPause = 0;
    bool can_control = true;

    //This is a referece to the questionCanvasManager set in the inspector.
    [SerializeField]
    public QuestionCanvasManager questionCanvasManager;

    //TODO:
    //It looks like this was done for an old test, and should be removed.
    public GameObject questions;

    public float redLightTime = 10;

    private void Start()
    {
        StartCoroutine(CarAccelerate());
        carAudioSource.PlayOneShot(startUp, .8f);
        armAnimation = rr_signal.GetComponent<Animator>();
        armAnimation2 = rr_signal2.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        // This prevents the car from ever exceeding the fullSpeed variable.
        if (Car.speed > fullSpeed)
        {
            Car.ChangeSpeed(fullSpeed);
        }

        //NO LONGER IN USE:::: This allows the user to control which path the car is on.
        if (can_control)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                string currentPath = Car.pathContainer.name;
                Car.SetPath(WaypointManager.Paths["StraightPath"]);
                Car.moveToPath = true;
                print("Path Change");
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                string currentPath = Car.pathContainer.name;
                Car.SetPath(WaypointManager.Paths["StraightPath_1"]);
                Car.moveToPath = true;
                print("Path Change");
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                string currentPath = Car.pathContainer.name;
                Car.SetPath(WaypointManager.Paths["StraightPath_2"]);
                Car.moveToPath = true;
                print("Path Change");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (carPause == 1)
                {
                    Car.Resume();
                    carPause = 0;
                    print("resume");
                }

                else
                {
                    Car.Pause();
                    carPause = 1;
                    print("pause");
                }
            }

            //Check to see if we are supposed to show the question.
            if (Input.GetKey("e"))
            {
                questionCanvasManager.loadQuestion();
            }

            //TODO:
            //I have no idea what this does.  Do we still need it?
            if (Input.GetKey("r"))
            {
                questions = GameObject.Find("/CarNav/Car/QuestionManager/New Game Object/QuestionCanvas");
                //questionmanager.closeQuestion();
                Destroy(questions);
            }
        }


    }

    /// <summary>
    /// What we're going to do here is check to see if this is a question trigger.  If so, we'll take the question numbers from it,
    /// and we'll assign those to the question canvas manager.  At that point, we'll set launch the first question and let the question
    /// canvas manager handle things from there.
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter(Collider collision)
    {
        //Make sure we are colliding with a QuestionTrigger.
        if (collision.gameObject.tag == "QuestionTrigger")
        {
            Car.Pause();
            //print("Collision(Activate Question Manager");
            //Give the question number list to the question canvas manager.
            questionCanvasManager.questionNums = collision.gameObject.GetComponent<questionNum>().qList;

            //Now, have the questionCanvasManager actually show the questions from the list we just gave it.
            questionCanvasManager.NextQuestionFromQuestionList();
        }

        //This triggers the BEGINNING OF NARRATION
        if (collision.gameObject.tag == "SoundTrigger")
        {
            carAudioSource.loop = true;
        }

        //this triggers the LEFT blinker audio clip
        if (collision.gameObject.tag == "SoundTrigger1")
        {
            LeftBlinkerSource.Play();
        }

        //this triggers the RIGHT blinker audio clip
        if (collision.gameObject.tag == "SoundTrigger2")
        {
            RightBlinkerSource.Play();
        }

        // This sets the speed for the slow down before a stop sign
        if (collision.gameObject.tag == "SlowStop")
        {
            if (train != null)
            {
                train.StartTrain();
            }
            print("Rotate Down");
            StartCoroutine(CarDeccelerate());
            //Car.ChangeSpeed(slowSpeed);
            armAnimation.SetBool("armUp", true);
            armAnimation2.SetBool("armUp", true);
            //rr_Control.rotateDown = true;
            //rr_Control2.rotateDown = true;

            signalLightController.Signaling = true;
            //signalLightController.SignalFlashing();

        }

        if (collision.gameObject.tag == "TrafficLightStop")
        {
            StartCoroutine(CarDeccelerate());
            //Car.ChangeSpeed(slowSpeed);
            trafficLightsController.StartRedLight(redLightTime);
        }

        if (collision.gameObject.tag == "EndScene")
        {
            ScenarioTwoSceneChange("RailroadQuestion");
        }
    }

    private float stayCount = 0.0f;
    public float slowSpeed;
    public float fullSpeed;

    /// <summary>
    /// This determines what happens while the car is inside the colliders that control the order of events happening in the scene.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (stayCount > 0.25f)
        {
            //Debug.Log("staying");
            stayCount = stayCount - 0.25f;
        }
        else
        {
            stayCount = stayCount + Time.deltaTime;
        }
    }

    public bool exitBool = false;


    /// <summary>
    /// This determines what happens when the car exits the colliders controlling when sound, stop lights, and questions happen.
    /// </summary>
    /// <param name="collision">What happens when the car exits the collider.</param>
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "SoundTrigger1")
        {
            LeftBlinkerSource.Stop();
        }

        if (collision.gameObject.tag == "SoundTrigger2")
        {
            RightBlinkerSource.Stop();
        }

        // This sets the speed for the slow down before a stop sign
        if (collision.gameObject.tag == "SlowStop")
        {
            StartCoroutine(CarStop(35));
            //armAnimation.SetBool("armUp", false);
            //armAnimation2.SetBool("armUp", false);


            //StartCoroutine(RR_Signal(4));
            //Car.Pause();
            //Car.ChangeSpeed(fullSpeed);
        }

        if (collision.gameObject.tag == "TrafficLightStop")
        {
            StartCoroutine(CarStopForLight(8));
            //Car.ChangeSpeed(20);
        }

        // This will make the train begin moving when the cart exits the question trigger collider.
        if (collision.gameObject.tag == "QuestionTrigger")
        {
            //if (train != null)
            //{
            //    train.StartTrain();
            //}
        }

    }

    /// <summary>
    /// This funciton changes to the second scenario at the end of the first driving scenario.
    /// </summary>
    /// <param name="sceneName">Enter the exact name of the Scene that should come after the 1st scene.</param>
    public void ScenarioTwoSceneChange(string sceneName)
    {
        Car.Pause();
        SceneManager.LoadScene(sceneName);
    }

    public void CallCarGo()
    {
        if (train != null)
        {
            if (train.hasFinished == true)
            {
                StartCoroutine(CarGo(2));
            }
        }
    }

    IEnumerator CarStopForLight(float waitTime)
    {
        Car.Pause();
        yield return new WaitForSeconds(waitTime);
        Car.Resume();
        StartCoroutine(CarAccelerate());
    }

    /// <summary>
    /// This coroutine stops the car at a stop sign for a set amount of time determined by wait time.
    /// </summary>
    /// <param name="waitTime">This variable controls the amount  of time that the car will wait a stop sign before proceding.</param>
    /// <returns></returns>
    IEnumerator CarStop(float waitTime)
    {
        //This is a coroutine
        Car.Pause();
        yield return new WaitForSeconds(waitTime);    // How many seconds the car will stop at a stop sign

        armAnimation.SetBool("armUp", false);
        armAnimation2.SetBool("armUp", false);
    }

    IEnumerator CarGo(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);    // How many seconds the car will stop at a stop sign

        armAnimation.SetBool("armUp", false);
        armAnimation2.SetBool("armUp", false);

        yield return new WaitForSeconds(2);

        Car.Resume();
    }



    /// <summary>
    /// This Coroutine controls the railroad flashing signal. Raises and lowers the arms after a set amount of time.
    /// </summary>
    /// <param name="waitTime">This is the amount of times that the car will stop at the railroad signal.</param>
    /// <returns></returns>
    IEnumerator RR_Signal(float waitTime)
    {
        print("Rotate Up");
        yield return new WaitForSeconds(waitTime);    // How many seconds the car will stop at a stop sign

        rr_Control.rotateDown = false;
        rr_Control2.rotateDown = false;
        signalLightController.Signaling = false;
        Car.ChangeSpeed(fullSpeed);
    }

    /// <summary>
    /// This function Accelerates the car from any speed until it reaches a speed equal to FullSpeed
    /// </summary>
    IEnumerator CarAccelerate()
    {
        if (Car.speed < fullSpeed)
        {
            Car.ChangeSpeed(Car.speed + 1);
            yield return new WaitForSeconds(.5f);
            StartCoroutine(CarAccelerate());
        }
        else
        {
            StopCoroutine(CarAccelerate());
            //StopAllCoroutines();
        }
    }

    /// <summary>
    /// This function will decelerate the car from any speed down until its speed is equal to the slow speed variable.
    /// </summary>
    IEnumerator CarDeccelerate()
    {
        if (Car.speed > slowSpeed)
        {
            Car.ChangeSpeed(Car.speed - 1);
            yield return new WaitForSeconds(.5f);
            StartCoroutine(CarDeccelerate());
        }
        else
        {
            StopCoroutine(CarDeccelerate());
            //StopAllCoroutines();
        }
    }
}

