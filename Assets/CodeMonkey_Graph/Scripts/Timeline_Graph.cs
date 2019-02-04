using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Timeline_Graph : MonoBehaviour {

	[SerializeField] private Sprite circleSprite;
	private RectTransform graphContainer;
	private RectTransform labelTemplateY;

	private RectTransform timeAxisTemplate;
	private RectTransform timeAxis;
	private RectTransform dashContainer;
	private RectTransform graph;
	private List<GameObject> gameObjectList;

	private Scrollbar scrollbar;
	public int offset;
	private int timer;
	private void Awake(){
		scrollbar = transform.Find("scrollbar").GetComponent<Scrollbar>();
		scrollbar.gameObject.SetActive(false);
		gameObjectList = new List<GameObject>();
		timer = 0;
		graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
		graph = transform.GetComponent<RectTransform>();
		dashContainer = graphContainer.Find("dashContainer").GetComponent<RectTransform>();
		labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();
		timeAxisTemplate = dashContainer.Find("dashTemplateY").GetComponent<RectTransform>();

		timeAxis = Instantiate(timeAxisTemplate);
		timeAxis.SetParent(graphContainer,false);
		timeAxis.gameObject.SetActive(true);
		timeAxis.anchoredPosition = new Vector2(-1.96f,graphContainer.sizeDelta.y/2);

		List<double> headList = new List<double>(){};
		List<double> eyeList = new List<double>(){};
		ShowGraph(headList, eyeList);

		for(int i = -6; i <= 6; i++){
			RectTransform labelY = Instantiate(labelTemplateY);
			labelY.SetParent(graphContainer,false);
			labelY.gameObject.SetActive(true);
			labelY.anchoredPosition = new Vector2(-5f, (i/12f) * graphContainer.sizeDelta.y + graphContainer.sizeDelta.y/2 + 4f);
			labelY.GetComponent<Text>().text = i.ToString();

			RectTransform labelDown = Instantiate(labelTemplateY);
			labelDown.SetParent(graphContainer,false);
			labelDown.gameObject.SetActive(true);
			labelDown.anchoredPosition = new Vector2(graphContainer.sizeDelta.x+10f, (i/12f) * graphContainer.sizeDelta.y + graphContainer.sizeDelta.y/2 + 4f);
			labelDown.GetComponent<Text>().text = i.ToString();
			if(i == 6){
				labelY.GetComponent<Text>().text = "5+";
				labelY.GetComponent<Text>().color = Color.red;
				labelDown.GetComponent<Text>().text = "5+";
				labelDown.GetComponent<Text>().color = Color.red;
			}
			if(i == -6){
				labelY.GetComponent<Text>().text = "-5+";
				labelY.GetComponent<Text>().color = Color.red;
				labelDown.GetComponent<Text>().text = "-5+";
				labelDown.GetComponent<Text>().color = Color.red;
			}	
		}

		FunctionPeriodic.Create(() => {
			double x = 0;
            for (int i = 0; i < RayCaster.headHits.Length; i++)
            {
                RaycastHit hit = RayCaster.headHits[i];
                if (hit.collider.gameObject.name == "BaguetteCollider")
                {
                    x = Math.Round(
                        hit.collider.gameObject.transform.parent.transform.
                        InverseTransformPoint(hit.point).y * 5, 2) * -1;
                    //headPosList.Add(x);
					if(x > 0)
						x = Math.Round(x*2+0.5) /2;	
					if(x < 0)
						x = Math.Round(x*2-0.5) /2;
					if(x < -5)
						x = -6;
					if(x > 5)
						x = 6;

					headList.Add(x);	
                }
            }

			for (int i = 0; i < RayCaster.gazeHits.Length; i++)
            {
                RaycastHit hit = RayCaster.gazeHits[i];
                if (hit.collider.gameObject.name == "BaguetteCollider")
                {
                    x = Math.Round(
                        hit.collider.gameObject.transform.parent.transform.
                        InverseTransformPoint(hit.point).y * 5, 2) * -1;
                    //headPosList.Add(x);
					if(x > 0)
						x = Math.Round(x*2+0.5) /2;	
					if(x < 0)
						x = Math.Round(x*2-0.5) /2;
					if(x < -5)
						x = -6;
					if(x > 5)
						x = 6;

					eyeList.Add(x);	
                }
            }
			ShowGraph(headList, eyeList);
        }, .2f);
		
		/* FunctionPeriodic.Create(() => { 
			float xSize = 10.7f;
			timer++;
			GameObject labelTimeline = new GameObject("labelTimeline");
			labelTimeline.transform.SetParent(graphContainer,false);
			RectTransform timeline = Instantiate(labelTemplateY);
			timeline.SetParent(labelTimeline.transform,false);
			timeline.gameObject.SetActive(true);
			timeline.GetComponent<Text>().text = DisplayTime(timer);
			timeline.anchoredPosition = new Vector2(xSize + (timer*5) * xSize,graphContainer.sizeDelta.y/2);
		}, 1f);*/
	}

	private String DisplayTime(int timer){
		int secondes = timer % 60;
		double tg = timer / 60;
		int minutes = (int)Math.Floor(tg);
		String minutesString = "";
		String secondesString ="";
		if(minutes < 10){
			minutesString = "0"+minutes.ToString()+":";
		}	
		else{
			minutesString = minutes.ToString();
		}
		if(secondes < 10){
			secondesString = "0"+secondes.ToString();
		}else{
			secondesString = secondes.ToString();
		}
		 
		return minutesString + secondesString;
	}

	private GameObject CreateCircle(Vector2 anchoredPosition, Color dotColor){
		GameObject gameObject = new GameObject("circle", typeof(Image));
		gameObject.transform.SetParent(graphContainer, false);
		gameObject.GetComponent<Image>().sprite = circleSprite;
		gameObject.GetComponent<Image>().color = dotColor;
		RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
		rectTransform.anchoredPosition = anchoredPosition;
		rectTransform.sizeDelta = new Vector2(11,11);
		rectTransform.anchorMin = new Vector2(0,0);
		rectTransform.anchorMax = new Vector2(0,0);
		return gameObject;
	}

	private void ShowGraph(List<double> headList, List<double> eyeList){
		float graphHeight = graphContainer.sizeDelta.y;
		float graphWidth = graphContainer.sizeDelta.x;
		float graphMainWidth = graph.sizeDelta.x;
		float graphMainHeight = graph.sizeDelta.y;
		float yMaximum = 12f; 
		float xSize = 10.7f;

		 foreach (GameObject gameObject in gameObjectList) {
            Destroy(gameObject);
         }
		 gameObjectList.Clear();
		
		int NbMaxDisplayed = 46;
		int NbDataForOneSecond = 5;

		if(eyeList.Count < NbMaxDisplayed || headList.Count < NbMaxDisplayed){
			for(int i = 0; i < eyeList.Count || i < headList.Count; i++){
				float xPosition = xSize + i * xSize;
				float yPosition = ((float)eyeList[i]/ yMaximum) * graphHeight + graphHeight/2;
				Vector2 eyePos = new Vector2(xPosition, yPosition);
				float xPositionH = xSize + i * xSize;
				float yPositionH = ((float)headList[i]/ yMaximum) * graphHeight + graphHeight/2;
				Vector2 headPos = new Vector2(xPositionH, yPositionH);
				/* if(xPosition > graphWidth-10){
					graphContainer.sizeDelta = new Vector2(graphWidth + 20, graphHeight);
					graph.sizeDelta = new Vector2(graphMainWidth + 20, graphMainHeight);
					timeAxis.sizeDelta = new Vector2(timeAxis.sizeDelta.x + 20, timeAxis.sizeDelta.y);
				}*/
				
				if(i % 5 == 0){
					int timer = i/NbDataForOneSecond;

					GameObject labelTimeline = new GameObject("labelTimeline");
					labelTimeline.transform.SetParent(graphContainer,false);
					RectTransform timeline = Instantiate(labelTemplateY);
					timeline.SetParent(labelTimeline.transform,false);
					timeline.gameObject.SetActive(true);
					timeline.GetComponent<Text>().text = DisplayTime(timer);
					timeline.anchoredPosition = new Vector2(xSize + i * xSize,graphContainer.sizeDelta.y + 45f);
					gameObjectList.Add(labelTimeline);
				}

				if(eyeList[i] > headList[i])
					CreateDotConnection(headPos, eyePos, Color.blue);
				if(eyeList[i] < headList[i])
					CreateDotConnection(headPos, eyePos, Color.magenta);
				if(eyeList[i] == headList[i])
					CreateDotConnection(headPos, eyePos, Color.white);

				GameObject eyeGameObject = CreateCircle(eyePos, Color.red);
				GameObject headGameObject = CreateCircle(headPos, Color.grey);
				
				gameObjectList.Add(eyeGameObject);
				gameObjectList.Add(headGameObject);
			}
		}else{			
			scrollbar.gameObject.SetActive(true);

			int iStartValue = eyeList.Count < headList.Count ? eyeList.Count - NbMaxDisplayed : headList.Count - NbMaxDisplayed;
			int iMaxValue = eyeList.Count < headList.Count ? eyeList.Count : headList.Count;
			double offsetValueDouble = Math.Floor(scrollbar.value*100) / 100 * iMaxValue;
			offset = (int)Math.Floor(offsetValueDouble);
			iStartValue = iStartValue - offset >= 0 ? iStartValue - offset : 0;
			iMaxValue = iMaxValue - offset >= iStartValue + NbMaxDisplayed ? iMaxValue - offset : iStartValue + NbMaxDisplayed;

			int cpt = 0;

			for(int i = iStartValue; i < iMaxValue; i++){
				float xPosition = xSize + cpt * xSize;
				float yPosition = ((float)eyeList[i]/ yMaximum) * graphHeight + graphHeight/2;
				Vector2 eyePos = new Vector2(xPosition, yPosition);
				float xPositionH = xSize + cpt * xSize;
				float yPositionH = ((float)headList[i]/ yMaximum) * graphHeight + graphHeight/2;
				Vector2 headPos = new Vector2(xPositionH, yPositionH);
				/* if(xPosition > graphWidth-10){
					graphContainer.sizeDelta = new Vector2(graphWidth + 20, graphHeight);
					graph.sizeDelta = new Vector2(graphMainWidth + 20, graphMainHeight);
					timeAxis.sizeDelta = new Vector2(timeAxis.sizeDelta.x + 20, timeAxis.sizeDelta.y);
				}*/
				
				if(cpt % 5 == 0){
					int timer = (cpt + iStartValue)/NbDataForOneSecond;

					GameObject labelTimeline = new GameObject("labelTimeline");
					labelTimeline.transform.SetParent(graphContainer,false);
					RectTransform timeline = Instantiate(labelTemplateY);
					timeline.SetParent(labelTimeline.transform,false);
					timeline.gameObject.SetActive(true);
					timeline.GetComponent<Text>().text = DisplayTime(timer);
					timeline.anchoredPosition = new Vector2(xSize + cpt * xSize,graphContainer.sizeDelta.y + 40f);
					gameObjectList.Add(labelTimeline);
				}


				if(eyeList[i] > headList[i])
					CreateDotConnection(headPos, eyePos, Color.blue);
				if(eyeList[i] < headList[i])
					CreateDotConnection(headPos, eyePos, Color.magenta);
				if(eyeList[i] == headList[i])
					CreateDotConnection(headPos, eyePos, Color.white);

				GameObject eyeGameObject = CreateCircle(eyePos, Color.red);
				GameObject headGameObject = CreateCircle(headPos, Color.grey);
				
				gameObjectList.Add(eyeGameObject);
				gameObjectList.Add(headGameObject);
				cpt++;
			}
		}
		
		//CreateLabelYDown();
	}

	private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB, Color dotConnectionColor){
		GameObject gameObject = new GameObject("dotConnection", typeof(Image));
		gameObject.transform.SetParent(graphContainer, false);
		gameObject.GetComponent<Image>().color = dotConnectionColor;
		RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
		Vector2 dir = (dotPositionB - dotPositionA).normalized;
		float distance = Vector2.Distance(dotPositionA, dotPositionB);
		rectTransform.anchorMin = new Vector2(0,0);
		rectTransform.anchorMax = new Vector2(0,0);
		rectTransform.sizeDelta = new Vector2(distance,10f);
		rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
		rectTransform.localEulerAngles = new Vector3(0,0,UtilsClass.GetAngleFromVectorFloat(dir));
		gameObjectList.Add(gameObject);
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			graph.position = new Vector3(graph.position.x,graph.position.y-40,graph.position.z);
		}
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			graph.position = new Vector3(graph.position.x,graph.position.y+40,graph.position.z);
		}
	}
}
