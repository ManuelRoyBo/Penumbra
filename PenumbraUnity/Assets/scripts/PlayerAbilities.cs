using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAbilities : MonoBehaviour
{
    public Transform holdPosition;
    public Crystal holding, consumed;

    public GameObject fireCrystalPrefab;

    public GameObject fireCrystalThrow;
    public float CRYSTAL_THROW_FORCE = 10f;

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

        if (holding.name.Contains(fireCrystalPrefab.name))
        {
            GameObject obj =  (GameObject)Instantiate(fireCrystalThrow, gameObject.transform.position, new Quaternion(0,0,0,0));
            obj.GetComponent<Rigidbody2D>().velocity = PointTowardsMouse() * CRYSTAL_THROW_FORCE;
        }

        holding.GetComponent<Crystal>().DestroySelf(); //I wasn't able to destroy the crystal from this script. so Instead, the crystal destroy itself and I call its function.
        holding = null;
        
        
    }

    Vector3 PointTowardsMouse()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 playerPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

        Vector2 difference = mousePosition - playerPosition;
        double angleInRadian = Math.Atan2(difference.y, difference.x);
        Vector3 points = new Vector3((float)Math.Cos(angleInRadian), (float)Math.Sin(angleInRadian), 0);

        return points;
    }
}
