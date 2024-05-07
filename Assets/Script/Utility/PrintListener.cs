using System;
using System.IO;
using UnityEngine;

namespace Script.Utility
{
    public static class PrintListener
    {
        private static int counter = 0;
        public static void Print(String text)
        {
            String kk = "<color=brown>" + counter++ + "</color>";
            foreach (String tmp in text.Split("\n"))
            {
                kk += "<color=orange>" + tmp + "</color>\n";
            }
            Debug.LogWarning(kk);
            
        }
    }
}