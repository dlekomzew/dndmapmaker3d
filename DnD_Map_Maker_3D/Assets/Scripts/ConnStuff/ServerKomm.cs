using System.IO;
using System.Net;
using System.Text;
using DefaultNamespace;
using Newtonsoft.Json;
using UnityEngine;

/// <summary>
/// DEPRECATED and only for reference
/// Class was used to communicate with the server
/// </summary>
public static class ServerKomm
{
    /// <summary>
    /// Was used to send the map to the server
    /// </summary>
    /// <param name="meshSpawner">The mesh spawner containing the map</param>
    public static void TellServer(GameObject meshSpawner)
    {
        var request = (HttpWebRequest)WebRequest.Create($"http://{DataContainer.ServerIP}:5180/GameObject/MapChange");
        var mesh = meshSpawner.GetComponent<MeshFilter>().mesh;

        MapData mapData = new()
        {
            triangles = mesh.triangles,
            Vertices = new()
        };
        foreach (var vertex in mesh.vertices)
        {
            mapData.Vertices.Add(new []{vertex.x,vertex.y,vertex.z});
        }

        mapData.sizeX = meshSpawner.GetComponent<PlaneSpawner>().sizeX;
        mapData.sizeY = meshSpawner.GetComponent<PlaneSpawner>().sizeY;

        var json = JsonConvert.SerializeObject(mapData);

        var data = Encoding.UTF8.GetBytes(json);

        request.Method = "POST";
        request.ContentType = "application/json";
        request.ContentLength = data.Length;
        request.Proxy = null!;

        using var stream = request.GetRequestStream();
        stream.Write(data, 0, data.Length);
    }

    /// <summary>
    /// Used to fetch the map from the server
    /// </summary>
    /// <returns>The map from the server</returns>
    public static string FetchMap()
    {
        var request = (HttpWebRequest)WebRequest.Create($"http://{DataContainer.ServerIP}:5180/GameObject/GetMap");
        request.Method = "GET";
        request.Proxy = null!;
        using var re = new StreamReader(request.GetResponse().GetResponseStream()!).ReadToEndAsync();
        return re.Result;
    }

    /// <summary>
    /// Used to check if a map exists on the server
    /// </summary>
    /// <returns>True if a map exists</returns>
    public static bool ExistsMap()
    {
        var request = (HttpWebRequest)WebRequest.Create($"http://{DataContainer.ServerIP}:5180/GameObject/ExistsMap");
        request.Method = "GET";
        request.Proxy = null!;
        using var v = new StreamReader(request.GetResponse().GetResponseStream()!).ReadToEndAsync();
        return v.Result == "true";
    }
}
