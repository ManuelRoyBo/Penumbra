using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortableCrystals : MonoBehaviour
{

    public int hasCrystal = 0;
    public GameObject waterCrystalPrefab;
    private Sprite waterCrystalSprite;
    public GameObject fireCrystalPrefab;
    private Sprite fireCrystalSprite;
    public GameObject earthCrystalPrefab;
    private Sprite earthCrystalSprite;
    public GameObject darkCrystalPrefab;
    private Sprite darkCrystalSprite;


    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

        waterCrystalSprite = waterCrystalPrefab.GetComponent<SpriteRenderer>().sprite;
        fireCrystalSprite = fireCrystalPrefab.GetComponent<SpriteRenderer>().sprite;
        earthCrystalSprite = earthCrystalPrefab.GetComponent<SpriteRenderer>().sprite;
        darkCrystalSprite = darkCrystalPrefab.GetComponent<SpriteRenderer>().sprite;
    }


    void Update()
    {
        
        switch (hasCrystal)
        {
            case 0:
                spriteRenderer.enabled = false;
                break;
            case 1:
                spriteRenderer.enabled = true;
                spriteRenderer.sprite = waterCrystalSprite;
                break;
            case 2:
                spriteRenderer.enabled = true;
                spriteRenderer.sprite =fireCrystalSprite;
                break;
            case 3:
                spriteRenderer.enabled = true;
                spriteRenderer.sprite = earthCrystalSprite;
                break;
            case 4:
                spriteRenderer.enabled = true;
                spriteRenderer.sprite = darkCrystalSprite;
                break;
        }

        //crystal throw
        if (Input.GetKeyDown(KeyCode.E)){
            GameObject crystal = null;
            switch (hasCrystal){
                case 0:
                    break;
                case 1:
                    crystal = waterCrystalPrefab;
                    break;
                case 2:
                    crystal = fireCrystalPrefab;
                    break;
                case 3:
                    crystal = earthCrystalPrefab;
                    break;
                case 4:
                    crystal = darkCrystalPrefab;
                    break;
                
            }
            if (hasCrystal != 0){
                    Instantiate(crystal,transform.position + new Vector3(0,0.1f,0), Quaternion.identity);
                    hasCrystal = 0;
                }
        }
    }


    

}
