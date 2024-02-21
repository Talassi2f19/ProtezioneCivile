using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Utility
{
    [Serializable]
    public class GenericUser
    {
        //TODO cambiare i nomi delle variabili
        public string Name;
        public Vector2 Coord;
        public string Role;
        
        public GenericUser(string name)
        {
            this.Name = name;
            Role = "null";
            Coord = Vector2.zero;
        }
        
        public GenericUser()
        {
            Name = "";
            Coord = Vector2.zero;
            Role = "null";
        }
        
        public GenericUser(string name, string role, Vector2 coord)
        {
            this.Name = name;
            this.Role = role;
            this.Coord = coord;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Coord)}: {Coord}, {nameof(Role)}: {Role}";
        }
    }
}