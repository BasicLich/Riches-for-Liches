using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIFade : MonoBehaviour
{
    //Popup Message
    public TMP_Text message;

    //Duration the Message will appear on Screen
    public float timeToFade = 2.5f;

    //How long the Message has been on Screen
    private float timeElapsed;

    private void Update()
    {
        //If the Message has not expired
        if (timeElapsed <= timeToFade)
        {
            //Increment Time Elapsed
            timeElapsed += Time.deltaTime;
            //Shift Message Transparency equal to the amount of time left on the message
            message.color = new Color(message.color.r, message.color.g, message.color.b, (timeToFade - timeElapsed) / timeToFade);
        }
    }

    //Set Message and Reset Timer
    public void SetMessage(string _message)
    {
        message.text = _message;
        timeElapsed = 0.0f;
    }
}
