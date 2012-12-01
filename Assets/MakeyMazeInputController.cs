using UnityEngine;
using System.Collections;

public class MakeyMazeInputController : MonoBehaviour {
	
	//if true that means errors are counted as breaking contact
	public bool isContactMode; 
	
	public GUIStyle textStyle;
	public Rect textRect;
	
	private float startTime;
	private float endTime;
	private int errorCount;
	private int totalErrors;
	private enum gameState {
		START,
		PLAYING,
		END
	}
	
	private gameState state;
	
	// Use this for initialization
	void Start () {
	
		state = gameState.START;
	}
	
	void Update ()
	{
		
		if(Input.GetKeyUp(KeyCode.UpArrow)) {
			BeginGame ();
			state = gameState.PLAYING;
		}
		
		if(Input.GetKeyDown(KeyCode.DownArrow)) {
			EndGame ();
			state = gameState.END;
		}
		
		if(isContactMode)
		{
			if(Input.GetKeyUp(KeyCode.RightArrow)) {
				errorCount++;
			}
		}
		else
		{
			if(Input.GetKeyDown(KeyCode.RightArrow)) {
				errorCount++;
			}
		}
	
	}
	
	
	void OnGUI () {
		
		switch(state) {
		case gameState.START:
			DrawStart();
			break;
		case gameState.PLAYING:
			DrawTimer();
			break;
		case gameState.END:
			DrawEndGame();
			break;
		}
	
	}
	
	void BeginGame () {
		startTime = Time.time;
		errorCount = 0;
	}
	
	void EndGame () {
		endTime = Time.time;
		totalErrors = errorCount;
		
	}
	
	void DrawTimer() {
		GUI.Label(textRect, "Elapsed Time: " + ElapsedTime(Time.time) + "\n Faults: " + errorCount, textStyle); 
	}
	
	void DrawEndGame () {
		
		GUI.Label(textRect, "Congratulations you finished in " + ElapsedTime(endTime) + " with " + totalErrors.ToString() + " faults", textStyle);
		
	}
	
	void DrawStart () {
		
		GUI.Label(textRect, "To Start touch the start point on your maze. Get to the end of the maze as fast as you can without lifting your pointer", textStyle);
	}
	
	string ElapsedTime (float end) {
		float elapsedTime = end - startTime;
		int min = (int)elapsedTime / 60;
		int sec = (int)elapsedTime - (min*60);
		
		string display = min.ToString() + ":" + sec.ToString();
		if(sec < 10)
		{
			display = min.ToString() + ":0" + sec.ToString();		
		}
		
		return display;
	}
}
