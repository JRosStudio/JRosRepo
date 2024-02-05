using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float currentTime = 0;
    private int min;
    private int sec;
    public float timeMultiplier = 1f;

    public bool timeRunning = true;

    [SerializeField]
    public TMP_Text timerText;


    private void Update()
    {
        if (timeRunning) {
            currentTime += Time.deltaTime * timeMultiplier;

            min = Mathf.FloorToInt(currentTime / 60F);
            sec = Mathf.FloorToInt(currentTime - min * 60);

            string niceTime = string.Format("{0:0}:{1:00}", min, sec);

            timerText.text = niceTime;
        }

       
    }


}
