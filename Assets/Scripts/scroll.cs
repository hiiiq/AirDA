using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroll : MonoBehaviour
{
    private bool drag;
    public float mouse;
    public float click;
    private Vector3 pos;
    public Vector3 orig;

    void Update()   
    {
        mouse = Input.mousePosition.y-click;
        if (drag)
        {
           transform.position = new Vector3(orig.x,orig.y+mouse/40f,orig.z);
        }
    }

    void OnMouseDown()
    {
        orig = transform.position;
        click = Input.mousePosition.y;
        drag = true;
        transform.GetComponentInParent<table>().StickTable();
    }

    void OnMouseUp()
    {
        drag = false;
        if (transform.localPosition.y < 0)
        {
            transform.localPosition = Vector3.zero;
        }
        else
        {
            orig = transform.position;
        }

    }

}
