using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        MySceneManager.instance.ChangeScene(MySceneManager.SceneState.GAME);
    }   
    
    public void ExitGame()
    {
        Application.Quit();
    }    
}
