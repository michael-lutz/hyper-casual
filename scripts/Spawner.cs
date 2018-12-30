using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject Prefab;

    public GameManager GameManagerScript;

    GameObject instnantiatedObj;

    static System.Random rnd = new System.Random();

    //public float spawnHeightDifference = 4;

    public Transform player;

    public float spawnHeight = 5;

    Vector3 playerPos = Vector3.zero;

    bool resetPlayerPos = false;

    public void DestroyObjects()
    {
        if (GameManagerScript.gameOverConfirmed)
        {


            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("BarZone");
            foreach (GameObject target in gameObjects)
            {
                GameObject.Destroy(target);
            }
        }
    }


    void Update()
    {
        DestroyObjects();

        if (resetPlayerPos == false)
        {
            playerPos.y = player.position.y;

            resetPlayerPos = true;
        }

        if (resetPlayerPos)
        {

            if (player.position.y > playerPos.y + rnd.Next(4,14))
            {
                float newSpawnHeight = player.position.y + spawnHeight;
                instnantiatedObj = Instantiate(Prefab, new Vector3(player.position.x, newSpawnHeight, player.position.z), Quaternion.identity);
                resetPlayerPos = false;
                Destroy(instnantiatedObj, 15);
            }
        }

        if (GameManagerScript.gameOverConfirmed)
        {
            playerPos.y = player.position.y;
        }
    }
}
