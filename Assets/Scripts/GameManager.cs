using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public float pointsPerUnitTravelled = 1.0f;
    public float gameSpeed = 10.0f;
    public static GameManager Instance; //means this exists doesn't matter how mush instance of the game only one 

    private float score = 0.0f;
    private bool gameOver = false;
    private static float highScore;
    private bool hasSaved=false;

	// Use this for initialization
	void Start () {
        Instance = this; //then we can access GameManager.Instance from every other function
        loadhighScore();
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(GameObject.FindGameObjectWithTag("Player")==null)
        {
            gameOver = true;
        }
        if (gameOver)
        {
            if (!hasSaved)
            {
                SaveHighScore();
                hasSaved = true;
            }
            if (Input.anyKeyDown)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }

        if (!gameOver)
        {          
            score += pointsPerUnitTravelled * gameSpeed * Time.deltaTime;
            if (score > highScore)
            {
                highScore = score;
            }
        }
    }


    void SaveHighScore()
    {
        PlayerPrefs.SetInt("Highscore", (int)highScore);
        PlayerPrefs.Save();
    }

    void loadhighScore()
    {
        highScore = PlayerPrefs.GetInt("Highscore");
    }

    void OnGUI()
    {
        int currentHighScore = (int)highScore;
        int currentScore = (int)score;
        GUILayout.Label("Score : " + currentScore.ToString());
        GUILayout.Label("Highscore : " + currentHighScore.ToString());
        if (gameOver == true)
        {
            GUILayout.Label("Game Over ! Press any key to reset");
        }
    }
}
