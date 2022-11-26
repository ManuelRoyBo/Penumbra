using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAbilities : MonoBehaviour
{
    public float CRYSTAL_THROW_FORCE = 10f;

    public Transform holdPosition;
    public Crystal holding, consumed;

    /*
     * Dear Experienced programmers reading this. 
     * I am sorry. I have difficulty with object oriented programming
     * Your eyes may bleed. I am sorry.
     * - kingpin
     */

    //Default crystals
    public GameObject fireCrystalPrefab;
    public GameObject waterCrystalPrefab;
    public GameObject earthCrystalPrefab;
    public GameObject darkCrystalPrefab;
    
    //Throw prefab
    public GameObject fireCrystalThrowPrefab;
    public GameObject waterCrystalThrowPrefab;
    public GameObject earthCrystalThrowPrefab;
    public GameObject darkCrystalThrowPrefab;

    //Consume Prefab (mostly accessing the script)
    public GameObject fireCrystalConsumePrefab;
    public GameObject waterCrystalConsumePrefab;
    public GameObject earthCrystalConsumePrefab;
    public GameObject darkCrystalConsumePrefab;

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
            ThrowInstantiate(fireCrystalThrowPrefab);
        }
        else
        {
            Debug.Log("ERROR: the crystal throw behaviour either wasn't programmed yet or the name of this crystal object is wrong." +
                " (Note that the crystal gameObject must include it's prefab's name in it.  )" +
                "In either cases, the crystal auto-destructs itself and doesn't instanciante it's thrown version.");
        }

        holding.GetComponent<Crystal>().DestroySelf(); //I wasn't able to destroy the crystal from this script. so Instead, the crystal destroy itself and I call its function.
        holding = null;
    }

    void ThrowInstantiate(GameObject objectToInstantiate)
    {
        GameObject obj = (GameObject)Instantiate(objectToInstantiate, gameObject.transform.position, new Quaternion(0, 0, 0, 0));
        obj.GetComponent<Rigidbody2D>().velocity = PointTowardsMouse() * CRYSTAL_THROW_FORCE;
    }

    /// <summary>
    /// Returns a vector from the player pointing towards the mouse.
    /// </summary>
    /// <returns></returns>
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
