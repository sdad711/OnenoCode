using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Monolith : MonoBehaviour
{
    public Boss boss;
    public GameManager gm;
    public AudioManager am;
    public SkeletonAnimation animator;
    public AnimationReferenceAsset faseOne, transitionOne, faseTwo, transitionTwo, faseThree, shoot;
    string currentState;
    string currentAnimation;
    public int monolithPhase = 1;
    private void Start()
    {
        currentState = "FaseOne";
        SetCharacterState(currentState);
    }
    public void SetAnimation(AnimationReferenceAsset animationName, bool loop, float timeScale)
    {
        if (animationName.name.Equals(currentAnimation))
            return;
        animator.state.SetAnimation(0, animationName, loop).TimeScale = timeScale;
        currentAnimation = animationName.name;
    }
    public void SetCharacterState(string state)
    {
        if (state.Equals("FaseOne"))
            SetAnimation(faseOne, true, 1f);
        if (state.Equals("TransitionOne"))
            SetAnimation(transitionOne, false, 1f);
        if (state.Equals("FaseTwo"))
            SetAnimation(faseTwo, true, 1f);
        if (state.Equals("TransitionTwo"))
            SetAnimation(transitionTwo, false, 1f);
        if (state.Equals("FaseThree"))
            SetAnimation(faseThree, true, 1f);
        if (state.Equals("Shoot"))
            SetAnimation(shoot, false, 1f);
    }
    public void BuildMonolith()
    {
        if(monolithPhase == 1)
        {
            ChangeToPhaseTwo();
            monolithPhase++;
        }
        else if(monolithPhase == 2)
        {
            ChangeToPhaseThree();
            monolithPhase++;
        }
        else if (monolithPhase == 3)
        {
            ChangeToShoot();
            monolithPhase++;
        }
    }
    public void ChangeToPhaseTwo()
    {
        SetCharacterState("TransitionOne");
        am.Monolith1();
        Invoke("PhaseTwo", 2);
    }
    public void PhaseTwo()
    {
        SetCharacterState("FaseTwo");
    }
    public void ChangeToPhaseThree()
    {
        SetCharacterState("TransitionTwo");
        am.Monolith2();
        Invoke("PhaseThree", 3);
    }
    public void PhaseThree()
    {
        SetCharacterState("FaseThree");
    }
    public void ChangeToShoot()
    {
        am.Monolith3();
        Invoke("Shoot", 0.5f);
    }
    public void Shoot()
    {
        SetCharacterState("Shoot");
        Invoke("BossHit", 2.5f);
    }
    public void BossHit()
    {
        boss.bossDead = true;
        am.BossDefeatedSFX();
        Invoke("EndGame", 2);
    }
    public void EndGame()
    {
        gm.bossDefeated = true;
    }
}
