using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class joinop : MonoBehaviour {

    public GameObject count;
    public GameObject sum;
    public GameObject avg;
    public GameObject min;
    public GameObject max;

    private void OnMouseEnter()
    {
        count.GetComponent<SpriteRenderer>().enabled = false;
        sum.GetComponent<SpriteRenderer>().enabled = false;
        avg.GetComponent<SpriteRenderer>().enabled = false;
        min.GetComponent<SpriteRenderer>().enabled = false;
        max.GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = true;
        table.joinop = GetComponent<Text>().text;

    }
    
}
