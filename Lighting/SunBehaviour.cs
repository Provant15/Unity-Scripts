/*
    Attach this Script to a Directional Light
    Optionally, you can create a Canvas Text item and attach this for a time-sync'd clock
*/

using UnityEngine;
using UnityEngine.UI;

public class Sun : MonoBehaviour
{
    [Tooltip("Speed (degrees per second) for the entire 24-hour cycle.")]
    public float timeCycleSpeed = 2f;

    [Tooltip("UI Text for displaying the clock.")]
    public Text timeText;

    private float angleTracker = 0f;

    void Start()
    {
        angleTracker = 0f;
    }

    void Update()
    {
        float rotateAmount = timeCycleSpeed * Time.deltaTime;

        angleTracker += rotateAmount;
        if (angleTracker >= 360f) angleTracker -= 360f;
        if (angleTracker < 0f) angleTracker += 360f;

        float physicalAngle = angleTracker + 90f;

        transform.RotateAround(Vector3.zero, Vector3.right, rotateAmount);
        transform.rotation = Quaternion.Euler(physicalAngle, 0f, 0f);
        transform.LookAt(Vector3.zero);

        if (timeText != null)
        {
            UpdateClockUI();
        }
    }

    private void UpdateClockUI()
    {
        float hoursFloat = (angleTracker / 360f) * 24f;
        hoursFloat = (hoursFloat + 12f) % 24f;

        int hours = Mathf.FloorToInt(hoursFloat);
        int minutes = Mathf.FloorToInt((hoursFloat - hours) * 60f);

        bool isPM = (hours >= 12);
        int displayHour = hours % 12;
        if (displayHour == 0) displayHour = 12;

        string period = isPM ? "PM" : "AM";
        timeText.text = string.Format("{0:00}:{1:00} {2}", displayHour, minutes, period);
    }
}
