using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;


public class MYSQLTest : MonoBehaviour
{
    public string uid = "bchuser";

    public string domain = "34.217.102.173";
    public string port = "6023";
    public string pwd = "qwe!@3";
    public string dbName = "bchgamedb";



    // Start is called before the first frame update
    void Start()
    {


    }

    public void GetGdStage(string stageName)
    {
        using (MySqlConnection connection = new MySqlConnection(
            $"Server={domain};" +
            $"Port={port};" +
            $"Database={dbName};" +
            $"Uid={uid};" +
            $"Pwd={pwd}"
            ))
        {
<<<<<<< HEAD
            string nsa2 = null;
=======
            string nsa1 = null;
>>>>>>> LobbySet
            connection.Open();
            string sql_ = $"SELECT * FROM gdstage WHERE id = '{stageName}'";
            MySqlCommand command = new MySqlCommand(sql_, connection);
            MySqlDataReader table = command.ExecuteReader();

            while (table.Read())
            {
                print($"{table["id"]} {table["zoneId"]}");
            }
            table.Close();
            connection.Close();

        }


    }
}
