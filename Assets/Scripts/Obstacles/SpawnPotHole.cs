using System.Collections;
using UnityEngine;

public class SpawnPotHole : MonoBehaviour
{
    private float delaySeconds = 5f;
    private float intervalSeconds = 3f;
    private float effectSeconds = 4f;

    private void Start()
    {
        InvokeRepeating("SpawnCoroutine", delaySeconds, intervalSeconds);
    }

    private void SpawnCoroutine()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        int destination = Random.Range(0, GameManager.Instance.innerList.Count);
        GameManager.Instance.innerList[destination].SetActive(false);
        yield return new WaitForSeconds(effectSeconds);
        GameManager.Instance.innerList[destination].SetActive(true);
    }
}