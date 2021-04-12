using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public Animator myAnimator;

    public Text foundKey;
    public int key;
    public int boxInt;

    public bool isCircle = true;
    public bool isTriangle = false;
    bool isSquare = false;

    public bool canTriangle = false;
    bool canSquare = false;

    bool hasDiamondKey = false;

    bool isLevel2 = false;


    //references to components
    Rigidbody2D myBody;
    BoxCollider2D myCollider;

    float moveDir = 1;
    bool onFloor = true;

    // Start is called before the first frame update
    void Start()
    {
        myBody = gameObject.GetComponent<Rigidbody2D>();
        myCollider = gameObject.GetComponent<BoxCollider2D>();

        foundKey.text = "";

    }

    // Update is called once per frame
    void Update()
    {
        foundKey.text = "" + key;
    }

    private void FixedUpdate()
    {
        if (onFloor && myBody.velocity.y >= 1)
        {
            onFloor = false;
        }
        CheckKeys();
        HandleMovement();

        if (isCircle)
        {
            myAnimator.SetBool("isCircle", true);

            jumpHeight = 16;
        }

        if (isTriangle)
        {
            myAnimator.SetBool("isTriangle", true);

            jumpHeight = 23;
        }

        if (isSquare)
        {
            myAnimator.SetBool("isSquare", true);

            jumpHeight = 5;

            

        }


    }

    void CheckKeys()
    {

        if (Input.GetKey(KeyCode.D))
        {
            moveDir = 1;
        }

        else if (Input.GetKey(KeyCode.A))
        {
            moveDir = -1;

        }

        else
        {
            moveDir = 0;

        }
        

        if (Input.GetKey(KeyCode.Space) && onFloor)
        {
            //myBody.velocity is a reference to my character's rigid body's velocity(speed)
            myBody.velocity = new Vector3(myBody.velocity.x, jumpHeight);
        }

        if(Input.GetKey(KeyCode.Alpha1))
        {
            myAnimator.SetBool("isTriangle", false);
            myAnimator.SetBool("isSquare", false);
            onFloor = true;

            isTriangle = false;
            isSquare = false;
            isCircle = true;

        }

        if(Input.GetKey(KeyCode.Alpha2) && canTriangle)
        {
            myAnimator.SetBool("isCircle", false);
            myAnimator.SetBool("isSquare", false);

            isCircle = false;
            isSquare = false;
            isTriangle = true;
            

        }

        if(Input.GetKey(KeyCode.Alpha3) && canSquare)
        {
            myAnimator.SetBool("isCircle", false);
            myAnimator.SetBool("isTriangle", false);

            isTriangle = false;
            isCircle = false;
            isSquare = true;
        }


    }

    void HandleMovement()
    {
        myBody.velocity = new Vector3(moveDir * speed, myBody.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Floor")
        {
            onFloor = true;
        }

        if (collisionInfo.gameObject.tag == "Tilt")
        {
            
        }

        /*if (collisionInfo.gameObject.tag == "Button")
        {
            GameObject.Find("Game Manager").GetComponent<Manager>().spawnPlatform();
        }
        */

        if (collisionInfo.gameObject.tag == "Tilt" && isCircle)
        {
            onFloor = true;

            collisionInfo.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

        }

        if (collisionInfo.gameObject.tag == "Tilt" && isTriangle)
        {
            collisionInfo.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            onFloor = false;
        }

        if(collisionInfo.gameObject.tag == "Tri Button" && isTriangle)
        {
            collisionInfo.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            GameObject.Find("Game Manager").GetComponent<Manager>().spawnDiamond();

            if(key == 1)
            {
                Destroy(GameObject.Find("Go Away (1)"));
                Destroy(GameObject.Find("Go Away (2)"));
            }

        }

        if (collisionInfo.gameObject.tag == "Tri Button" && isCircle)
        {
            collisionInfo.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }


        //red box conditionals
        if(collisionInfo.gameObject.tag == "box" && isSquare)
        {
            collisionInfo.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

            
            
        }

        if (collisionInfo.gameObject.tag == "push box" && isSquare)
        {
            boxInt += 1;
            collisionInfo.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

            GameObject.Find("Game Manager").GetComponent<Manager>().boxStuff();

            Debug.Log("heyooo");

        }


        if (collisionInfo.gameObject.tag == "box" && isCircle)
        {
            collisionInfo.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if(collisionInfo.gameObject.tag == "box" && isTriangle)
        {
            collisionInfo.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (collisionInfo.gameObject.tag == "push box" && isCircle)
        {
            collisionInfo.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (collisionInfo.gameObject.tag == "push box" && isTriangle)
        {
            collisionInfo.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }


        if(collisionInfo.gameObject.tag == "Last Wall Check")
        {
            onFloor = true;

            if(key == 3)
            {
                Destroy(GameObject.Find("Locked Wall Blocker"));
                Destroy(GameObject.Find("Locked Wall Blocker (1)"));
                Destroy(GameObject.Find("Locked Wall Blocker (2)"));


            }
        }

    }

    

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Tri Check")
        {
            
            Destroy(other.gameObject);
            Destroy(GameObject.Find("triCover"));

           
        }

        //when the player comes into contact with the diamond key
        if(other.tag == "Dia Check")
        {
            hasDiamondKey = true;
            key = key + 1;
            

            Destroy(other.gameObject);
            //make another thing to detect which diamond we are destroying 
            Destroy(GameObject.Find("Lock HUD"));

            if(key == 1)
            {
                Destroy(GameObject.Find("last shadow HUD 3"));
                

                Debug.Log("next plz");
            }

            if(key == 2)
            {
                Destroy(GameObject.Find("last shadow HUD 1"));
                
                
            }

            

        }


        if (other.tag == "Tri Check")
        {
            canTriangle = true;
            Destroy(other.gameObject);
            
        }

        if(other.tag == "Square Check")
        {
            canSquare = true;
            Destroy(other.gameObject);
            Destroy(GameObject.Find("squareCover"));
        }

        if(other.tag == "Dia Wall Check" && hasDiamondKey)
        {
            
            Destroy(other.gameObject);
            Destroy(GameObject.Find("Locked Wall"));
            Destroy(GameObject.Find("Lock Big HUD"));

        }


        if (other.tag == "Level Door")
        {
            if(isTriangle && isLevel2 == false)
            {
                ChangeScene("Level 2");
                isLevel2 = true;
            }

            if(isSquare && isLevel2)
            {
                ChangeScene("Game End");
            }

        }
        
        

        /*if (other.tag == "Button")
        {
            GameObject.Find("Game Manager").GetComponent<Manager>().spawnPlatform();

        }

        if (other.tag == "Locked Wall Blocker" && hasDiamondKey == true)
        {
            Destroy(other.gameObject);
            Destroy(GameObject.Find("Hexagon Lock"));
            Destroy(GameObject.Find("Cross Lock"));
            Destroy(GameObject.Find("Diamond Lock"));
            Destroy(GameObject.Find("Door Block"));

        }

        /*if(other.tag == "Black Door")
        {
            GameObject.Find("Game Manager").GetComponent<Manager>().spawnText();
            Debug.Log("You're home idiot");
        }
        */

    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

    }

}
    






