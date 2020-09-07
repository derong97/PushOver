using System.Collections;
using UnityEngine;

public class TimedSpawn : MonoBehaviour
{
    public GameObject prefab;
    protected float minRespawnSeconds;
    protected float maxRespawnSeconds;
    protected float effectSeconds;

    private void Start()
    {
        Invoke("SpawnCoroutine", Random.Range(minRespawnSeconds, maxRespawnSeconds));
    }

    private void SpawnCoroutine()
    {
        StartCoroutine(Spawn());
        Invoke("SpawnCoroutine", Random.Range(minRespawnSeconds, maxRespawnSeconds));
    }

    protected virtual IEnumerator Spawn()
    {
        GameObject targetBlock = BlockManager.Instance.GetRandomBlock();
        if (targetBlock == null)
        {
            yield return 0;
        }
        else
        {
            if(prefab.GetComponent<PowerUp>() != null)
            {
                // there is a need to identify which block to free when player picks up powerup
                // because the wait here is infinite, so block will never be freed in this function
                prefab.GetComponent<PowerUp>().attached_block = targetBlock;
            }
            BlockManager.Instance.UseBlock(targetBlock);
            Vector3 destination = targetBlock.transform.position + new Vector3(0, targetBlock.transform.lossyScale.y, 0);
            GameObject clone = Instantiate(prefab, destination, Quaternion.identity);
            clone.transform.SetParent(transform);

            Debug.Log(clone + " spawned at " + targetBlock);

            yield return new WaitForSeconds(effectSeconds);

            Destroy(clone);
            BlockManager.Instance.FreeBlock(targetBlock);
        }
    }
}