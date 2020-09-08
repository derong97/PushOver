using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour {

    private bool check = false;
    public GameObject firePrefab;
    public GameObject fallingBlockPrefab;

    private GameObject fireInstantiation;
    private GameObject fallingBlockInstantiation;

    private List<GameObject> outerList;
    private List<GameObject> innerList;
    private List<GameObject> spawnLeft;

    void Start () {
        fireInstantiation = Spawn(firePrefab, Vector3.zero, transform);
        fallingBlockInstantiation = Spawn(fallingBlockPrefab, Vector3.zero, transform);

        outerList = new List<GameObject> (GameObject.FindGameObjectsWithTag ("Outer"));
        innerList = new List<GameObject> (GameObject.FindGameObjectsWithTag ("Inner"));
        // Spawn second border and check when tiles fall
        // spawnLeft consist of both second and first border
        spawnLeft = innerList;
        InvokeRepeating ("SpawnFire", 0f, 5f);
        InvokeRepeating ("SpawnPothole", 0f, 5f);
        InvokeRepeating ("SpawnFallingBlock", 0f, 2f);
    }


    void Update () {
        
        if (Timer.getRemainingTime() <= 10 && !check) {
            CancelInvoke ("SpawnFallingBlock");
            check = true;
            float countdown = 10f / spawnLeft.Count;
            InvokeRepeating ("SpawnFallingBlock", 0f, countdown);
        }
        if (spawnLeft.Count == 0)
        {
            CancelInvoke("SpawnFallingBlock");
        }
    }
    private GameObject Spawn(GameObject go, Vector3 pos, Transform parent)
    {
        GameObject clone = Instantiate(go, pos, Quaternion.Euler(0, 180, 0));
        clone.transform.SetParent(parent);
        return clone;
    }

    private void SpawnFire () {
        int destination = Random.Range (0, innerList.Count);
        Spawn (firePrefab, innerList[destination].transform.position, fireInstantiation.transform);
    }

    private void SpawnFallingBlock () {
        if (outerList.Count != 0) {
            int destination = Random.Range (0, outerList.Count);
            Spawn (fallingBlockPrefab, outerList[destination].transform.position, fallingBlockInstantiation.transform);
            outerList.RemoveAt (destination);
        } else {
            int destination = Random.Range (0, spawnLeft.Count);
            Spawn (fallingBlockPrefab, spawnLeft[destination].transform.position, fallingBlockInstantiation.transform);
            spawnLeft.RemoveAt (destination);
        }
    }

    private void SpawnPothole () {
        StartCoroutine (ChoosePothole ());
    }
    private IEnumerator ChoosePothole()
    {
        int destination = Random.Range(0, innerList.Count);
        innerList[destination].SetActive(false);
        yield return new WaitForSeconds(2);
        innerList[destination].SetActive(true);
    }
}