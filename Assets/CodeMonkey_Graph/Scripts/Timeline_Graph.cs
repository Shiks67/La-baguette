using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Timeline_Graph : MonoBehaviour {

	[SerializeField] private Sprite circleSprite;
	private RectTransform graphContainer;

	private RectTransform graph;
	private List<GameObject> gameObjectList;
	private void Awake(){
		gameObjectList = new List<GameObject>();
		graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
		graph = transform.GetComponent<RectTransform>();

		List<double> headList = new List<double>(){-5,-4,-2,2,3,1,5,0,0,0,0,2,4,1,-4,-5,-2,-1,-1,-1,-1};
		List<double> eyeList = new List<double>(){-2,-2,-3,0,2,-1,5,1,0,0,2,4,5,0,-2,-3,-1,0,0,0,0};
		ShowGraph(headList, eyeList);

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

		for(int i = 0; i < eyeList.Count && i < headList.Count; i++){
			float xPosition = xSize + i * xSize;
			float yPosition = ((float)eyeList[i]/ yMaximum) * graphHeight + graphHeight/2;
			Vector2 eyePos = new Vector2(xPosition, yPosition);
			float xPositionH = xSize + i * xSize;
			float yPositionH = ((float)headList[i]/ yMaximum) * graphHeight + graphHeight/2;
			Vector2 headPos = new Vector2(xPositionH, yPositionH);
			if(xPosition > graphWidth-10){
				graphContainer.sizeDelta = new Vector2(graphWidth + 20, graphHeight);
				graph.sizeDelta = new Vector2(graphMainWidth + 20, graphMainHeight);
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
}
