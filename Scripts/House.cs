using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class House : MonoBehaviour
{
    public GameManager gm;
    public MiddleMan middleMan;
    public GameObject HouseOne;
    public GameObject HouseTwo;
    public int value;
    public GameObject glowAnimation, partFour, partThree, partTwo;
    public GameObject crash;
    private void Start()
    {
        crash.SetActive(false);
        value = middleMan.transitionValue;
    }
    private void Update()
    {
        if(value >= 0)
        {
            if(value == 0)
            {
                partTwo.SetActive(false);
                partThree.SetActive(false);
                partFour.SetActive(false);
                glowAnimation.SetActive(false);
            }
            if(value == 1)
            {
                partTwo.SetActive(true);
                partThree.SetActive(false);
                partFour.SetActive(false);
                glowAnimation.SetActive(false);
            }
            else if (value == 2)
            {
                partTwo.SetActive(true);
                partThree.SetActive(true);
                partFour.SetActive(false);
                glowAnimation.SetActive(false);
            }
            else if (value == 3)
            {
                partTwo.SetActive(true);
                partThree.SetActive(true);
                partFour.SetActive(true);
                glowAnimation.SetActive(true);
            }
        }
    }
    public void HouseBuild()
    {
        if (value < 3)
        {
            value += 1;
            if(this.transform.tag == "HouseOne")
            {
                gm.playerOneScore += 100;
                gm.ShowScore();
            }
            if(this.transform.tag == "HouseTwo")
            {
                gm.playerTwoScore += 100;
                gm.ShowScore();
            }
        }
    }
    public void HouseSteal()
    {
        middleMan.transitionValue = value;
        if (this.transform.tag == "HouseOne")
        {
            gm.playerOneScore -= value * 100;
            gm.playerTwoScore += value * 100;
            gm.ShowScore();
            HouseTwo.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else if (this.transform.tag == "HouseTwo")
        {
            gm.playerOneScore += value * 100;
            gm.playerTwoScore -= value * 100;
            gm.ShowScore();
            HouseOne.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
    public void HouseDestroy()
    {
        if (value > 0)
        {
            value -= 1;
            StartCoroutine(Crash());
            if (this.transform.tag == "HouseOne")
            {
                gm.playerOneScore -= 100;
                gm.ShowScore();
            }
            if (this.transform.tag == "HouseTwo")
            {
                gm.playerTwoScore -= 100;
                gm.ShowScore();
            }
        }
    }
    IEnumerator Crash()
    {
        crash.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        crash.SetActive(false);
    }
}
