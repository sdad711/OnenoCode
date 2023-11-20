using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerTwoScript : MonoBehaviour
{
    public AudioManager am;
    public House house;
    public Monolith monolith;
    public SkeletonAnimation animator;
    public AnimationReferenceAsset walking, idle, working, attack, damaged;
    Rigidbody2D rb;
    public PlayerOneScript playerOne;
    public GameObject pow;
    public GameObject build;
    public bool buildHouse;
    public bool stealHouse;
    public bool activateMonolith;
    public float buildTime;
    public float stealTime;
    public float monolithTime;
    bool hit;
    bool punch;
    string currentState;
    string currentAnimation;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentState = "Idle";
        SetCharacterState(currentState);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            am.PowPlayerTwoSFX();
            collision.gameObject.SetActive(false);
            StartCoroutine(PlayerHit());
            rb.velocity = new Vector2(0, 0);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "HouseTwo")
        {
            buildHouse = true;
            house = collision.gameObject.GetComponent<House>();
        }
        if (collision.gameObject.tag == "HouseOne")
        {
            stealHouse = true;
            house = collision.gameObject.GetComponent<House>();
        }
        if (collision.gameObject.tag == "Player")
        {
            punch = true;
        }
        if (collision.gameObject.tag == "Monolith")
        {
            activateMonolith = true;
            monolith = collision.gameObject.GetComponent<Monolith>();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "HouseTwo")
        {
            buildHouse = false;
        }
        if (collision.gameObject.tag == "HouseOne")
        {
            stealHouse = false;
        }
        if (collision.gameObject.tag == "Player")
        {
            rb.velocity = new Vector2(0, 0);
            punch = false;
        }
        if (collision.gameObject.tag == "Monolith")
        {
            activateMonolith = false;
        }
    }
    private void Update()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, 5f); //ogranièava brzinu kretanja
        if (!hit)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb.velocity += new Vector2(0, 1f);
                SetCharacterState("Walking");
            }
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                rb.velocity = new Vector2(0, 0);
                SetCharacterState("Idle");
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                rb.velocity += new Vector2(0, -1f);
                SetCharacterState("Walking");
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                rb.velocity = new Vector2(0, 0);
                SetCharacterState("Idle");
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb.velocity += new Vector2(-1f, 0);
                SetCharacterState("Walking");
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                rb.velocity = new Vector2(0, 0);
                SetCharacterState("Idle");
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.velocity += new Vector2(1f, 0);
                SetCharacterState("Walking");
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                rb.velocity = new Vector2(0, 0);
                SetCharacterState("Idle");
            }
            if (Input.GetKeyDown(KeyCode.KeypadEnter) && buildHouse && house.value < 3)
                am.BuildPlayerTwoSFX();
            if (Input.GetKey(KeyCode.KeypadEnter) && buildHouse && house.value < 3)
            {
                SetCharacterState("Working");
                build.SetActive(true);
                buildTime += Time.deltaTime;
                if (buildTime >= 3)
                {
                    am.FinishPlayerTwoSFX();
                    house.HouseBuild();
                    buildTime = 0;
                }
                if (house.value == 3)
                {
                    SetCharacterState("Idle");
                    build.SetActive(false);
                }
                if(!am.asSFX_2.isPlaying)
                {
                    am.asSFX_2.clip = am.build_2SFX;
                    am.asSFX_2.Play();
                }
            }
            if (Input.GetKeyDown(KeyCode.KeypadEnter) && stealHouse)
                am.BuildPlayerTwoSFX();
            if (Input.GetKey(KeyCode.KeypadEnter) && stealHouse)
            {
                SetCharacterState("Working");
                build.SetActive(true);
                stealTime += Time.deltaTime;
                if (stealTime >= 6)
                {
                    am.FinishPlayerTwoSFX();
                    house.HouseSteal();
                    SetCharacterState("Idle");
                    build.SetActive(false);
                }
            }
            if (Input.GetKeyDown(KeyCode.KeypadEnter) && activateMonolith && monolith.monolithPhase <= 3)
                am.BuildPlayerTwoSFX();
            if (Input.GetKey(KeyCode.KeypadEnter) && activateMonolith && monolith.monolithPhase <= 3)
            {
                SetCharacterState("Working");
                build.SetActive(true);
                monolithTime += Time.deltaTime;
                if (monolithTime >= 7)
                {
                    am.FinishPlayerTwoSFX();
                    monolith.BuildMonolith();
                    monolithTime = 0;
                }
                if (monolith.monolithPhase == 4)
                {
                    SetCharacterState("Idle");
                    build.SetActive(false);
                }
                if (!am.asSFX_2.isPlaying)
                {
                    am.asSFX_2.clip = am.build_2SFX;
                    am.asSFX_2.Play();
                }
            }
            if (Input.GetKeyDown(KeyCode.KeypadEnter) && punch)
            {
                am.PowPlayerOneSFX();
                SetCharacterState("Attack");
                playerOne.PlayerHitVoid();
            }
            if (Input.GetKeyUp(KeyCode.KeypadEnter))
            {
                SetCharacterState("Idle");
                build.SetActive(false);
                buildTime = 0;
                stealTime = 0;
                monolithTime = 0;
                am.StopPlayerTwoSFX();
            }
        }
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
        if (state.Equals("Walking"))
            SetAnimation(walking, true, 1f);
        if (state.Equals("Working"))
            SetAnimation(working, true, 1f);
        if (state.Equals("Attack"))
            SetAnimation(attack, true, 1f);
        if (state.Equals("Damaged"))
            SetAnimation(damaged, true, 1f);
    }
    public void PlayerHitVoid()
    {
        StartCoroutine(PlayerHit());
    }
    IEnumerator PlayerHit()
    {
        PowVoid();
        SetCharacterState("Damaged");
        build.SetActive(false);
        hit = true;
        yield return new WaitForSeconds(5);
        hit = false;
        SetCharacterState("Idle");
    }
    public void PowVoid()
    {
        StartCoroutine(Pow());
    }
    IEnumerator Pow()
    {
        pow.SetActive(true);
        yield return new WaitForSeconds(0.35f);
        pow.SetActive(false);
    }
}
