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

    private bool timeRunning = false;

    [SerializeField]
    public TMP_Text timerText;

    [SerializeField]
    public Player player;

    private void Update()
    {
        if (timeRunning) {
            currentTime += Time.deltaTime * timeMultiplier;

            min = Mathf.FloorToInt(currentTime / 60F);
            sec = Mathf.FloorToInt(currentTime - min * 60);

            string niceTime = string.Format("{0:0}:{1:00}", min, sec);

            timerText.text = niceTime;
        }

        if (player.horizontal != 0) {
            timeRunning = true;
        }
        /*else
        {
            timeRunning = false;
        }*/

       
    }


}
