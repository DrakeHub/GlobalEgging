using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    private Animator ContainerAnim;
    [SerializeField]
    private Animator EggAnim;

    public void PickUpLeft()
    {
        ContainerAnim.SetBool("isCupPickedLeft", true);
    }

    public void PlaceLeft()
    {
        ContainerAnim.SetBool("isCupPickedLeft", false);
    }

    public void PickUpRight()
    {
        ContainerAnim.SetBool("isCupPickedRight", true);
    }

    public void PlaceRight()
    {
        ContainerAnim.SetBool("isCupPickedRight", false);
    }

    public void HitCup()
    {
       FindObjectOfType<AudioManager>().Play("CupHit");
    }

    public void HitEgg()
    {
       FindObjectOfType<AudioManager>().Play("EggHit");
    }

    public void HitTable()
    {
        FindObjectOfType<AudioManager>().Play("TableHit");
    }

    public void EggNature()
    {
        EggAnim.SetBool("EggNature", true);
    }

    public void EggPollution()
    {
        EggAnim.SetBool("EggPollution", true);
    }
}
