using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseMenuController : MonoBehaviour
{
    public void MainMenu()
    {
        MySceneManager.instance.ChangeScene(MySceneManager.SceneState.MAIN);
    }

}
