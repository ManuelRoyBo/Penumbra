using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaMovement : MonoBehaviour
{

    public float speed = 5f;
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private Rigidbody2D rb;
    
    [Header("Collider for ground")]
    public BoxCollider2D collider;
    // Start is called before the first frame update

    bool isFacingRight = true;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isFacingRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }

        if (isNearEdge(isFacingRight) || isNearWall(isFacingRight))
        {
            isFacingRight = !isFacingRight;
            gameObject.transform.localScale = new Vector2(-gameObject.transform.localScale.x, gameObject.transform.localScale.y);
        }




    }
    private bool isNearEdge(bool isFacingRight)
    {
        //direction of the rays
        Vector2 rayDirection;
        if (isFacingRight)
        {
            rayDirection = new Vector2(1, -1);
        }
        else
        {
            rayDirection = new Vector2(-1, -1);
        }


        float extraLengthText = 0.01f;
        float rayCastLength = Mathf.Sqrt(Mathf.Pow(collider.size.y, 2f) + Mathf.Pow(collider.size.x, 2f)) + extraLengthText;
        RaycastHit2D raycastHit = Physics2D.Raycast(collider.bounds.center, rayDirection, rayCastLength, platformLayerMask);
        Color rayColor;
        if (!(raycastHit.collider != null))
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(collider.bounds.center, rayDirection * (rayCastLength), rayColor);
        return !(raycastHit.collider != null);
    }
    private bool isNearWall(bool isFacingRight)
    {
        //direction of the rays
        Vector2 rayDirection;
        if (isFacingRight)
        {
            rayDirection = new Vector2(1, 0);
        }
        else
        {
            rayDirection = new Vector2(-1, 0);
        }


        float extraLengthText = 0.01f;
        float rayCastLength = collider.size.x + extraLengthText;
        RaycastHit2D raycastHit = Physics2D.BoxCast(collider.bounds.center,collider.size*0.8f,0f, rayDirection, rayCastLength, platformLayerMask);
        Color rayColor;
        if (!(raycastHit.collider != null))
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(collider.bounds.center, rayDirection * (rayCastLength), rayColor);
        return (raycastHit.collider != null);
    }
}
