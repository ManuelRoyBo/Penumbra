using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   Rigidbody2D rb;
   float speed = 7;
   float jumpVelocity = 8;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

      float dirX = Input.GetAxisRaw("Horizontal");//direction X axis

      rb.velocity = new Vector2(dirX*speed, rb.velocity.y);
      

      if (Input.GetButtonDown("Jump"))
      {
            rb.velocity = new Vector2(rb.velocity.x,jumpVelocity);
      } 

      
    }
}
