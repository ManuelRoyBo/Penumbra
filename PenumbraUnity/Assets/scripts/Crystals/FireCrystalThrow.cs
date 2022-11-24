using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCrystalThrow : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameManager.Instance.Player.GetComponent<Collider2D>(), true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
