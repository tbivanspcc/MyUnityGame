using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public class s_Question {   // : MonoBehaviour 

	public string questionTitleText ;
	public string questionText ;
	public XmlNode [] answerArray ;
	public answerStruct [] answerStructArray ;



	public s_Question(XmlNode  node)
	{
		if ( s_Manager.currentQuestionType == QuestionType.PCC ){
			CreatePCCQuestion(node);
		}

	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void CreatePCCQuestion(XmlNode node)
	{
		Debug.Log("  CreatePCCQuestion ");
		//Debug.Log("Creating Question from Node");
		XmlNode questionTitleNode = node.FirstChild;
		XmlNode questionTextNode = questionTitleNode.NextSibling; 

		Debug.Log("questionTitle  = " +  questionTitleNode.InnerXml );
		questionTitleText = questionTitleNode.InnerXml ;
		questionText = questionTextNode.InnerText ;

		string xmlPathPattern = "answer";
		XmlNodeList myNodeList = node.SelectNodes( xmlPathPattern );
		InitAnswers(myNodeList);

	}


	public void CreateMoodleQuestion(XmlNode node)
	{
		Debug.Log("  CreateMoodleQuestion ");




	}

	public void InitAnswers (XmlNodeList nodeList)
	{
	 	answerArray = new XmlNode[nodeList.Count]; 				// might be unneccesary now
		answerStructArray = new answerStruct[nodeList.Count];
		Debug.Log ("InitAnswers   nodeList.Count  " + nodeList.Count);
		for ( int i = 0 ; i < nodeList.Count ; i++ )
		{
			answerArray[i] = nodeList[i];
			answerStructArray[i].answerText = nodeList[i].InnerText ;
//			Debug.Log("answerStructArray[i].answerText  = " +   answerStructArray[i].answerText );
			answerStructArray[i].answerScore = int.Parse( nodeList[i].Attributes.GetNamedItem("score").Value );
			Debug.Log( "answerStructArray[i].answerScore  " + answerStructArray[i].answerScore );
		}
	} 

	public struct answerStruct
	{
		public string 	answerText ;
		public int 		answerScore ;
	}



}
