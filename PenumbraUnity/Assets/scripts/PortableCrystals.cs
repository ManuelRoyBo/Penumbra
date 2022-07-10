using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortableCrystals : MonoBehaviour
{

    public int hasCrystal = 0;
    public GameObject waterCrystalPrefab;
    // Update is called once per frame
    void Update()
    {
        
        switch (hasCrystal)
        {
            case 0:
                this.gameObject.GetComponent<Renderer>().enabled = false;
                break;
            case 1:
                this.gameObject.GetComponent<Renderer>().enabled = true;
                break;
        }

        //crystal throw
        if (Input.GetKeyDown(KeyCode.E)){
            
            switch (hasCrystal){
                case 0:
                    break;
                case 1:
                    Instantiate(waterCrystalPrefab,transform.position + new Vector3(1,0,0), Quaternion.identity);
                    hasCrystal = 0;
                    break;
        }
        }
    }


    

}
