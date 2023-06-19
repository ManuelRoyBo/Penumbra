using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Crystal : MonoBehaviour
{
    public GameObject throwCrystal;
    public GameObject consumeCrystal;
    public Color consumePlayerAura;
    private void Start()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerAbilities>().HoldCrystal(this);
        }
        
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
