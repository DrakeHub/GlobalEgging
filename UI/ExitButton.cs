using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void QuitGame()
    {
        AudioManager.instance.Play("Button");
        Application.Quit();
    }
}
