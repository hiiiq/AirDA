using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class table : MonoBehaviour
{
    public static float distance;
    public int tableid;
    public string[,] data;
    public int[] columns;
    public GameObject cellprefab;
    public GameObject frame;
    public GameObject sheet;
    public List<GameObject> chilren;
    private int oscj = 0;
    public bool movingtable = false;

    void Start ()
	{   frame.transform.localScale = new Vector3(data.GetLength(1)/7f,1f,1f);
        chilren = new List<GameObject>();
	    sheet.GetComponent<BoxCollider>().center = new Vector3(0f,data.GetLength(0)/-5f +2.2f,0f);
	    sheet.GetComponent<BoxCollider>().size = new Vector3(data.GetLength(1),data.GetLength(0)/2.5f,1f);
        columns = new int[data.GetLength(1)];

        for (int i = 0; i < data.GetLength(1); i++)
        {
            columns[i] = 0;
            GameObject cell = Instantiate(cellprefab);
            cell.name = tableid.ToString() + ".0." + i.ToString();
            cell.transform.localScale = new Vector3(1f, 0.4f, 0.4f);
            cell.transform.position = new Vector3(transform.localPosition.x - (data.GetLength(1)/2f - 0.5f) + i * 1f, transform.localPosition.y + 2.6f, transform.localPosition.z);
            cell.GetComponentInChildren<Text>().text = data[0, i];
            cell.transform.parent = transform;

            cell.AddComponent<EventTrigger>();

            EventTrigger enterevent = cell.GetComponent<EventTrigger>();
            EventTrigger.Entry PointerEntry = new EventTrigger.Entry();
            PointerEntry.eventID = EventTriggerType.PointerEnter;
            PointerEntry.callback.AddListener(delegate { Enter(cell); });
            enterevent.triggers.Add(PointerEntry);

            EventTrigger clickevent = cell.GetComponent<EventTrigger>();
            EventTrigger.Entry ClickEntry = new EventTrigger.Entry();
            ClickEntry.eventID = EventTriggerType.PointerDown;
            ClickEntry.callback.AddListener(delegate { Click(cell); });
            clickevent.triggers.Add(ClickEntry);

            chilren.Add(cell);

            for (int j = 1; j < data.GetLength(0); j++)
            {

                oscj++;
                if (j < data.GetLength(0)-20)
                {
                    oscj = 0;
                }
                GameObject cel = Instantiate(cellprefab);
                cel.name = tableid.ToString() + "." + j.ToString() + "." + i.ToString();
                cel.transform.localScale = new Vector3(1f, 0.5f, 0.4f);
                cel.transform.position = new Vector3(transform.localPosition.x - (data.GetLength(1) / 2f - 0.5f) + i * 1f, transform.localPosition.y + 2.3f - j * 0.4f, transform.localPosition.z + oscj * 0.009f);
                cel.GetComponentInChildren<Text>().text = data[j, i];
                cel.transform.parent = sheet.transform;
                chilren.Add(cel);



            }
        }

    }
	
	void Update () {
	    foreach (GameObject cell in chilren)
	    {
            string[] index = cell.name.Split('.');
	        if (Convert.ToInt32(index[1]) != 0)
	        {
	            if (cell.transform.position.y + transform.localScale.y * 3 < transform.position.y ||
	                cell.transform.position.y - transform.localScale.y * 2 > transform.position.y)
	            {
	                cell.SetActive(false);
	            }
	            else
	            {
	                cell.SetActive(true);
	            }
	        }

	        for (int i = 0; i < columns.Length; i++)
	        {
	            

	            if (Convert.ToInt32(index[2]) == i)
	            {      
	                if (columns[i] == 1)
	                {
	                    cell.GetComponent<Renderer>().material.color = Color.red;
	                }
	                else
	                {
	                    cell.GetComponent<Renderer>().material.color = Color.white;

	                }
	            }
	        }
	    }

        if (movingtable)
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = r.GetPoint(distance);
            gameObject.transform.position = rayPoint;

        }
    }

    void OnMouseEnter()
    {
        start.Inspect(tableid);
    }

    void Enter(GameObject cell)
    {
        string[] cellData = cell.name.Split('.');
        if (columns[Convert.ToInt32(cellData[2])] == 1)
        {
            columns[Convert.ToInt32(cellData[2])] = 0;
        }
        else
        {
            columns[Convert.ToInt32(cellData[2])] = 1;
        }
    }

    void Click(GameObject cell)
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        string[,] newdata = new string[data.GetLength(0), columns.Sum()];
                int count = 0;
        if (columns.Sum() > 0)
        {
            for (int i = 0; i < columns.Length; i++)
            {
                if(columns[i] == 1)
                {
                    for (int j = 0; j < data.GetLength(0); j++)
                    {
                        newdata[j, count] = data[j, i];
                    }
                    count++;
                }
            }
        }
        GameObject.Find("import").transform.GetComponent<start>().CreateNewTable(newdata);
    }
}

