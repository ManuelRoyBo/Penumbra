using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerAbilities : MonoBehaviour
{
    [Header("Variables")]
    public float CRYSTAL_THROW_FORCE = 10f;
    public float HOLD_TO_CONSUME_TIME = 2.5f;
    public float COLOR_AURA_FADE_TIME = 1.0f;

    [Header("Hold positions slots")]
    public Transform holdPosition;
    public Transform crystalConsumingHoldPosition;


    [Header("For Debugging")]
    public Crystal holding;
    public Crystal consumed;

    /*
     * Dear Experienced programmers reading this. 
     * I am sorry. I have difficulty with object oriented programming
     * Your eyes may bleed. I am sorry.
     * - kingpin
     */
    public Light2D playerLight;


    Coroutine holdButton;
    Color defaultColor;

    private void Start()
    {
        defaultColor = playerLight.color;
    }

    public void HoldCrystal(Crystal toHold)
    {
        if (holding != null)
        {
            return;
        }

        holding = toHold;
        holding.GetComponent<Collider2D>().enabled = false;
    }


    private void Update()
    {
        if (holding != null)
        {
            Animator animator = holding.GetComponent<Animator>();
            animator.SetBool("isHolding", true);

            holding.transform.position = holdPosition.position;
            holding.transform.rotation = holdPosition.rotation;
            holding.transform.localScale = holdPosition.localScale;

            if (Input.GetButtonDown("Fire1"))
            {
                ThrowCrystal();
            }

            
            
            if (Input.GetButton("Fire2"))
            {
                if (Input.GetButtonDown("Fire2"))
                {
                    holdButton = StartCoroutine(HoldToConsume());
                    animator.SetBool("isConsuming", true);
                }
                
                holding.transform.position = crystalConsumingHoldPosition.position;
                holding.transform.rotation = crystalConsumingHoldPosition.rotation;
                holding.transform.localScale = crystalConsumingHoldPosition.localScale;
            }
            else if (Input.GetButtonUp("Fire2"))
            {
                StopCoroutine(holdButton);
                animator.SetBool("isConsuming", false);
            }



        }


    }
    void ThrowCrystal()
    {
        if (holding)
        {
            GameObject obj = (GameObject)Instantiate(holding.throwCrystal, gameObject.transform.position, new Quaternion(0, 0, 0, 0));
            obj.GetComponent<Rigidbody2D>().velocity = GameManager.Instance.PointTowardsMouse(gameObject) * CRYSTAL_THROW_FORCE;

            holding.GetComponent<Crystal>().DestroySelf(); //I wasn't able to destroy the crystal from this script. so Instead, the crystal destroy itself and I call its function.
            holding = null;
        }
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
        //gameObject.GetComponent<PlayerMovement>().LockInput(false);
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
        Debug.Log("consumed");
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
