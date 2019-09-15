using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GazeController : MonoBehaviour {

    bool gvrStatus;
    float gvrTimer;

    Color defaultColor = new Color32(94, 133, 214, 235);
    Color gazeColor = new Color32(44, 83, 164, 255);

    List<string> questions = new List<string>() {"What is DOG in Spanish?", "What is CODE in Spanish?", "What is the capital of Spain?", "How do you say THANK YOU in Spanish?", "What is WATER in Spanish?" };
    List<string> answers = new List<string>() { "   Perro", "Zorro", "  Taza", "Código", "Barcelona", "Madrid", "Acogida", "Gracias", "Agua", "Jugo" };
    List<int> answersID = new List<int>() {0, 1, 1, 1, 0};

    int questionNumber;
    bool answered;

    // Use this for initialization
    void Start () {
        GetComponent<Text>().text = questions[0];
       
        GameObject.Find("Left text").GetComponent<TextMesh>().text = answers[0];
        GameObject.Find("Right text").GetComponent<TextMesh>().text = answers[1];
        Debug.Log(questions.Capacity);

        answered = false;
    }

	// Update is called once per frame
	void Update () {

        if (gvrStatus == true){
            GetComponent<Renderer>().material.color = gazeColor; // maybe for correct/incorrect answers
            gvrTimer += Time.deltaTime;
            if(gvrTimer >= 1.8){
                Debug.Log("current question before start: " + questionNumber);
                // check answer, change colour answersID[questionNumber]);

                if ((gameObject.name == "Cube left" && answersID[questionNumber] == 0) || (gameObject.name == "Cube right" && answersID[questionNumber] == 1)) {
                    Debug.Log("selected: " + gameObject.name);
                    Debug.Log("correct: " + answersID[questionNumber]);
                    answered = true;
                    GetComponent<Renderer>().material.color = Color.green;
                }
                else {
                    answered = true;
                    GetComponent<Renderer>().material.color = Color.red;

                }
            }
        }
        else if(gvrStatus == false){
            GetComponent<Renderer>().material.color = defaultColor;
        }

	}

    public void GVROn(){
        gvrStatus = true;
    }

    public void GVROff()
    {
        if(answered == true) {
            answered = false;
            questionNumber++;
            Debug.Log("after switch question: " + questionNumber);

            GameObject.Find("Question text").GetComponent<Text>().text = questions[questionNumber];
            GameObject.Find("Left text").GetComponent<TextMesh>().text = answers[questionNumber * 2];
            GameObject.Find("Right text").GetComponent<TextMesh>().text = answers[questionNumber * 2 + 1];
        }
      
        gvrStatus = false;
        gvrTimer = 0;

        // reset after answering question
    }

}

