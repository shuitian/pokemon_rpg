using UnityEngine;
using System;
using Mono.Data.Sqlite;

public class Sql : MonoBehaviour {

    static SqliteConnection dbConnection;
    static SqliteCommand dbCommand;
    static SqliteDataReader reader;
    // Use this for initialization

    static public SqliteConnection OpenDB(string dbName)
    {
        try
        {
            if (dbConnection == null) 
            {
                dbConnection = new SqliteConnection("data source=" + Application.dataPath + "/StreamingAssets/" + dbName);
                Debug.Log("Connected to db");
                dbConnection.Open();
                
                return dbConnection;
            }else
            {
                return dbConnection;
            }
        }
        catch (Exception e)
        {
            string temp1 = e.ToString();
            Debug.Log(temp1);
        }
        return null;
    }

    static public void CloseDB()
    {
        if (dbCommand != null)
        {
            dbCommand.Dispose();
        }
        dbCommand = null;
        if (reader != null)
        {
            reader.Dispose();
        }
        reader = null;
        if (dbConnection != null)
        {
            dbConnection.Close();
        }
        dbConnection = null;
        Debug.Log("Disconnected from db.");
    }

    static public SqliteDataReader ExecuteQuery(string sqlQuery)
    {
        dbCommand = OpenDB("monsters.db").CreateCommand();
        dbCommand.CommandText = sqlQuery;
        reader = dbCommand.ExecuteReader();
        return reader;
    }

    static public SqliteDataReader ShowTable()
    {
        string query = "SELECT * FROM MONSTER";
        return ExecuteQuery(query);
    }

    static public MonsterData GetMonsterData(int id)
    {
        string query = "SELECT * FROM MONSTER where id = " + id;
        reader = ExecuteQuery(query);
        MonsterData monster = new MonsterData();
        if (reader.Read())
        {
            monster.id = reader.GetInt32(0);
            monster.name = reader.GetString(1);
            monster.hp = reader.GetFloat(2);
            monster.attack = reader.GetFloat(3);
            monster.defence = reader.GetFloat(4);
            monster.gold = reader.GetInt32(5);
        }
        reader.Dispose();
        return monster;
    }
}
