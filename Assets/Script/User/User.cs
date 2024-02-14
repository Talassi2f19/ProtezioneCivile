using System;
using UnityEngine;

namespace Script
{
    [Serializable]
    public class User
    {
        public string name;

        public Vector2 cord;
        public string role;
        
        public User(string name)
        {
            this.name = name;
            role = "null";
            cord = Vector2.zero;
        }
        
        public User()
        {
            name = "";
            cord = Vector2.zero;
            role = "null";
        }
        
        public User(string name, string role, Vector2 cord)
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