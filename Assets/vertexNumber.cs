using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vertexNumber : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		Vector3[] before = GetComponent<MeshFilter>().mesh.vertices;
		Vector3[] test = new Vector3[GetComponent<MeshFilter>().mesh.vertexCount/2];
		Vector3[] normal = mesh.normals;

		int[] beforeT = mesh.triangles;
		int[] testT = new int[1248];
		for(int i =0; i < (GetComponent<MeshFilter>().mesh.vertexCount/2 -1);i++){
			test[i] = before[i];
		}
		for(int j = 0; j < 1248; j++){
			testT[j] = beforeT[j];
		}
		mesh.Clear();
		mesh.vertices = before;
		mesh.triangles = testT;
		mesh.normals = normal;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
