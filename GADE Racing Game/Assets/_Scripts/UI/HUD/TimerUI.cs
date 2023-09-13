using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerUI : MonoBehaviour
{
    //Text must be on separate game objects
    [SerializeField] private TextMeshProUGUI timerText;
    [Space]
    [SerializeField] private TextMeshProUGUI addTimeText;
    private Vector3 addTimeStartPosition;
    [SerializeField] private Vector3 addTimeEndPosition;
    [SerializeField] private float addTimeMoveDuration;

    private void Awake()
    {
        timerText.gameObject.SetActive(false);
        addTimeText.gameObject.SetActive(false);
        addTimeStartPosition = addTimeText.transform.position;
    }

    public void ShowTimer()
    {
        timerText.gameObject.SetActive(true);
    }


    private int minutes;
    private int seconds;
    private string formattedTime;


    public void UpdateTimerUI(float currentTime)
    {
        minutes = Mathf.FloorToInt(currentTime / 60);
        seconds = Mathf.FloorToInt(currentTime % 60);

        formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        timerText.text = formattedTime;
    }

    public void TimeAdded(float addedTime)
    {
        int seconds = Mathf.FloorToInt(addedTime % 60);
        addTimeText.text = $"+ {seconds}s";
        StartCoroutine(nameof(MoveTimeAdded));
    }
    private IEnumerator MoveTimeAdded()
    {
        addTimeText.transform.position = addTimeStartPosition;
        addTimeText.gameObject.SetActive(true);
        yield return new WaitForSeconds(addTimeMoveDuration);
        addTimeText.gameObject.SetActive(false);
    }

}
