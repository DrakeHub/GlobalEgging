using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    public void LoadMainMenu()
    {
        UIManager.Instance.ShowNatureVictoryPanel(false);
        UIManager.Instance.ShowPollutionVictoryPanel(false);
        UIManager.Instance.ShowGameCanvas(false);

        AudioManager.instance.Play("Button");
        GameManager.Instance.LoadLevel(0);
    }
}
