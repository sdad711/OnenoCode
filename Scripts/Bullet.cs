using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public AudioManager am;
    public House house;
    Rigidbody2D rb;
    public House[] kucice;
    public GameObject[] targets;
    GameObject target;
    public float speed;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        kucice = FindObjectsOfType<House>();
        am = FindObjectOfType<AudioManager>();
        target = kucice[Random.Range(0, kucice.Length)].gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "HouseOne")
        {
            am.DamageHouseSFX();
            house = collision.gameObject.GetComponent<House>();
            house.HouseDestroy();
            this.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "HouseTwo")
        {
            am.DamageHouseSFX();
            house = collision.gameObject.GetComponent<House>();
            house.HouseDestroy();
            this.gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
}
