using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> outerList;
    public List<GameObject> innerList;
    public List<GameObject> spawnLeft;
    public int gameMinutes;
    public int gameSeconds;

    private static GameManager _instance;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else // in case you create multiple game objects with GameManager script
        {
            Destroy(gameObject);
        }
        
        outerList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Outer"));
        innerList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Inner"));
        spawnLeft = innerList;
        gameMinutes = 0; // TODO: get from start screen
        gameSeconds = 30; // TODO: get from start screen
    }

    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if(_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(GameManager).Name;
                    _instance = go.AddComponent<GameManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }
}
