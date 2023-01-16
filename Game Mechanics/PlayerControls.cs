using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    //Timer and Delay
    [SerializeField]
    private float ResponseTime;
    [SerializeField]
    private float Delay;
    private float Timer;
    private bool TimerState;
    [SerializeField]
    private float TimeResetGame;
    [SerializeField]
    private float TimePutCupBack;

    //Players
    private bool Player1Turn;
    private bool Player2Turn;
    private int Player1Score;
    private int Player2Score;

    //Sprites
    [SerializeField]
    private GameObject LeftHand;
    [SerializeField]
    private GameObject RightHand;

    //Animations
    private Animator LeftHandAnim;
    private Animator RightHandAnim;
    [SerializeField]
    private Animator Cup;
    [SerializeField]
    private Animator EggAnim;

    //Keys
    private int TimesKeyPressed;
    private bool KeyOnePressed;
    private bool KeyTwoPressed;

    //Other Bools
    private bool CanHitEgg;
    private bool CoroutineDone;

    //Coroutine References
    private Coroutine KeyOnePress = null;
    private Coroutine KeyTwoPress = null;

    //Score
    [SerializeField]
    private GameObject Green1;
    [SerializeField]
    private GameObject Green2;
    [SerializeField]
    private GameObject Green3;
    [SerializeField]
    private GameObject Red1;
    [SerializeField]
    private GameObject Red2;
    [SerializeField]
    private GameObject Red3;

    private void Start()
    {
        Red1.SetActive(false);
        Red2.SetActive(false);
        Red3.SetActive(false);
        Green1.SetActive(false);
        Green2.SetActive(false);
        Green3.SetActive(false);
        Timer = 0f;
        TimerState = false;
        Player1Turn = true;
        Player2Turn = false;
        Player1Score = 0;
        Player2Score = 0;
        TimesKeyPressed = 0;        
        KeyOnePressed = false;
        KeyTwoPressed = false;
        CanHitEgg = false;
        CoroutineDone = false;
        LeftHandAnim = LeftHand.GetComponent<Animator>();
        RightHandAnim = RightHand.GetComponent<Animator>();
        LeftHand.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1f);
        RightHand.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    }

    private void Update()
    {

        if (CanHitEgg)
        {
            Timer += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.M) && Player1Turn && Timer < ResponseTime) // If Pollution Hits The Egg
            {
                CanHitEgg = false;
                TimerState = false;
                if (KeyOnePress != null)
                {
                    StopCoroutine(KeyOnePress);
                }
                if (KeyTwoPress != null)
                {
                    StopCoroutine(KeyTwoPress);
                }
                RightHandAnim.SetTrigger("SmackEgg");
                Player1Score += 1;
                if (Player1Score == 1)
                {
                    Green1.SetActive(true);
                }
                else if (Player1Score == 2)
                {
                    Green2.SetActive(true);
                }
                else if (Player1Score == 3)
                {
                    Green3.SetActive(true);
                }
                if (Player1Score >= 3)
                {
                    Invoke("NatureWon", 2f);
                }
                Invoke("ResetGame", TimeResetGame);
            }

            if (Input.GetKeyDown(KeyCode.Z) && Player2Turn && Timer < ResponseTime) // If Nature Hits The Egg
            {
                CanHitEgg = false;
                TimerState = false;
                if (KeyOnePress != null)
                {
                    StopCoroutine(KeyOnePress);
                }
                if (KeyTwoPress != null)
                {
                    StopCoroutine(KeyTwoPress);
                }
                LeftHandAnim.SetTrigger("SmackEgg");
                Player2Score += 1;
                if (Player2Score == 1)
                {
                    Red1.SetActive(true);
                }
                else if (Player2Score == 2)
                {
                    Red2.SetActive(true);
                }
                else if (Player2Score == 3)
                {
                    Red3.SetActive(true);
                }
                if (Player2Score >= 3)
                {
                    Invoke("PollutionWon", 2f);
                }
                Invoke("ResetGame", TimeResetGame);
            }
            if (Timer > ResponseTime && CanHitEgg) //If Player Didnt Hit Egg
            {
                if (Player1Turn)
                {
                    RightHandAnim.SetTrigger("SmackTable");
                    Invoke("ResetLeftHand", TimePutCupBack);
                }
                if (Player2Turn)
                {
                    LeftHandAnim.SetTrigger("SmackTable");
                    Invoke("ResetRightHand", TimePutCupBack);
                }
                ResetTurn();
            }
        }

        if (TimerState) // If Timer Starts / Is Needed by Setting Bool To True
        {
            Timer += Time.deltaTime;
            if (Timer >= Delay && CoroutineDone)
            {
                ResetTurn();
            }
        }

        if (GameManager.Instance.IsPaused == false)
        {
            if (Player1Turn)
            {
            if (Input.GetKeyDown(KeyCode.Z) && TimesKeyPressed < 2)
            {            
                Debug.Log("Player1");
                TimesKeyPressed += 1;
            }

            if (Input.GetKeyDown(KeyCode.Z) && TimesKeyPressed == 1 && !KeyOnePressed) // If Player Presses Key, it's the first time pressing and has never pressed before
            {
                FindObjectOfType<AudioManager>().Play("HandNature");
                KeyOnePressed = true;
                Timer = 0f;
                Debug.Log("Pressed once");
                KeyOnePress = StartCoroutine(OnePress(Delay));
                TimerState = true;
            }

            if (Input.GetKeyDown(KeyCode.Z) && TimesKeyPressed == 2 && !CanHitEgg && !KeyTwoPressed) // If Player Presses Key, it's the first time pressing, he can hit the egg and has pressed before
            {
                if (Timer < Delay)
                {
                    KeyTwoPressed = true;
                    Debug.Log("Pressed twice");
                    CanHitEgg = true;
                    TimerState = false;
                    Timer = 0f;
                    CoroutineDone = false;
                    KeyTwoPress = StartCoroutine(TwoPress(Delay));
                }
            }

        }

            if (Player2Turn)
        {
            if (Input.GetKeyDown(KeyCode.M) && TimesKeyPressed < 2)
            {
                Debug.Log("Player2");
                TimesKeyPressed += 1;
            }

            if (Input.GetKeyDown(KeyCode.M) && TimesKeyPressed == 1 && !KeyOnePressed) // If Player Presses Key, it's the first time pressing and has never pressed before
            {
                FindObjectOfType<AudioManager>().Play("HandPollution");
                KeyOnePressed = true;
                Timer = 0f;
                Debug.Log("Pressed once");
                KeyOnePress = StartCoroutine(OnePress(Delay));
                TimerState = true;
            }

            if (Input.GetKeyDown(KeyCode.M) && TimesKeyPressed == 2 && !CanHitEgg && !KeyTwoPressed) // If Player Presses Key, it's the first time pressing, he can hit the egg and has pressed before
            {
                if (Timer < Delay)
                {
                    KeyTwoPressed = true;
                    Debug.Log("Pressed twice");
                    CanHitEgg = true;
                    TimerState = false;
                    Timer = 0f;
                    CoroutineDone = false;
                    KeyTwoPress = StartCoroutine(TwoPress(Delay));
                }
            }
        }

        }
        
    }

    private void ResetTurn()
    {
        EggAnim.SetBool("EggPollution", false);
        EggAnim.SetBool("EggNature", false);
        TimerState = false;
        Timer = 0;
        TimesKeyPressed = 0;
        CoroutineDone = false;
        if (KeyOnePress != null)
        {
            StopCoroutine(KeyOnePress);
        }
        if (KeyTwoPress != null)
        {
            StopCoroutine(KeyTwoPress);
        }
        CanHitEgg = false;
        Player1Turn = Player2Turn;
        Player2Turn = !Player1Turn;
        KeyOnePressed = false;
        KeyTwoPressed = false;
    }
    private void ResetGame()
    {
        EggAnim.SetBool("EggPollution", false);
        EggAnim.SetBool("EggNature", false);
        LeftHandAnim.SetBool("isGrabbing", false);
        RightHandAnim.SetBool("isGrabbing", false);
        TimerState = false;
        Timer = 0;
        TimesKeyPressed = 0;
        CoroutineDone = false;
        if (KeyOnePress != null)
        {
            StopCoroutine(KeyOnePress);
        }   
        if (KeyTwoPress != null)
        {
            StopCoroutine(KeyTwoPress);
        }
        CanHitEgg = false;
        Player1Turn = Player2Turn;
        Player2Turn = !Player1Turn;
        KeyOnePressed = false;
        KeyTwoPressed = false;
    }

    private void ResetLeftHand()
    {
        LeftHandAnim.SetBool("isGrabbing", false);
    }

    private void ResetRightHand()
    {
        RightHandAnim.SetBool("isGrabbing", false);
    }

    private void NatureWon()
    {
        UIManager.Instance.PauseButton(false);
        UIManager.Instance.ShowNatureVictoryPanel(true);
        Time.timeScale = 0f;
    }

    private void PollutionWon()
    {
        UIManager.Instance.PauseButton(false);
        UIManager.Instance.ShowPollutionVictoryPanel(true);
        Time.timeScale = 0f;
    }
    private IEnumerator OnePress(float Delay)
    {
        yield return new WaitForSeconds(Delay);
        CoroutineDone = true;

        if (KeyOnePressed  == false)
        {
            yield break;
        }
        if (Player1Turn)
        {
            RightHand.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1f);
            LeftHand.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            LeftHandAnim.SetTrigger("Smack");
            RightHandAnim.ResetTrigger("Smack");
        }
        if (Player2Turn)
        {            
            LeftHand.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1f);
            RightHand.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            RightHandAnim.SetTrigger("Smack");
            LeftHandAnim.ResetTrigger("Smack");
        }
    }
    private IEnumerator TwoPress(float Delay)
    {
        StopCoroutine(KeyOnePress);
        CoroutineDone = true;

        if (KeyTwoPressed == false)
        {
            yield break;
        }

        if (Player1Turn)
        {
            RightHand.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1f);
            LeftHand.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            LeftHandAnim.SetBool("isGrabbing", true);

        }
        if (Player2Turn)
        {
            LeftHand.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1f);
            RightHand.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            RightHandAnim.SetBool("isGrabbing", true);
        }
        yield return new WaitForSeconds(Delay);
    }

}
