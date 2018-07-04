using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Point : MonoBehaviour {

    float x, y;

    // Use this for initialization
    void Start()
    { 
        x = transform.position.x;
        y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnUserAction()
    {
        Debug.Log("sucess");
        this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
        
    }
}
