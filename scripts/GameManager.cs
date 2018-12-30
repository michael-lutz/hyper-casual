using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    //FollowPlayer GameOverFollow = new FollowPlayer();
    //public BottomCollider BottomColliderScript;
    public FollowPlayer followPlayerScript;
    public FollowPlayer followPlayerScriptBottom;
    //public LeftSideCollider LeftSideColliderScript;
    //public RightSideCollider RightSideColliderScript;
    //public BarScript BarScriptCode;

    public bool gameOverConfirmed;

    public static bool isGameOver;


   // public FollowPlayerBottomBar FollowPlayerBottomBarScript;

    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOverConfirmed;

    public static GameManager Instance;

    public GameObject startPage;
    public GameObject gameOverPage;
    public GameObject countdownPage;
    public Text scoreText;

    enum PageState
    {
        None,
        Start,
        GameOver,
        Countdown
    }

    int score = 0;
    public bool gameOver = true;

    public bool GameOver { get { return gameOver; } }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SetPageState(PageState.Start);
        isGameOver = false;
    }

    void OnEnable()
    {
        CountdownText.OnCountdownFinished += OnCountdownFinished;
        TapController.OnPlayerDied += OnPlayerDied;
        TapController.OnPlayerScored += OnPlayerScored;
    }

    void OnDisable()
    {
        CountdownText.OnCountdownFinished -= OnCountdownFinished;
        TapController.OnPlayerDied -= OnPlayerDied;
        TapController.OnPlayerScored -= OnPlayerScored;
    }

    void OnCountdownFinished()
    {
        SetPageState(PageState.None);
        OnGameStarted();
        //LeftSideColliderScript.works = true;
        score = 0;
        gameOver = false;
        //BarScriptCode.gameStarted = true;
    }

    void OnPlayerDied()
    {
        gameOver = true;
        int savedScore = PlayerPrefs.GetInt("HighScore");
        if (score > savedScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        Debug.Log("player is dead");
        SetPageState(PageState.GameOver);
        //BarScriptCode.gameStarted = false;
        
    }

    void OnPlayerScored()
    {
        score++;
        scoreText.text = score.ToString();
    }

    void SetPageState(PageState state)
    {
        switch(state)
        {
            case PageState.None:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countdownPage.SetActive(false);
                break;
            case PageState.Start:
                startPage.SetActive(true);
                gameOverPage.SetActive(false);
                countdownPage.SetActive(false);
                break;
            case PageState.GameOver:
                startPage.SetActive(false);
                gameOverPage.SetActive(true);
                countdownPage.SetActive(false);
                break;
            case PageState.Countdown:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countdownPage.SetActive(true);
                break;
        }
    }

    public void ConfirmedGameOver()
    {
        gameOverConfirmed = true;
        //actived when you hit the replay button
        OnGameOverConfirmed(); //event
        //OnGameOverConfirmedBall();
        scoreText.text = "";
        followPlayerScript.OnGameOverConfirmedBall();
        //LeftSideColliderScript.OnGameOverConfirmedBar();
        //RightSideColliderScript.OnGameOverConfirmedBar();
        followPlayerScriptBottom.OnGameOverConfirmedBall();
        //BarScriptCode.OnGameOverConfirmed();

        //BottomColliderScript.OnGameOverBottom();
        //OnGameOverBottom();
        //FollowPlayerBottomBarScript.OnGameOverConfirmedBottomBar();
        isGameOver = true;

        SetPageState(PageState.Start);
        
        
        //LeftSideColliderScript.works = false;
    }

    public void StartGame()
    {
        gameOverConfirmed = false;
        //actived when you hit the play button
        SetPageState(PageState.Countdown);
    }
}
