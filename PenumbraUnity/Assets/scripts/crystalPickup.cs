using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crystalPickup : MonoBehaviour
{
    //private int hasCrystal;
    
    public int crystalType;

    private PortableCrystals portableCrystals;
    private void Start()
 {
          GameObject player = GameObject.FindGameObjectWithTag("Player");
          Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());

          portableCrystals = GameObject.Find("PortableCrystals").GetComponent<PortableCrystals>();
 }


    private void OnTriggerEnter2D(Collider2D collision){
        if ((collision.tag == "Player") && (portableCrystals.hasCrystal == 0))
        {
        portableCrystals.hasCrystal = crystalType;
          Destroy(gameObject);

        }
    }

}
