using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAbilities : MonoBehaviour
{
    public float CRYSTAL_THROW_FORCE = 10f;
    public float HOLD_TO_CONSUME_TIME = 2.5f;

    public Transform holdPosition, crystalConsummingHoldPosition;
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

    Coroutine holdButton;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1")) { ThrowCrystal(); }

        if (holding)
        {
            Animator animator = holding.GetComponent<Animator>();

            animator.SetBool("isHolding", true);

            if (Input.GetButtonDown("Fire2"))
            {
                holdButton = StartCoroutine(HoldToConsume());
            }
            else if (Input.GetButtonUp("Fire2"))
            {
                StopCoroutine(holdButton);//No error? Variable "hold" might not be initialized
            }

            if (Input.GetButton("Fire2"))
            {
                gameObject.GetComponent<PlayerMovement>().LockInput(true); //player can't move while holding
                animator.SetBool("isConsuming", true);
                holding.transform.position = crystalConsummingHoldPosition.position;
                holding.transform.rotation = crystalConsummingHoldPosition.rotation;
                holding.transform.localScale = crystalConsummingHoldPosition.localScale;
            }
            else
            {
                gameObject.GetComponent<PlayerMovement>().LockInput(false);
                animator.SetBool("isConsuming", false);
                holding.transform.position = holdPosition.position;
                holding.transform.rotation = holdPosition.rotation;
                holding.transform.localScale = holdPosition.localScale;
            }
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
        //add an if else for other crystals

        holding.GetComponent<Crystal>().DestroySelf(); //I wasn't able to destroy the crystal from this script. so Instead, the crystal destroy itself and I call its function.
        holding = null;
    }

    void ThrowInstantiate(GameObject objectToInstantiate)
    {
        GameObject obj = (GameObject)Instantiate(objectToInstantiate, gameObject.transform.position, new Quaternion(0, 0, 0, 0));
        obj.GetComponent<Rigidbody2D>().velocity = GameManager.Instance.PointTowardsMouse(gameObject) * CRYSTAL_THROW_FORCE;
    }

    IEnumerator HoldToConsume()
    {
        Animator animator = holding.GetComponent<Animator>();
        float timeElapsed = 0f;
        while (timeElapsed < HOLD_TO_CONSUME_TIME) //This while loop make sure the consume animation takes the exact same time as HOLD_TO_CONSUME_TIME
        {
            animator.SetFloat("motionTime", Mathf.Lerp(0f, 1f, timeElapsed/HOLD_TO_CONSUME_TIME));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        animator.SetFloat("motionTime", 1f);
        ConsumeCrystal();
        gameObject.GetComponent<PlayerMovement>().LockInput(false);
    }

    void ConsumeCrystal()
    {
        if (!holding) { return; }
        if (holding.name.Contains(fireCrystalPrefab.name))
        { 
            Instantiate(fireCrystalConsumePrefab);
        }

        holding.GetComponent<Crystal>().DestroySelf(); //I wasn't able to destroy the crystal from this script. so Instead, the crystal destroy itself and I call its function.
        holding = null;
    }
}
