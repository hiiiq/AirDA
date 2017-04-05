using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class importresizer : MonoBehaviour
{
    public GameObject frame;
    public Vector3 origpos;
    public Vector3 origscale;
    public Vector3 startpos;
    public Vector3 griporigpos;
    public Vector3 griporigscale;
    public Vector3 gripstartpos;
    private bool drag;
    public float mouse;
    public float click;

    void Start()
    {
        origpos = frame.transform.localPosition;
        origscale = frame.transform.localScale;
        startpos = transform.localPosition;
    }
    
    void Update()
    {
        mouse = Input.mousePosition.x - click;
        if (drag && griporigscale.x- mouse/100f > 2.75f )
        {
            frame.transform.localPosition = new Vector3(griporigpos.x + mouse / 50f, griporigpos.y, griporigpos.z);
            frame.transform.localScale = new Vector3(griporigscale.x - mouse / 100f, griporigscale.y, griporigscale.z);
            transform.localPosition = new Vector3(gripstartpos.x + mouse / 25f, gripstartpos.y, gripstartpos.z);
        }
        if (!drag)
        {
            if (frame.transform.localScale.x < 5.8f)
            {
                frame.transform.localScale = origscale;
                frame.transform.localPosition = origpos;
                transform.localPosition = startpos;
            }
            else
            {
                frame.transform.localScale = new Vector3(origscale.x + 6f, origscale.y, origscale.z);
                frame.transform.localPosition = new Vector3(origpos.x - 12f, origpos.y, origpos.z);
                transform.localPosition = new Vector3(startpos.x - 24f, startpos.y, startpos.z);
                start.inspector = true;

            }
        }
        if (frame.transform.localScale.x < 8.5f)
        {
            start.inspector = false;
            start.selected = null;
        }
    }

    void OnMouseDown()
    {
        griporigpos = frame.transform.localPosition;
        griporigscale = frame.transform.localScale;
        gripstartpos = transform.localPosition;
        click = Input.mousePosition.x;
        drag = true;
        Debug.Log("clicked");
    }

    void OnMouseUp()
    {
        drag = false;
    }

}