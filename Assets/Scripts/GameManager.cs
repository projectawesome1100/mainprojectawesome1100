using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static GameManager _instance; //means this exists doesn't matter how mush instance of the game only one 

    public float pointsPerUnitTravelled = 1.0f;
    public float gameSpeed = 10.0f;
    public string titleScreenName = "TitleScreen";

    public static GameManager Instance
    {
        get
        {
            if(_instance != null)
            {
                return _instance;
            }
            else
            {
                GameObject gameManager =  new GameObject("GameManager");
                _instance =  gameManager.AddComponent<GameManager>();
                return _instance;

            }
            
        }
    }

    [HideInInspector]
    public int previousScore = 0; //we don't want to be visible in inspector but need to access the value from an other script so public

    private float score = 0.0f;
    private bool gameOver = false;
    private static float highScore;
    private bool hasSaved=false;

	// Use this for initialization
	void Start () {
        if (_instance != this)
        {
            if (_instance == null)
            {
                _instance = this; //then we can access GameManager.Instance from every other function
            }
            else
            {
                Destroy(gameObject);
            }
        }
        loadhighScore();
        DontDestroyOnLoad(gameObject); //by default everything is destroyed on launch of scene
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Application.loadedLevelName != titleScreenName) //if not in title screen do game logic
        {
            if (GameObject.FindGameObjectWithTag("Player") == null)
            {
                gameOver = true;
            }
            if (gameOver)
            {
                if (!hasSaved)
                {
                    SaveHighScore();
                    previousScore = (int)score;
                    hasSaved = true;
                }
                if (Input.anyKeyDown)
                {
                    Application.LoadLevel(titleScreenName);
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

        else
        {
            //reset stuff for next game
            ResetGame();
        }
    }

    void ResetGame()
    {
        score = 0.0f;
        gameOver = false;
        hasSaved = false;
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
        if (Application.loadedLevelName != titleScreenName)
        {
            int currentHighScore = (int)highScore;
            int currentScore = (int)score;
            GUILayout.Label("Score : " + currentScore.ToString());
            GUILayout.Label("Highscore : " + currentHighScore.ToString());
            if (gameOver == true)
            {
                GUILayout.Label("Game Over ! Press any key to quit");
            }
        }
    }
}
