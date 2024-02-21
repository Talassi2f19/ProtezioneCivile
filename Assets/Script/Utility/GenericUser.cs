using System;
using UnityEngine;
// ReSharper disable IdentifierTypo

namespace Script.Utility
{
    [Serializable]
    public class GenericUser
    {

        public string name;
        public Vector2 coord;
        public string role;
        
        public GenericUser(string name)
        {
            this.name = name;
            role = "null";
            coord = Vector2.zero;
        }
        
        public GenericUser()
        {
            name = "";
            coord = Vector2.zero;
            role = "null";
        }
        
        public GenericUser(string name, string role, Vector2 coord)
        {
            this.name = name;
            this.role = role;
            this.coord = coord;
        }

        public override string ToString()
        {
            return $"{nameof(name)}: {name}, {nameof(coord)}: {coord}, {nameof(role)}: {role}";
        }
    }
}