using UnityEngine;

public class SpawnFallingBlock : MonoBehaviour
{
    /*
    public GameObject fallingBlockPrefab;
    private float delaySeconds = 0f;
    private float intervalSeconds = 2f;

    private void Start()
    {
        //InvokeRepeating("Spawn", delaySeconds, intervalSeconds);
    }
    private void Spawn()
    {
        int index = Random.Range(0, GameManager.Instance.innerList.Count);
        GameObject targetGO = GameManager.Instance.innerList[index];
        Vector3 destination = targetGO.transform.position + new Vector3(0, targetGO.transform.lossyScale.y, 0);
        GameObject clone = Instantiate(fallingBlockPrefab, destination, Quaternion.identity);
        clone.transform.SetParent(transform);
    }
    */
}