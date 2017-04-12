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
    public string tablename;
    public string[,] data;
    public int[] columns;
    public string[] columnnames;
    public List<string> joincolumns;
    public GameObject cellprefab;
    public GameObject frame;
    public GameObject sheet;
    public List<GameObject> chilren;
    private int oscj = 0;
    public bool movingtable = false;
    public bool newtable = false;
    public float radius = 0;
    public Collider[] colliders;
    public int joincounter = 0;
    public string query;
    static public string joinop;

    void Start ()
	{
        chilren = new List<GameObject>();
        joincolumns = new List<string>();
        Draw();
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
	                    cell.GetComponent<Renderer>().material.color = new Color(179f/ 255f,217f / 255f, 1f);
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
            gameObject.transform.position = new Vector3(rayPoint.x, rayPoint.y, -0.27f);
            transform.GetComponent<BoxCollider>().enabled = true;

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
        GameObject.Find("import").transform.GetComponent<start>().CreateNewTable(newdata, tablename);
    }

    public void StickTable()
    {
        if (movingtable)
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = r.GetPoint(distance);
            gameObject.transform.position = new Vector3(rayPoint.x, rayPoint.y, -0.22f);
            sheet.GetComponent<BoxCollider>().enabled = true;
            movingtable = false;
        }
    }

    public void LateUpdate()
    {
        if (newtable == true && movingtable == false)
        {
            colliders = Physics.OverlapSphere(transform.position, radius);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].transform.GetComponent<table>() != null)
                {
                    if (colliders[i].transform.GetComponent<table>().columns.Length == 1 && colliders[i].transform != this.transform && joincounter < joincolumns.Count)
                    {
                        GameObject incoming = colliders[i].transform.gameObject;
                        joincounter++;
                        joincolumns.Add(joinop + incoming.GetComponent<table>().tablename + "." + incoming.GetComponent<table>().data[0,0] + ")");
                        query = "SELECT " + tablename + "." + columnnames[0] + ", ";
                        for (int k = 1; k < joincolumns.Count; k++)
                        {
                            query = query + joincolumns[k];
                            if (k < joincolumns.Count - 1)
                            {
                                query = query + ", ";
                            }

                        }
                        query = query + " FROM " + tablename + ", " + incoming.GetComponent<table>().tablename;
                        query = query + " WHERE " + tablename + "." + columnnames[0] +" = "+ incoming.GetComponent<table>().tablename + "." + data[0,0] + " GROUP BY " + tablename + "." + columnnames[0]+" LIMIT 100;";
                        data = dbhandler.Join(query, joincolumns.Count);
                        for (int j = 0; j < chilren.Count; j++)
                        {
                            Destroy(chilren[j]);
                        }
                        chilren.Clear();
                        Destroy(incoming.gameObject);
                        Draw();
                    }
                }
            }
        }
    }

    public void Draw()
    {
        frame.transform.localScale = new Vector3(data.GetLength(1) / 7f, 1f, 1f);
        transform.GetComponent<BoxCollider>().enabled = false;
        sheet.GetComponent<BoxCollider>().center = new Vector3(0f, data.GetLength(0) / -5f + 2.2f, 0f);
        sheet.GetComponent<BoxCollider>().size = new Vector3(data.GetLength(1), data.GetLength(0) / 2.5f, 1f);
        columns = new int[data.GetLength(1)];
        columnnames = new string[data.GetLength(1)];
        joincolumns.Clear();
        for (int i = 0; i < data.GetLength(1); i++)
        {
            columns[i] = 0;
            GameObject cell = Instantiate(cellprefab);
            cell.name = tableid.ToString() + ".0." + i.ToString();
            cell.transform.localScale = new Vector3(1f, 0.4f, 0.4f);
            cell.transform.position = new Vector3(transform.localPosition.x - (data.GetLength(1) / 2f - 0.5f) + i * 1f, transform.localPosition.y + 2.6f, transform.localPosition.z);
            cell.GetComponentInChildren<Text>().text = data[0, i];
            columnnames[i] = data[0, i];
            joincolumns.Add(columnnames[i]);
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
                if (j < data.GetLength(0) - 20)
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

    static void changeop(string newop)
    {
        joinop = newop;
    }
}

