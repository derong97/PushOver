using UnityEngine;

public class GamePreferences: MonoBehaviour
{
    // TODO: HARDCODED FOR NOW
    private void Start()
    {
        PlayerPrefs.SetInt("gameMinutes", 0);
        PlayerPrefs.SetInt("gameSeconds", 30);
        PlayerPrefs.SetInt("suddenDeathMode", (int) SuddenDeathMode.RANDOM);
        PlayerPrefs.Save();
    }

    public enum SuddenDeathMode
    {
        NORMAL,
        RANDOM
    }
}
