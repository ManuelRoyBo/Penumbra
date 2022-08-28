using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    private void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameManager.Instance.Player.GetComponent<Collider2D>(), true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) { return; }
        GameManager.Instance.PAbility.HoldCrystal(this);
    }
}
