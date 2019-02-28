using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoggingManager : MonoBehaviour {

	private Dictionary<string, List<string>> logCollection;

	private ConnectToMySQL mySQL;

	void Awake () {
		logCollection = new Dictionary<string, List<string>>();

		// Add the database columns
		logCollection.Add("Email", new List<string>());
		logCollection.Add("DateAdded", new List<string>());
		logCollection.Add("ParticipantNumber", new List<string>());
		logCollection.Add("TrialNumber", new List<string>());
		logCollection.Add("CutPosition", new List<string>());
		logCollection.Add("TimeToCut", new List<string>());
		logCollection.Add("BaguetteSize", new List<string>());
		logCollection.Add("Mirror", new List<string>());
		logCollection.Add("EyePatch", new List<string>());
		logCollection.Add("PrismOffset", new List<string>());

		mySQL = gameObject.GetComponent<ConnectToMySQL>();
		
	}

	public void WriteToLog(string varName, string varValue) {
		logCollection[varName].Add(varValue);

	}

	// TODO: Write to log file on disk as backup

	public void UploadLogs() {
		mySQL.AddToUploadQueue(logCollection);
		mySQL.UploadNow();
		logCollection.Clear();
	}
}
