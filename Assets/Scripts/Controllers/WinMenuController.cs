using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinMenuController : MonoBehaviour
{
    public TMP_Text totalTreasure;

    // Start is called before the first frame update
    void Start()
    {
        totalTreasure.text = MySceneManager.instance.totalTreasureRemaining.ToString() + " gold";
    }

    public void MainMenu()
    {
        MySceneManager.instance.ChangeScene(MySceneManager.SceneState.MAIN);
    }    

}
