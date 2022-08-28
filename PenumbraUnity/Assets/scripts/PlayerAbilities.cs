using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public Transform holdPosition;
    public Crystal holding, consumed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) { ThrowCrystal(); }
        if (holding)
        {
            holding.transform.position = holdPosition.position;
            holding.transform.rotation = holdPosition.rotation;
            holding.transform.localScale = holdPosition.localScale;
        }
    }
    public void HoldCrystal(Crystal toHold)
    {
        if (holding) { return; }
        toHold.GetComponent<Collider2D>().enabled = false;
        holding = toHold;
    }
    void ThrowCrystal()
    {
        if (!holding) { return; }
        holding.transform.rotation = Quaternion.identity;
        holding.transform.localScale = Vector3.one;
        holding.GetComponent<Collider2D>().enabled = true;
        holding = null;
    }
}
