/// <summary>
/// s_Manager - Manager class written specifically for a Quiz Game using XML data files for question banks.
/// 
/// </summary>

using UnityEngine;
using UnityEngine.UI ;
using UnityEngine.EventSystems ;
using System.Collections;
using System.Collections.Generic ;
using System.Xml;
using System.IO;

public enum GameState { Starting ,Paused, InQuestion, BetweenQuestions, Feedback};
public enum QuestionType { PCC,  Moodle};



public class s_Manager : MonoBehaviour {

	public GameObject 		questionPanel ;// Panel on which the question will appear
	public GameObject		feedbackPanel ;	// Panel on which the feedback will appear
	public Text				feedbackText ;
	public GameObject		placementPanel ;// Just used for moving panel in and out. 

	public static GameState 		currentGameState ;
	public static QuestionType 	currentQuestionType ;
	public TextAsset 		questionFile;
	public List<s_Question> questionList ;
	public int 				currentQuestionIndex ; // Index of the currentQuestion in the questionList 

	public Text				questionText ;
	public Button [] 		answerButtonArray ; // Array of answer buttons

	private Text buttonText ;


	// Use this for initialization
	void Start () {
		feedbackPanel.SetActive(false);
		currentGameState = GameState.Starting ;
		currentQuestionType = QuestionType.PCC ;
		currentQuestionIndex = 0 ;

		questionList = new List<s_Question> ();
		string textData = questionFile.text;	
		InitQuestionList (textData);
		currentGameState = GameState.BetweenQuestions ;
	}
	
	// Update is called once per frame
	void Update () {
		if ( currentGameState == GameState.BetweenQuestions){
			LoadNextQuestion();
		}

		if ( EventSystem.current.IsPointerOverGameObject())
		{
			//Debug.Log(" Over a UI element");
		}
	
	}

	/// <summary>
	/// Inits the question list.
	/// </summary>
	public void InitQuestionList(string xmlData)
	{	
		Debug.Log ("InitQuestionList    ");
		if ( currentQuestionType == QuestionType.PCC){
			InitPCCQuestions(xmlData);
		}


	}

	public void InitPCCQuestions(string xmlData)
	{
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.Load( new StringReader(xmlData) );
		string xmlPathPattern = "//questionList/question";
		XmlNodeList myNodeList = xmlDoc.SelectNodes( xmlPathPattern );

		foreach (XmlNode node in myNodeList) {
			print (  node.InnerXml ) ;
			s_Question newQ = new s_Question (node);
			questionList.Add (newQ);
		}
		print (questionList.Count);


	}

	public void InitMoodleQuestions(string xmlData)
	{


	}


	public void LoadNextQuestion()
	{
		Debug.Log("Inside LoadNextQuestion") ;
		currentGameState = GameState.InQuestion ;
		// Set Button and Question text here.
		questionText.text 	= questionList[ currentQuestionIndex ].questionText  ;
		int numAnswers 		= questionList[ currentQuestionIndex ].answerArray.Length ;

		//answerButtonArray = new Button[ numAnswers ];
		for (int i = 0; i < numAnswers; i++) 
		{
			string answer = questionList [ currentQuestionIndex ].answerArray [i].InnerText;
			print ("answer = " +answer);
			//print (answerButtonArray [i]);
			answerButtonArray [i].GetComponentInChildren<Text> ().text = answer;
			//answerButtonArray[i]
		}
	}// END of 


	public void HandleAnswerButtonPress(int buttonIndex )
	{
		currentGameState = GameState.Feedback ;
		Debug.Log("HandleAnswerButtonPress  " + buttonIndex);
		Debug.Log("You scored : " +    questionList[currentQuestionIndex].answerStructArray[buttonIndex].answerScore);

		questionPanel.SetActive(false);
		feedbackPanel.SetActive(true);
		feedbackText.text = "You scored : " +    questionList[currentQuestionIndex].answerStructArray[buttonIndex].answerScore ;

	}

	public void HandleNextButton()
	{
		Debug.Log("HandleNextButton");
		feedbackPanel.SetActive(false);
		questionPanel.SetActive(true);
		currentGameState = GameState.BetweenQuestions ;

		
	}


}// END OF CLASS =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

