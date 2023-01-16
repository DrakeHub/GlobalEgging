using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } = null;

    [SerializeField]
    private GameObject naturePanelVictory = null;

    [SerializeField]
    private GameObject pollutionPanelVictory = null;

    [SerializeField]
    private GameObject panelPause = null;

    [SerializeField]
    private GameObject gameCanvas = null;

    [SerializeField]
    private GameObject pauseButton = null;

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
    public void ShowPanelPause(bool value)
    {
        panelPause.SetActive(value);
    }

    public void ShowGameCanvas(bool value)
    {
        gameCanvas.SetActive(value);
    }

    public void ShowNatureVictoryPanel(bool value)
    {
        naturePanelVictory.SetActive(value);
    }

    public void ShowPollutionVictoryPanel(bool value)
    {
        pollutionPanelVictory.SetActive(value);
    }

    public void PauseButton(bool value)
    {
        pauseButton.SetActive(value);
    }

}
