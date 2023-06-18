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
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameManager.Instance.Player.GetComponent<Collider2D>(), true);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) { return; }
        GameManager.Instance.PAbility.HoldCrystal(this);
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
