﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DefaultNamespace;
using DnD_3D.ServerConnection.Default;
using Newtonsoft.Json;
using UnityEngine;

namespace ConnStuff
{
    public class WebserviceCon : IDnDConnection
    {
        public void SendMap(MapData map)
        {
            var request = (HttpWebRequest)WebRequest.Create($"http://{DataContainer.ServerIP}:5180/GameObject/MapChange");
            var json = JsonConvert.SerializeObject(map);

            var data = Encoding.UTF8.GetBytes(json);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            request.Proxy = null!;

            using var stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);
        }

        public event EventHandler<MapEventArgs> GetMap;
        public void AddGameObject(GameObject gameObject)
        {
            var request = (HttpWebRequest)WebRequest.Create($"http://{DataContainer.ServerIP}:5180/GameObject/PostChange");
            JKGameObject jkGameObject = new JKGameObject(gameObject);
            DataContainer.GameObjects.Add(jkGameObject.Guid,gameObject);
            var json = JsonConvert.SerializeObject(jkGameObject);

            var data = Encoding.UTF8.GetBytes(json);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            request.Proxy = null!;

            using var stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);
        }

        public event EventHandler<GameObjectEventArgs> GetGameObjects;
        public Task<bool> MapExists()
        {
            var request = (HttpWebRequest)WebRequest.Create($"http://{DataContainer.ServerIP}:5180/GameObject/ExistsMap");
            request.Method = "GET";
            request.Proxy = null!;
            using var v = new StreamReader(request.GetResponse().GetResponseStream()!).ReadToEndAsync();
            var t = new Task<bool>(() =>  v.Result == "true");
            return t;
        }

        public List<GameObject> OnConnectGO()
        {
            throw new NotImplementedException();
        }

        public MapData OnConnectMap()
        {
            throw new NotImplementedException();
        }

        public bool Connected()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}