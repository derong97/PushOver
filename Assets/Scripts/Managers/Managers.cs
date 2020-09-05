using UnityEngine;

public class Managers : MonoBehaviour
{
    private void Start()
    {
        _ = GameManager.Instance;
        _ = BlockManager.Instance;
    }
}
