using System.Collections;
using UnityEngine;

public class SpawnFire : MonoBehaviour
{
    public GameObject firePrefab;
    private float delaySeconds = 4f;
    private float intervalSeconds = 5f;
    private float effectSeconds = 7f;

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
        int index = Random.Range(0, GameManager.Instance.innerList.Count);
        GameObject targetGO = GameManager.Instance.innerList[index];
        Vector3 destination = targetGO.transform.position + new Vector3(0, targetGO.transform.lossyScale.y, 0);
        GameObject clone = Instantiate(firePrefab, destination, Quaternion.identity);
        clone.transform.SetParent(transform);

        yield return new WaitForSeconds(effectSeconds);

        Destroy(clone);
    }
}