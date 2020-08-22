using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool paused = false;
    // UI GameObjects
    public GameObject pauseOverlay;
    public GameObject hudOverlay;

    //Toggle UI and Timescale to Pause
    public void TogglePause()
    {
        if (!paused)
        {
            paused = true;
            pauseOverlay.SetActive(true);
            hudOverlay.SetActive(false);
            Time.timeScale = 0.0f;
        }
        else
        {
            paused = false;
            pauseOverlay.SetActive(false);
            hudOverlay.SetActive(true);
            Time.timeScale = 1.0f;
        }
    }
}
