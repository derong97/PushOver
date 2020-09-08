using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    private List<GameObject> blockGOList;
    private List<string> blockNameList; // keeps track of free blocks
    private static BlockManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else // in case you create multiple game objects with GameManager script
        {
            Destroy(gameObject);
        }
        blockGOList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Block"));
        blockNameList = (from block in blockGOList select block.name).ToList();
    }

    public static BlockManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BlockManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject
                    {
                        name = typeof(BlockManager).Name
                    };
                    _instance = go.AddComponent<BlockManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    public GameObject GetRandomBlock()
    {
        int index = Random.Range(0, blockNameList.Count);
        if (blockNameList.Count == 0)
        {
            return null;
        }
        else{
            string name = blockNameList[index];
            return GetNamedBlock(name);
        }
    }

    public GameObject GetNamedBlock(string name)
    {
        return blockGOList.Where(go => go.name == name).SingleOrDefault();
    }

    public void UseBlock(GameObject go)
    {
        blockNameList.Remove(go.name);
    }

    public void FreeBlock(GameObject go)
    {
        blockNameList.Add(go.name);
    }

    public void InactivateBlock(GameObject go)
    {
        UseBlock(go);
        go.SetActive(false);
    }
}
