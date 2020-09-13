using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int totalGameSeconds;
    public GamePreferences.SuddenDeathMode suddenDeathMode;

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
        totalGameSeconds = PlayerPrefs.GetInt("totalGameSeconds");
        suddenDeathMode = (GamePreferences.SuddenDeathMode) PlayerPrefs.GetInt("suddenDeathMode");
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
                    GameObject go = new GameObject
                    {
                        name = typeof(GameManager).Name
                    };
                    _instance = go.AddComponent<GameManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }
}
