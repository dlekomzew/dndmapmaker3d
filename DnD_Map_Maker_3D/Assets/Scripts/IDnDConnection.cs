﻿using System.Collections.Generic;
using ConnStuff;
using DefaultNamespace;
using UnityEngine;

public interface IDnDConnection
{
    void SendMap(MapData map);
    void AddGameObject(GameObject gameObject);
    public List<JKGameObject> GetGameObjects();
    bool MapExists();
    List<GameObject> OnConnectGO();

    MapData OnConnectMap();

    bool Connected();

    void Dispose();
}