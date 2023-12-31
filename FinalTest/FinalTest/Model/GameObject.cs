﻿namespace FinalTest.Model
{
    public class GameObject
    {
        // Pos3, Rot3,  Scale3, Modeltype
        public float[] Pos3 { get; set; }
        public float[] Rot3 { get; set; }
        public float[] Scale3 { get; set; }
        public string Modeltype { get; set; }
        public Guid Guid { get; set; }
        public DateTime LastChanged { get; set; }
        public Guid ClientId { get; set; }

        public GameObject(float[] pos3, float[] rot3, float[] scale3, string modeltype, Guid guid, DateTime lastChanged, Guid clientId)
        {
            Pos3 = pos3;
            Rot3 = rot3;
            Scale3 = scale3;
            Modeltype = modeltype;
            this.Guid = guid;
            this.LastChanged = DateTime.Now;
            ClientId = clientId;
        }

        public override string ToString()
        {
            return "GameObject";
            //return $"Pos: [{Pos3[0]}, {Pos3[1]}, {Pos3[2]}], Rot: [{Rot3[0]}, {Rot3[1]}, {Rot3[2]}], Scale : [{Scale3[0]}, {Scale3[1]}, {Scale3[2]}]";
        }

    }
}
