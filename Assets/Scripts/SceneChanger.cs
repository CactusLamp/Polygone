using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            ChangeScene("Tutorial");
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            ChangeScene("Level 1");
            Debug.Log("gut");
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            ChangeScene("Level 2");
        }

    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

    }



}
