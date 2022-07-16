using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crystalPickup : MonoBehaviour
{
    
    public int crystalType;

    private PortableCrystals portableCrystals;
    private GameObject player;
    public bool readyToBePickedUp = false; //make sure crystal isn't instantly picked up when a player drop it

    private void Start()
 {
  player = GameObject.FindGameObjectWithTag("Player");
  Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
  portableCrystals = GameObject.Find("PortableCrystals").GetComponent<PortableCrystals>();

  StartCoroutine(crystalSpawned());
 }

  IEnumerator crystalSpawned() //basically a delayed on start. (Cause there need to be some physics simulated)
  {
  yield return new WaitForSeconds(0.1f);
  readyToBePickedUp = ! ((gameObject.GetComponent<CircleCollider2D>().IsTouching(player.GetComponent<Collider2D>())));
  }
  private void OnTriggerExit2D(Collider2D collision){
    readyToBePickedUp = true;
  }

  
  private void OnTriggerEnter2D(Collider2D collision){
    if ((collision.tag == "Player") && (portableCrystals.hasCrystal == 0) && (readyToBePickedUp == true))
    {
      portableCrystals.hasCrystal = crystalType;
      Destroy(gameObject);

      }
  }

}
