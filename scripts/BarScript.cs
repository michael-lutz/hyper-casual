using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarScript : MonoBehaviour {

    Vector3 tempPos;

    public Vector3 startPos;

    public int speedDivider = 8;

    //public RightSideCollider RightColliderScript;
    //public LeftSideCollider LeftColliderScript;

    public float moveSpeed = 1f;

    float journeyLength;

    public int leftOrRightNo;

    float startTime;

    bool leftIsFalse = false;

    public Vector3 firstPos;
    public Vector3 secondPos;

    //public bool gameStarted = false;

    static System.Random rnd = new System.Random();



    void Awake()
    {
        float screenWidth = Camera.main.orthographicSize;
        screenWidth = screenWidth * Camera.main.aspect;

        float leftDist = (-1 * screenWidth);
        float rightDist = (screenWidth);

        startTime = Time.time;

        //leftOrRightDeterminer();
        leftOrRightNo = leftOrRightDeterminer();

        Debug.Log("Left Or Right No: " + leftOrRightNo);
        
        if (leftOrRightNo == -1)
        {
            firstPos = new Vector3(leftDist, transform.position.y, transform.position.z);
            secondPos = new Vector3(rightDist, transform.position.y, transform.position.z);
        }
        else if (leftOrRightNo == 1)
        {
            firstPos = new Vector3(rightDist, transform.position.y, transform.position.z);
            secondPos = new Vector3(leftDist, transform.position.y, transform.position.z);
        }

        moveSpeed = randomDeterminer();

        Debug.Log("Left Position: " + firstPos);
        Debug.Log("Right Position: " + secondPos);

        journeyLength = Mathf.Abs(leftDist - rightDist);

        float size = SizeGenerator();

        Debug.Log(size);

        transform.localScale += new Vector3(size, 0, 0);

    }

    void Update()
    {
        /*if (RightColliderScript.leftOrRightChange || LeftColliderScript.leftOrRightChange)
        {
            leftOrRightNo = leftOrRightNo * (-1);
        }*/
        moveObject();
       // Debug.Log("Movement Speed: " + moveSpeed);
        //transform.position = Vector3.Lerp(firstPos, secondPos, moveSpeed);


    }

    /*public void OnGameOverConfirmed()
    {
        transform.position = new Vector3(startPos.x, transform.position.y, transform.position.z);
        Debug.Log("Moved Bar Back to: " + transform.position);
    }*/

    float SizeGenerator()
    {
        int sizeInt = rnd.Next(0, 47);


        Debug.Log("sizeInt: " + sizeInt);

        float sizeFloat = sizeInt / 1000f;

        Debug.Log("sizeFloat: " + sizeFloat);

        return sizeFloat;
    }

    float randomDeterminer()
    {
        float speed = rnd.Next(5, 20);


        if (speed == 0)
        {
            speed = 20;
        }
        speed = speed / speedDivider;

        return speed;
    }

    int leftOrRightDeterminer()
    {
        int leftOrRightVar = rnd.Next(0, 2);

        if (leftOrRightVar == 0)
        {
            leftOrRightVar = -1;
        }
        else if (leftOrRightVar == 1)
        {
            leftOrRightVar = 1;
        }

        return leftOrRightVar;
    }

    void moveObject()
    {



        /*float distCovered = (Time.time - startTime) * moveSpeed;

        float fracJourney = distCovered / journeyLength;*/

        float speedCoef = Time.time * moveSpeed;

        transform.position = Vector3.Lerp(firstPos, secondPos, Mathf.PingPong(speedCoef, 1));

        /*if (transform.position == secondPos)
        {
            secondPos = new Vector3(firstPos.x, transform.position.y, transform.position.z);
            firstPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            distCovered = (Time.time - startTime) * moveSpeed;

            fracJourney = distCovered / journeyLength;

            transform.position = Vector3.Lerp(firstPos, secondPos, fracJourney);

            //transform.position = new Vector3(firstPos.x, transform.position.y, transform.position.z);

            Debug.Log("First Position: " + firstPos);
            Debug.Log("Second Position: " + secondPos);

        }*/

        //Debug.Log("is moving");
        
        /*tempPos = transform.position;
        tempPos.x += (leftOrRightNo * moveSpeed);

        transform.position = tempPos;*/
        
        
    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BounceZone")
        {
            Debug.Log("hit the bounce zone");
        }
    }*/


}
