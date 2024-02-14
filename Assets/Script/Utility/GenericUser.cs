using System;
using UnityEngine;

namespace Script.Utility
{
    [Serializable]
    public class GenericUser
    {
        public string name;

        public Vector2 cord;
        public string role;
        
        public GenericUser(string name)
        {
            this.name = name;
            role = "null";
            cord = Vector2.zero;
        }
        
        public GenericUser()
        {
            name = "";
            cord = Vector2.zero;
            role = "null";
        }
        
        public GenericUser(string name, string role, Vector2 cord)
        {
            this.name = name;
            this.role = role;
            this.cord = cord;
        }

        public override string ToString()
        {
            return $"{nameof(name)}: {name}, {nameof(cord)}: {cord}, {nameof(role)}: {role}";
        }
    }
}