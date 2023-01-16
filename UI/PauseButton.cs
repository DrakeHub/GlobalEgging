using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField]
    private bool IsOpen = false;

    public void SetPause()
    {
        AudioManager.instance.Play("Button");
        UIManager.Instance.ShowPanelPause(IsOpen);
        GameManager.Instance.PauseGame(IsOpen);
        
    }
}
