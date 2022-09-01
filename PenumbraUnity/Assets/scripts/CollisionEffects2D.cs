using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionEffects2D : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Damage taken");
            //Restart scene here
        }
    }

}