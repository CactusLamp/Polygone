using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{

    
    public GameObject Diamond;
    public GameObject diaSpawn1;
    public GameObject diaSpawn2;
    public GameObject diaSpawn3;
    public int keyM;
    public int boxM;
    


    //private GameObject triHUD;

 




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //wtf
    

    public void spawnDiamond()
    {
        //we tryna get the key counter in the player script
        keyM = GameObject.Find("Player").GetComponent<PlayerBehavior>().key;
        Debug.Log(keyM);
        
        if (keyM == 0)
        {
            Instantiate(Diamond, diaSpawn1.transform.position, Quaternion.identity);
            Destroy(GameObject.Find("Go Away"));
            
            
        }
        
        if (keyM == 1)
        {
            Instantiate(Diamond, diaSpawn2.transform.position, Quaternion.identity);
            

            //Debug.Log(keyM);

        }

        if (keyM == 2)
        {
            Instantiate(Diamond, diaSpawn3.transform.position, Quaternion.identity);
            
            Destroy(GameObject.Find("Go Away (2)"));
        }
        
        


    }

    public void boxStuff()
    {
        boxM = GameObject.Find("Player").GetComponent<PlayerBehavior>().key;

        if(boxM == 0)
        {
            Destroy(GameObject.Find("red away"));
            Destroy(GameObject.Find("red away (1)"));
            Destroy(GameObject.Find("red away (2)"));
        }

        if(boxM == 1)
        {
            spawnDiamond();
        }
    }

    public void spawnTilt()
    {
        //Instantiate()
    }

    
}
