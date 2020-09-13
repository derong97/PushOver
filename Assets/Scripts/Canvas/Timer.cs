using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public static Timer instance;

    public Image loadingBar;
    public Text timeText;

    [SerializeField]
    private int gameMinutes, gameSeconds, remainingGameSeconds, maxGameSeconds;

    private void Awake()
    {
        instance = this;
    }

    private void Start () {
        gameMinutes = GameManager.Instance.totalGameSeconds / 60;
        gameSeconds = GameManager.Instance.totalGameSeconds % 60;
        timeText.text = gameMinutes + ":" + gameSeconds.ToString ("D2");
        maxGameSeconds = remainingGameSeconds = gameMinutes * 60 + gameSeconds;
    }

    private void CountDown()
    {
        remainingGameSeconds--;
        gameMinutes = remainingGameSeconds / 60;
        gameSeconds = remainingGameSeconds % 60;
        timeText.text = gameMinutes + ":" + gameSeconds.ToString("D2");
        FillLoadingBar();

        if (remainingGameSeconds <= 0)
        {
            CancelInvoke("CountDown");
        }
    }

    private void FillLoadingBar () 
    {
        loadingBar.fillAmount = (float) remainingGameSeconds / maxGameSeconds;
    }

    private int GetRemainingTime()
    {
        return remainingGameSeconds;
    }

    public void StartTimer()
    {
        InvokeRepeating("CountDown", 0f, 1f); // starts countdown immediately in 1s interval
    }
}