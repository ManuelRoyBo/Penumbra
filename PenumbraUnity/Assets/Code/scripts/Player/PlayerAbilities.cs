using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class PlayerAbilities : MonoBehaviour
{
    [Header("Variables")]
    public float CRYSTAL_THROW_FORCE = 10f;
    public float HOLD_TO_CONSUME_TIME = 2.5f;
    public float COLOR_AURA_FADE_TIME = 1.0f;

    [Header("Hold positions slots")]
    public Transform holdPosition;
    public Transform crystalConsummingHoldPosition;


    [Header("For Debugging")]
    public Crystal holding;
    public Crystal consumed;

    /*
     * Dear Experienced programmers reading this. 
     * I am sorry. I have difficulty with object oriented programming
     * Your eyes may bleed. I am sorry.
     * - kingpin
     */

    [Header("Consume aura light color")]
    public Light2D playerLight;
    public Color fireCrystalAuraColor;
    public Color waterCrystalAuraColor;
    public Color earthCrystalAuraColor;
    public Color darkCrystalAuraColor;


    Coroutine holdButton;
    Color defaultColor;

    private void Start()
    {
        defaultColor = playerLight.color;
    }

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

        GameObject obj = (GameObject)Instantiate(holding.throwCrystal, gameObject.transform.position, new Quaternion(0, 0, 0, 0));
        obj.GetComponent<Rigidbody2D>().velocity = GameManager.Instance.PointTowardsMouse(gameObject) * CRYSTAL_THROW_FORCE;

        holding.GetComponent<Crystal>().DestroySelf(); //I wasn't able to destroy the crystal from this script. so Instead, the crystal destroy itself and I call its function.
        holding = null;
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

        GameObject consumeCrystal = holding.consumeCrystal;
        ICrystalConsume consumeCrystalScript = consumeCrystal.GetComponent<ICrystalConsume>();

        Instantiate(consumeCrystal);

        float duration = consumeCrystalScript.DURATION_OF_CRYSTAL;
        StartCoroutine(changeAuraColor(consumeCrystalScript.PLAYER_COLOR_AURA, duration));

        holding.GetComponent<Crystal>().DestroySelf(); //I wasn't able to destroy the crystal from this script. so Instead, the crystal destroy itself and I call its function.
        holding = null;
    }

    IEnumerator changeAuraColor(Color color, float time)
    {
       
        float timeElapsed = 0;
        while (timeElapsed < COLOR_AURA_FADE_TIME) //This while loop make sure the consume animation takes the exact same time as HOLD_TO_CONSUME_TIME
        {
            playerLight.color = Color.Lerp(defaultColor, color, timeElapsed / COLOR_AURA_FADE_TIME);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(time - (COLOR_AURA_FADE_TIME*2));
        timeElapsed = 0;
        while (timeElapsed < COLOR_AURA_FADE_TIME) //This while loop make sure the consume animation takes the exact same time as HOLD_TO_CONSUME_TIME
        {
            playerLight.color = Color.Lerp(color, defaultColor, timeElapsed / COLOR_AURA_FADE_TIME);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
