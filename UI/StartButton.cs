using UnityEngine;

public class StartButton : MonoBehaviour
{

    public void LoadGame()
    {
        Time.timeScale = 1f;
        UIManager.Instance.ShowNatureVictoryPanel(false);
        UIManager.Instance.ShowPollutionVictoryPanel(false);        
        UIManager.Instance.ShowGameCanvas(true);
        UIManager.Instance.PauseButton(true);
        AudioManager.instance.Play("Button");
        GameManager.Instance.LoadLevel(3);
    }
}
