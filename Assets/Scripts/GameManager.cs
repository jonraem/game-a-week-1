using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public int currentLevel = 0;	
	public float currentTime;
	public Rect timeRect;
	public string timeText;
	public GUISkin skin;

	private bool showWinScreen = false;
	public int winScreenWidth, winScreenHeight;
	private bool completed = false;

	// Use this for initialization
	void Start () {
		/*currentLevel = 0;
		if (PlayerPrefs.GetInt ("Level Completed") > 0) {
			currentLevel = PlayerPrefs.GetInt ("Level Completed");
		} else if (PlayerPrefs.GetInt("Level Completed") > 2) {
			currentLevel = 0;
		}*/
	}

	// Update is called once per frame
	void Update () {

		if(!completed)
		{
			currentTime += Time.deltaTime;
			timeText = string.Format ("{0:0.00}", currentTime);
		}


		if (Input.GetKey(KeyCode.Escape)){
			Application.LoadLevel (currentLevel);
			}


		if (Input.GetKey (KeyCode.Backspace)) {
			Application.LoadLevel(currentLevel-1);
		}
	}
	
	public void CompleteLevel()
	{
		showWinScreen = true;
		completed = true;
	}

	void SaveGame()
	{
		PlayerPrefs.SetInt ("Level Completed", currentLevel);
	}

	void LoadNextLevel()
	{
		if (currentLevel < 2) {
			SaveGame ();
			Application.LoadLevel (currentLevel + 1);
		}
	}

	void OnGUI(){
		GUI.skin = skin;
		GUI.Label (timeRect, timeText, skin.GetStyle ("GUI"));
		GUI.Label (new Rect (40, Screen.height - 60, 300, 100), "Press ESC to reset level.");
		GUI.Label (new Rect (40, Screen.height - 40, 300, 100), "Press Backspace to play previous level."); 

		if (showWinScreen)
		{
			Rect winScreenRect = new Rect(Screen.width/2 - (winScreenWidth/2), Screen.height/2 - (winScreenHeight/2), winScreenWidth, winScreenHeight);
			GUI.Box(winScreenRect, "Level completed");

			if (currentLevel != 2)
			{
				if (GUI.Button (new Rect(winScreenRect.x + 20, winScreenRect.y + winScreenRect.height - 40 - 20, 100, 40), "Quit"))
				{
					Application.Quit ();
				}

				if (GUI.Button (new Rect(winScreenRect.x + winScreenRect.width - 150 - 20, winScreenRect.y + winScreenRect.height - 40 - 20, 150, 40), "Continue"))
				{
					LoadNextLevel();
				}

			} else {
				if (GUI.Button (new Rect(winScreenRect.x + 100, winScreenRect.y + winScreenRect.height - 40 - 20, 100, 40), "Quit"))
				{
					Application.Quit ();
				}
			}
			GUI.Label(new Rect(winScreenRect.x + 20, winScreenRect.y + 45, 300, 50), "Congratulations! Your time was " + timeText);

		}
	}
}