using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClockUI : MonoBehaviour
{
    private const float REAL_SECONDS_PER_INGAME_DAY = 60f;
    public Transform clockHourHandTransform;
    public Transform clockMinuteHandTransform;
    public TMP_Text timeText;
    private float day;
    private void Awake()
    {
     
    }

    private void Update()
    {
        day += Time.deltaTime / REAL_SECONDS_PER_INGAME_DAY;
        float dayNormalized = day % 1f;
        float rotationDegreesPerDay = 360f;
        clockHourHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized*rotationDegreesPerDay);
        float hoursPerDay = 24f;
        clockMinuteHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreesPerDay * hoursPerDay);
        string hoursString = Mathf.Floor(dayNormalized * hoursPerDay).ToString("00");
        float minutesPerHour =  60f;
        string minutesString = Mathf.Floor(((dayNormalized * hoursPerDay) % 1f) * minutesPerHour).ToString("00");
        timeText.text = hoursString + ":" + minutesString;
    }
}
