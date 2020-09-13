using UnityEngine;

public class GamePreferences: MonoBehaviour
{
    private int gameSeconds = 30, gameMinutes = 0;

    private void Start()
    {
        PlayerPrefs.SetInt("totalGameSeconds", gameMinutes * 60 + gameSeconds);
        PlayerPrefs.SetInt("suddenDeathMode", (int) SuddenDeathMode.RANDOM);
        PlayerPrefs.Save();
    }

    public enum SuddenDeathMode
    {
        NORMAL,
        RANDOM
    }
}
