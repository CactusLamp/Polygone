using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class cameraFollow : MonoBehaviour
{
    public Transform followTransform;
    //to make sure that the camera doesnt move out of the space we want it to see
    public BoxCollider2D worldBounds;
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    float camX;
    float camY;
    //ratio is the width, x component
    float camRatio;
    float camSize;
    //reference to camera component
    Camera mainCam;
    //math for smoothing, staggered effect
    Vector3 smoothPos;
    //speed at which we are smoothing
    public float smoothRate;

    //public float cameraOffset = -10;

    public float smoothSpeed = 0.125f; //higher -> the slower the camera locks onto target
    public Vector3 offset;
    Vector3 velocity = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        xMin = worldBounds.bounds.min.x;
        xMax = worldBounds.bounds.max.x;
        yMin = worldBounds.bounds.min.y;
        yMax = worldBounds.bounds.max.y;

        Debug.Log(xMin);
        Debug.Log(xMax);

        mainCam = gameObject.GetComponent<Camera>();
        //getting the camera ratio so that we have this centered
        camSize = mainCam.orthographicSize;
        camRatio = (xMax + camSize) / 8.0f;
    }
    // Update is called once per frame
    void Update()
    {

    }
    //fixed update has frequency of the physics system.
    void FixedUpdate()
    {
        //
        camY = Mathf.Clamp(followTransform.position.y, yMin + camSize, yMax - camSize);
        camX = Mathf.Clamp(followTransform.position.x, xMin + camRatio, xMax - camRatio);
        //this = gameObject
        //
        //Vector3 desiredPosition = followTransform.position + offset;
        smoothPos = Vector3.Lerp(this.transform.position, new Vector3(camX, camY, gameObject.transform.position.z), smoothRate);
        gameObject.transform.position = smoothPos;
    }


    /*void LateUpdate() //makes it so that it runs right after update and it won't compete with other scripts' updates
    {
        Vector3 desiredPosition = followTransform.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed); //use smoothdamp instead of lerp cos lerp doesn't much like tracking moving objects apparently
        transform.position = smoothedPosition;
    }
    */
}