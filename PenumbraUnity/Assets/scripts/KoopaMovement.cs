using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaMovement : MonoBehaviour
{

    public float speed = 5f;
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private Rigidbody2D rb;
    // Start is called before the first frame update

    bool isFacingRight = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 rayDirection;
        if (isFacingRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            rayDirection = new Vector2(1, -1);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            rayDirection = new Vector2(-1, -1);
        }

        if (isNearEdge(rayDirection))
        {
            Debug.Log("isNearEdge");
            isFacingRight = !isFacingRight;
            gameObject.transform.localScale = new Vector2(-gameObject.transform.localScale.x, gameObject.transform.localScale.y);
        }

        

        
    }
    private bool isNearEdge(Vector2 direction)
    {
        float extraHeightText = 0.3f;
        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
        RaycastHit2D raycastHit = Physics2D.Raycast(collider.bounds.center, direction,collider.bounds.extents.y + extraHeightText, platformLayerMask);
        Color rayColor;
        if (!(raycastHit.collider != null))
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(collider.bounds.center, direction * (collider.bounds.extents.y + extraHeightText),rayColor);
        return !(raycastHit.collider != null);
    }
}
