using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour {

    public GameObject timer;
    Timer time;
    bool check = false;
    public GameObject fire;
    public Transform fireObstacles;
    public GameObject tile;
    public Transform tileObstacles;
    GameObject[] spawnPoints;
    List<GameObject> spawnLeft;

    void Start () {
        time = timer.GetComponent<Timer> ();
        spawnPoints = GameObject.FindGameObjectsWithTag ("SpawnPoint");
        spawnLeft = new List<GameObject> (spawnPoints);
        InvokeRepeating ("spawnFire", 0f, 5f);
        InvokeRepeating ("spawnPothole", 0f, 5f);
        InvokeRepeating ("spawnTile", 0f, 2f);
    }

    void Update () {
        if (time.sec == 10 && !check) {
            CancelInvoke ("spawnTile");
            check = true;
            float countdown = 10f / spawnLeft.Count;
            Debug.Log (countdown);
            InvokeRepeating ("spawnTile", 0f, countdown);
        }
    }

    private void spawnFire () {
        int destination = chooseDestination (spawnPoints.Length);
        spawn (fire, spawnPoints[destination].transform.position, fireObstacles);
    }

    private void spawnTile () {
        int destination = chooseDestination (spawnLeft.Count);
        spawn (tile, spawnLeft[destination].transform.position, tileObstacles);
        spawnLeft.RemoveAt (destination);
    }

    private void spawnPothole () {
        StartCoroutine (choosePothole ());
    }

    private int chooseDestination (int length) {
        return Random.Range (0, length);
    }

    private void spawn (GameObject obj, Vector3 pos, Transform parent) {
        GameObject clone = Instantiate (obj, pos, Quaternion.Euler (0, 180, 0));
        clone.transform.SetParent (parent);
    }

    private IEnumerator choosePothole () {
        int destination = chooseDestination (spawnPoints.Length);
        spawnPoints[destination].SetActive (false);
        yield return new WaitForSeconds (2);
        spawnPoints[destination].SetActive (true);
    }

}