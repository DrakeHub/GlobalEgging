using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButton : MonoBehaviour
{
    public void LoadCreditsMenu()
    {
        AudioManager.instance.Play("Button");
        GameManager.Instance.LoadLevel(2);
    }
}
