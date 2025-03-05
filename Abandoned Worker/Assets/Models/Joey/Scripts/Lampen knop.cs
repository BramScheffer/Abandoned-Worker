using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Lampenknop : MonoBehaviour
{
    public GameObject Light;
    public bool Lightbool;

    // Start is called before the first frame update
    void Start()
    {
        Light.SetActive(false);
        Lightbool = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            if(Lightbool==true)
            {
                Light.SetActive(false );
                Lightbool = false;
            }
            else if(Lightbool == false)
            {
                Lightbool = true;
                Light.SetActive(true );
            }

    }
}
