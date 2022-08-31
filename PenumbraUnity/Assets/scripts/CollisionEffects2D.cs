using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionEffects2D : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.collider.name == "Player")
        {
            Debug.Log("Damage taken");
        }
    }

}