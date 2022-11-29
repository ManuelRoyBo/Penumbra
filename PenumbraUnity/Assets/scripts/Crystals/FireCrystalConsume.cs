using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCrystalConsume : MonoBehaviour
{

    public float DURATION_OF_CRYSTAL = 10;
    public float TIME_OF_FLIGHT = 3;
    public float LAUNCH_FORCE = 5;

    bool isLaunched = false;

    Vector3 pointTowardMouse;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.PlayerObj;
        Destroy(gameObject, DURATION_OF_CRYSTAL);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log("launch");
            StartCoroutine(startLaunch());
            StartCoroutine(stopLaunch(TIME_OF_FLIGHT));
        }

        if (isLaunched)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(pointTowardMouse.x, pointTowardMouse.y) * LAUNCH_FORCE;
        }
    }
    IEnumerator startLaunch()
    {
        pointTowardMouse = GameManager.Instance.PointTowardsMouse(player);
        yield return new WaitForSeconds(0);
        isLaunched = true;
        player.GetComponent<PlayerMovement>().LockInput(true);
    }
    
    IEnumerator stopLaunch(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isLaunched = false;
        player.GetComponent<PlayerMovement>().LockInput(false);
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }
}
