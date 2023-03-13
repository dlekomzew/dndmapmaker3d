using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DefaultNamespace;
using Newtonsoft.Json;
using UnityEngine;

public class ServerKomm
{
    public static async void TellServer(GameObject meshSpawner)
    {
        var request = (HttpWebRequest)WebRequest.Create($"http://{DataContainer.ServerIP}:5180/GameObject/MapChange");
        var mesh = meshSpawner.GetComponent<MeshFilter>().mesh;

        MapData mapData = new MapData();
        mapData.Triangles = mesh.triangles;
        mapData.Vertices = new();
        foreach (var vertex in mesh.vertices)
        {
            mapData.Vertices.Add(new []{vertex.x,vertex.y,vertex.z});
        }

        var json = JsonConvert.SerializeObject(mapData);

        var data = Encoding.UTF8.GetBytes(json);

        request.Method = "POST";
        request.ContentType = "application/json";
        request.ContentLength = data.Length;
        request.Proxy = null!;

        using (var stream = request.GetRequestStream())
        {
            stream.Write(data, 0, data.Length);
        }
    }

    public static async Task<string> FetchMap()
    {
        var request = (HttpWebRequest)WebRequest.Create($"http://{DataContainer.ServerIP}:5180/GameObject/GetMap");
        request.Method = "GET";
        request.Proxy = null!;
        return new StreamReader(request.GetResponse().GetResponseStream()!).ReadToEndAsync().Result;
    }

    public static async Task<bool> ExistsMap()
    {
        var request = (HttpWebRequest)WebRequest.Create($"http://{DataContainer.ServerIP}:5180/GameObject/ExistsMap");
        request.Method = "GET";
        request.Proxy = null;
        return new StreamReader(request.GetResponse().GetResponseStream()).ReadToEndAsync().Result == "true";
    }
    public static MapData MapFromJson(string jsonString, GameObject meshSpawner)
    {
        
        return JsonConvert.DeserializeObject<MapData>(jsonString);
    }
}
