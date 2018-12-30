using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TapController : MonoBehaviour {

    public GameObject Explosion;
    public GameObject ball;

    public GameManager GameManagerScript;

    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;
    public static event PlayerDelegate OnPlayerScored;

    public float tapForce = 10;
    //public float tiltSmooth = 5;
    public Vector3 startPos;

    public AudioSource tapAudio;
    public AudioSource dieAudio;
    public AudioSource scoreAudio;

    Rigidbody2D rigidbody;
    //Quaternion downRotation;
    //Quaternion forwardRotation;

    GameManager game;

    void Start()
    {
        ball.GetComponent<Renderer>().enabled = true;
        rigidbody = GetComponent<Rigidbody2D>();
        //downRotation = Quaternion.Euler(0, 0, -90);
        //forwardRotation = Quaternion.Euler(0, 0, 35);
        game = GameManager.Instance;
        rigidbody.simulated = false;
    }

    void OnEnable()
    {
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }

    void OnDisable()
    {
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }

    void OnGameStarted()
    {
        
        rigidbody.velocity = Vector3.zero;
        rigidbody.simulated = true;
    }

    void OnGameOverConfirmed()
    {
        ball.GetComponent<Renderer>().enabled = true;
        transform.localPosition = startPos;
        transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        DestroyObjects();

        if (game.GameOver) return;

        if (Input.GetMouseButtonDown(0))
        {
            tapAudio.Play();
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(Vector2.up * tapForce, ForceMode2D.Force);
        }

        

        //transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, tiltSmooth * Time.deltaTime);

    }

    public void DestroyObjects()
    {
        if (GameManagerScript.gameOverConfirmed)
        {

            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("ExplodeZone");
            foreach (GameObject target in gameObjects)
            {
                GameObject.Destroy(target);
                Debug.Log("Destroyed Particles");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "ScoreZone")
        {
            //register a score event
            OnPlayerScored(); //event sent to game manager
            //maybe play a sound
            scoreAudio.Play();
        }

        if (col.gameObject.tag == "DeadZone")
        {
            rigidbody.simulated = false;
            //register a dead event
            OnPlayerDied(); //event sent to game manager
            ball.GetComponent<Renderer>().enabled = false;
            Instantiate(Explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            //play a sound
            dieAudio.Play();

            Debug.Log("Entered Dead Zone for real");
        }
        if (col.gameObject.tag == "BarZone")
        {
            rigidbody.simulated = false;
            //register a dead event
            OnPlayerDied(); //event sent to game manager
            ball.GetComponent<Renderer>().enabled = false;
            Instantiate(Explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            //play a sound
            dieAudio.Play();

            Debug.Log("Entered Bar Zone for real");
        }
    }
        

}
