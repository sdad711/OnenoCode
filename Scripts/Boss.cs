using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Boss : MonoBehaviour
{
    public AudioManager am;
    public SkeletonAnimation animator;
    public AnimationReferenceAsset idle, attack, damaged;
    public Transform spawnPoint;
    public GameObject missile;
    public float time;
    string currentAnimation;
    string currentState;
    public bool bossDead;
    private void Start()
    {
        StartCoroutine(AttackAnimation());
        currentState = "Idle";
        SetCharacterState(currentState);
    }
    IEnumerator AttackAnimation()
    {
        while (!bossDead)
        {
            yield return new WaitForSeconds(time);
            SetCharacterState("Attack");
            am.BossReadySFX();
            Invoke("Spawn", 2.5f);
        }
    }
    public void Spawn()
    {     
        Instantiate(missile, spawnPoint.position, Quaternion.identity);
        SetCharacterState("Idle");
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
        if (state.Equals("Idle"))
            SetAnimation(idle, true, 1f);
        if (state.Equals("Attack"))
            SetAnimation(attack, true, 1f);
        if (state.Equals("Damaged"))
            SetAnimation(damaged, true, 1f);
    }
    private void Update()
    {
        if(bossDead)
        {
           SetCharacterState("Damaged");
        }
    }

}
