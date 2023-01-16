using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;

    public bool IsPaused { get; private set; } = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);

    }

    public void PauseGame(bool pause)
    {
        IsPaused = pause;
        if (pause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        UIManager.Instance.ShowPanelPause(pause);
    }

}
