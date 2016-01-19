using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour {

	public Text tone;
	public Text ttwo;
	public Text tthree;
	public Text tfour;
	float SavedTime;
	float inTime;
	int fadesCompleted = 0;
	// Use this for initialization
	void Start () {
		SavedTime = Time.time;
		tone.gameObject.GetComponent<CanvasRenderer> ().SetAlpha (0);
		ttwo.gameObject.GetComponent<CanvasRenderer> ().SetAlpha (0);
		tthree.gameObject.GetComponent<CanvasRenderer> ().SetAlpha (0);
		tfour.gameObject.GetComponent<CanvasRenderer> ().SetAlpha (0);
		InvokeRepeating ("CompleteFade", 1, 5);
	}
	
	// Update is called once per frame
	void Update () {
		inTime = Time.time;
		if (inTime <= 1) {
			tone.gameObject.GetComponent<CanvasRenderer> ().SetAlpha (inTime / 1);
		} else if (inTime <= 6) {
			ttwo.gameObject.GetComponent<CanvasRenderer> ().SetAlpha ((inTime - 1) / 5);
		} else if (inTime <= 11) {
			tthree.gameObject.GetComponent<CanvasRenderer> ().SetAlpha ((inTime - 6) / 5);
		} else if (inTime <= 16) {
			tfour.gameObject.GetComponent<CanvasRenderer> ().SetAlpha ((inTime - 11) / 5);
		} else {
			Application.LoadLevel ("SceneTest");
		}
	
	}

	void CompleteFade() {
		fadesCompleted++;
		switch (fadesCompleted) {
		case 1:
			tone.gameObject.GetComponent<CanvasRenderer> ().SetAlpha (100);
			break;
		case 2:
			ttwo.gameObject.GetComponent<CanvasRenderer> ().SetAlpha (100);
			break;
		case 3:
			tthree.gameObject.GetComponent<CanvasRenderer> ().SetAlpha (100);
			break;
		case 4:
			tfour.gameObject.GetComponent<CanvasRenderer> ().SetAlpha (100);
			CancelInvoke("CompleteFade");
			break;

		}
	}
}
