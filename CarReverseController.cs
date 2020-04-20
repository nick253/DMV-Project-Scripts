using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;

public class CarReverseController : MonoBehaviour
{

    public GameObject carForward;
    public GameObject carReverse;

    public splineMove car;
    public splineMove bus;
    public BusController busController;

    //This is a referece to the questionCanvasManager set in the inspector.
    [SerializeField]
    public QuestionCanvasManager questionCanvasManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwitchToForward()
    {
        carReverse.SetActive(false);
        carForward.SetActive(true);
    }

    void OnTriggerEnter(Collider collision)
    {
        //Make sure we are colliding with a QuestionTrigger.
        if (collision.gameObject.tag == "QuestionTrigger")
        {
            car.Pause();
            //print("Collision(Activate Question Manager");
            //Give the question number list to the question canvas manager.
            questionCanvasManager.questionNums = collision.gameObject.GetComponent<questionNum>().qList;

            //Now, have the questionCanvasManager actually show the questions from the list we just gave it.
            questionCanvasManager.NextQuestionFromQuestionList();
        }

        //Make sure we are colliding with a QuestionTrigger.
        if (collision.gameObject.tag == "BusStart")
        {
            bus.StartMove();
        }

        //Make sure we are colliding with a QuestionTrigger.
        if ((collision.gameObject.tag == "BusStop") && (busController.isStopped == true))
        {
            StartCoroutine(ResumeCar());
        }
    }

    IEnumerator ResumeCar()
    {
        car.Pause();
        yield return new WaitForSeconds(9f);
        car.Resume();
    }
}
