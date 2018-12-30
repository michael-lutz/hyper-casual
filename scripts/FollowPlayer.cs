using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowPlayer : MonoBehaviour
{
    //public bool resetPosition = false;

    public Transform player;

    public Vector3 startPos;

   /* void OnEnable()
    {
        GameManager.OnGameOverConfirmed += OnGameOverConfirmedBall;
    }

    void OnDisable()
    {
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmedBall;
    }*/

    public void OnGameOverConfirmedBall()
    {
        transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);
        //transform.position = startPos;
        Debug.Log("reset camera position to: " + transform.position);
    }

    void Update()
    {
        if (player.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
        }
    }



    

    /* public delegate void GameDelegate();
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
     bool gameOver = true;

     public bool GameOver { get { return gameOver; } }

     void Awake()
     {
         Instance = this;
     }

     void OnEnable()
     {
         CountdownText.OnCountdownFinished += OnCountdownFinished;

     }

     void OnDisable()
     {
         CountdownText.OnCountdownFinished -= OnCountdownFinished;

     }

     void OnCountdownFinished()
     {
         SetPageState(PageState.None);
         OnGameStarted();
         score = 0;
         gameOver = false;
     }

     void OnPlayerDied()
     {
         gameOver = true;
         int savedScore = PlayerPrefs.GetInt("HighScore");
         if (score > savedScore)
         {
             PlayerPrefs.SetInt("HighScore", score);
         }
         SetPageState(PageState.GameOver);
     }

     void OnPlayerScored()
     {
         score++;
         scoreText.text = score.ToString();
     }

     void SetPageState(PageState state)
     {
         switch (state)
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
         //actived when you hit the replay button
         OnGameOverConfirmed(); //event
         scoreText.text = "0";
         SetPageState(PageState.Start);
     }

     public void StartGame()
     {
         //actived when you hit the play button
         SetPageState(PageState.Countdown);
     }
 */
}
