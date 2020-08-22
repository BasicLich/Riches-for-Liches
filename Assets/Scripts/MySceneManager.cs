using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public enum SceneState
    {
        MAIN,
        GAME,
        WIN,
        LOSS
    }

    public static MySceneManager instance;
    public SceneState sceneState = SceneState.MAIN;
    public int totalTreasureRemaining = 0;
    public Animator transition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        transition = GameObject.Find("SceneTransition").GetComponent<Animator>();
        AudioManager.instance.Play("Opening Theme");
    }

    public void ChangeScene(SceneState _sceneState)
    {
        switch (_sceneState)
        {
            case SceneState.MAIN:
                {
                    if (sceneState == SceneState.WIN)
                        AudioManager.instance.Stop("Victory Theme");
                    else if (sceneState == SceneState.LOSS)
                        AudioManager.instance.Stop("Loss Theme");
                    transition = GameObject.Find("SceneTransition").GetComponent<Animator>();
                    StartCoroutine(TransitionScene("MenuScene", 1.0f, "Opening Theme"));
                    sceneState = _sceneState;
                    break;
                }
            case SceneState.GAME:
                {
                    AudioManager.instance.Stop("Opening Theme");
                    transition = GameObject.Find("SceneTransition").GetComponent<Animator>();
                    StartCoroutine(TransitionScene("GameScene", 1.0f, "In-Game Theme"));
                    sceneState = _sceneState;
                    break;
                }
            case SceneState.WIN:
                {
                    AudioManager.instance.Stop("In-Game Theme");
                    transition = GameObject.Find("SceneTransition").GetComponent<Animator>();
                    StartCoroutine(TransitionScene("WinScene", 1.0f, "Victory Theme"));
                    sceneState = _sceneState;
                    break;
                }
            case SceneState.LOSS:
                {
                    AudioManager.instance.Stop("In-Game Theme");
                    transition = GameObject.Find("SceneTransition").GetComponent<Animator>();
                    StartCoroutine(TransitionScene("LossScene", 1.0f, "Loss Theme"));
                    sceneState = _sceneState;
                    break;
                }
        }

        IEnumerator TransitionScene(string _sceneName, float _transitionTime, string _sceneMusicName)
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(_transitionTime);
            SceneManager.LoadScene(sceneName: _sceneName);
            AudioManager.instance.Play(_sceneMusicName);
        }
    }
}
