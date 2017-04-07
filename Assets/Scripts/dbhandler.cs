using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class dbhandler : MonoBehaviour {
    public static string[,] table;
    public static string[,] table1;

    // database names
    private List<string> databaseNames;
    // list of tables
    
    // Use this for initialization
    void Start ()
	{
        databaseNames = new List<string>();
        databaseNames.Add("testdb");
        //databaseNames.Add("test");
        //databaseNames.Add("test2");
        List<string> rows = new List<string>();
        List<string> columns = new List<string>();

	    try
	    {
         
	            string conn = "URI=file:" + Application.dataPath + "/testdb.db"; //Path to database.

            //table 1
	            IDbConnection dbconn;
	            dbconn = (IDbConnection) new SqliteConnection(conn);
	            dbconn.Open(); //Open connection to the database.
	            IDbCommand dbcmd = dbconn.CreateCommand();

	            string sqlQuery = "SELECT * " + "FROM customers LIMIT 100";
	            dbcmd.CommandText = sqlQuery;
	            IDataReader reader = dbcmd.ExecuteReader();
	            for (int i = 0; i < reader.FieldCount; i++)
	            {
	                columns.Add(reader.GetName(i));
	            }

	            while (reader.Read())
	            {
	                string row = reader.GetInt32(0) + "," + reader.GetString(1) + "," + reader.GetString(2);
	                rows.Add(row);
	            }
	            table = new string[rows.Count, reader.FieldCount];

	            for (int i = 0; i < reader.FieldCount; i++)
	            {
                    table[0, i] = columns[i];
                    for (int j = 1; j < rows.Count - 1; j++)
	                    {
	                        string[] row = rows[j - 1].Split(',');
	                        table[j, i] = row[i];
	                    }
	            }
            reader.Close();
            dbcmd.Dispose();
            dbconn.Close();
            //end table1

            // table 2
            dbconn = (IDbConnection)new SqliteConnection(conn);
            dbconn.Open(); //Open connection to the database.
            dbcmd = dbconn.CreateCommand();
            string query = "SELECT * FROM orders LIMIT 100";
            dbcmd.CommandText = query;
            reader = dbcmd.ExecuteReader();
            columns.Clear();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                columns.Add(reader.GetName(i));
            }
            rows.Clear();
            while (reader.Read())
            {
                string row = reader.GetInt32(0) + "," + reader.GetInt32(1)+ "," + reader.GetInt32(2) + "," + reader.GetInt32(3);
                rows.Add(row);
            }
            table1 = new string[rows.Count, columns.Count];

            for (int i = 0; i < reader.FieldCount; i++)
            {

                table1[0, i] = columns[i];

                for (int j = 1; j < rows.Count; j++)
                {
                    string[] row = rows[j - 1].Split(',');
                    table1[j, i] = row[i];
                }
            }
                reader.Close();
	            dbcmd.Dispose();
	            dbconn.Close();
	        // end table2

	    }
	    catch (Exception e)
	    {
	        Debug.Log(e.Message);
	    }
	}

	// Update is called once per frame
	void Update () {
		
	}
}