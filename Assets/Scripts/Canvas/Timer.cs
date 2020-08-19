using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public Image LoadingBar;
    public Text timeText;
    public int min;
    public int sec;
    int totalsec = 0;
    int total = 0;

    private void Start () {
        timeText.text = min + ":" + sec.ToString ("D2");
        if (min > 0) totalsec += min * 60;
        if (sec > 0) totalsec += sec;
        total = totalsec;
        StartCoroutine (second ());
    }

    private void Update () {
        if (sec == 0 && min == 0) {
            StopCoroutine (second ());
        }
    }

    IEnumerator second () {
        yield return new WaitForSeconds (1f);
        if (sec > 0) sec--;
        if (sec == 0 && min != 0) {
            sec = 60;
            min--;
        }
        timeText.text = min + ":" + sec.ToString ("D2");
        fillLoadingBar ();
        StartCoroutine (second ());
    }

    private void fillLoadingBar () {
        totalsec--;
        float fill = (float) totalsec / total;
        LoadingBar.fillAmount = fill;
    }
}