using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class that contains all the data that needs to be accessed from multiple scripts
/// </summary>
public class DataContainer
{
    /// <summary>
    /// The server ip that is used to connect to the server
    /// </summary>
    public static string ServerIP { get; set; }
    /// <summary>
    /// The connection object that is used to communicate with the server
    /// </summary>
    public static IDnDConnection Conn { get; private set; }
    /// <summary>
    /// 
    /// </summary>
    public static Dictionary<Guid, GameObject> GameObjects;
    public static Dictionary<GameObject, Guid> Guids;
    public static Guid ClientId;

    static DataContainer()
    {
        GameObjects = new();
        Guids = new();
        ServerIP = "10.0.207.3";
    }
    
    public static void CreateConn(IDnDConnection con)
    {
        Conn = con;
        Conn.Connect();
    }
}
