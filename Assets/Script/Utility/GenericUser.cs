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
        public Ruoli role;
        
        public GenericUser(string name)
        {
            this.name = name;
            role = Ruoli.Null;
            coord = Vector2.zero;
        }
        
        public GenericUser()
        {
#if UNITY_EDITOR
            name = "admin";
#else
            name = "";
#endif
            coord = Vector2.zero;
            role = Ruoli.Null;
        }
        
        public GenericUser(string name, Ruoli role, Vector2 coord)
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