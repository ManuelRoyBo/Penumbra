using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool lockInput = false;
   public Animator animator;
   private float scale;
   Rigidbody2D rb;
   float speed = 7;
   float jumpVelocity = 8;

    // Start is called before the first frame update
    void Start()
    {
      scale = gameObject.transform.localScale.x;
       rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.CanMove()) { rb.velocity = new Vector2(0, rb.velocity.y); return; }

        float dirX;

        if (!lockInput)
        {
            dirX = Input.GetAxisRaw("Horizontal");//direction X axis
        }
        else
        {
            dirX = 0;
        }

        rb.velocity = new Vector2(dirX * speed, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(dirX));

        if (dirX > 0)
        {
            transform.localScale = new Vector2(scale, transform.localScale.y);
        }
        else if (dirX < 0)
        {
            transform.localScale = new Vector2(-scale, transform.localScale.y);
        }

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
        }
    }
    public void LockInput(bool doLock)
    {
        lockInput = doLock;
        
    }
}
