using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCrystalThrow : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D Light;
    public Animator animator;

    public float MIN_INTENSITY = 0.0f; //Apparently, ou cannot make constants appear in the inspector. If you have an alternative, feel free to modify it.
    public float DEFAULT_INTENSITY = 1.0f;
    public float TIME_BEFORE_IGNITION = 2.0f;
    public float IGNITE_DURATION = 1.0f;
    public float BURN_DURATION = 5.0f;
    public float FADE_TIME = 2.0f;

    // Start is called before the first frame update
    private void Start()
    {
        /*
         * 0. Crystal has just been thrown. Nothing really happens
         * 1. Crystal ignites. Starts animation and lights up
         * 2. Crystal is burning for a certain amount of time. Animation repeats
         * 3. Crystal slowly fades until removed from the game. Light fades too.
         */
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameManager.Instance.Player.GetComponent<Collider2D>(), true);

        Light.intensity = MIN_INTENSITY;
        StartCoroutine(Ignite(TIME_BEFORE_IGNITION));  //Ignition
        StartCoroutine(Burn(IGNITE_DURATION + TIME_BEFORE_IGNITION));  //burn
        StartCoroutine(Fade(BURN_DURATION+ IGNITE_DURATION + TIME_BEFORE_IGNITION));  //Fade

        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    IEnumerator Ignite(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //Ignition
        Light.intensity = DEFAULT_INTENSITY;

        animator.SetInteger("crystalState", 1);

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / IGNITE_DURATION)
        {
            Light.intensity = (Mathf.Lerp(MIN_INTENSITY, DEFAULT_INTENSITY, t));
            yield return null;
        }
    }
    IEnumerator Burn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        animator.SetInteger("crystalState", 2);
        //Burn
    }
    IEnumerator Fade(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        animator.SetInteger("crystalState", 3);
        //Fade
        SpriteRenderer rend = gameObject.transform.GetComponent<SpriteRenderer>();
        Color matColor = rend.color;
        float alphaValue = rend.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / FADE_TIME)
        {
            Light.intensity = (Mathf.Lerp(DEFAULT_INTENSITY,0, t));//Light
            rend.color = new Color(matColor.r, matColor.g, matColor.b, Mathf.Lerp(alphaValue, 0, t));//Fade colour (so that it becomes invisible)
            yield return null;
        }

        //destroy
        Destroy(gameObject);
    }


}
