using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsButton : MonoBehaviour
{
    public void LoadControlsMenu()
    {
        AudioManager.instance.Play("Button");
        GameManager.Instance.LoadLevel(1);
    }
}
