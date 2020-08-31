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
    List<GameObject> spawnBorder;
    List<GameObject> spawnPoints;
    List<GameObject> spawnLeft;

    void Start () {
        time = timer.GetComponent<Timer> ();
        spawnBorder = new List<GameObject> (GameObject.FindGameObjectsWithTag ("SpawnBorder"));
        spawnPoints = new List<GameObject> (GameObject.FindGameObjectsWithTag ("SpawnPoint"));
        // Spawn second border and check when tiles fall
        // spawnLeft consist of both second and first border
        spawnLeft = spawnPoints;
        Debug.Log (spawnBorder.Count);
        Debug.Log (spawnPoints.Count);
        Debug.Log (spawnLeft.Count);
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
        if (spawnLeft.Count == 0) CancelInvoke ("spawnTile");
    }

    private void spawnFire () {
        int destination = chooseDestination (spawnPoints.Count);
        spawn (fire, spawnPoints[destination].transform.position, fireObstacles);
    }

    private void spawnTile () {
        if (spawnBorder.Count != 0) {
            int destination = chooseDestination (spawnBorder.Count);
            spawn (tile, spawnBorder[destination].transform.position, tileObstacles);
            spawnBorder.RemoveAt (destination);
        } else {
            int destination = chooseDestination (spawnLeft.Count);
            spawn (tile, spawnLeft[destination].transform.position, tileObstacles);
            spawnLeft.RemoveAt (destination);
        }
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
        int destination = chooseDestination (spawnPoints.Count);
        spawnPoints[destination].SetActive (false);
        yield return new WaitForSeconds (2);
        spawnPoints[destination].SetActive (true);
    }

}