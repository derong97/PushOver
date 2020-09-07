using System.Collections;
using UnityEngine;

public class SpawnPotHole : TimedSpawn
{
    private void Awake()
    {
        minRespawnSeconds = 8f;
        maxRespawnSeconds = 15f;
    }

    protected override IEnumerator Spawn()
    {
        GameObject go = BlockManager.Instance.GetRandomBlock();
        if(go != null)
        {
            BlockManager.Instance.InactivateBlock(go);
        }
        yield return 0;
    }
}