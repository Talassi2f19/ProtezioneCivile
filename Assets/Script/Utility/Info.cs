// ReSharper disable CommentTypo IdentifierTypo

namespace Script.Utility
{
    public static class Info
    {
        //public const string DBUrl = "https://prova-3266d-default-rtdb.europe-west1.firebasedatabase.app/";
        public const string DBUrl = "https://prtcv-de7d0-default-rtdb.europe-west1.firebasedatabase.app/";

        public static string sessionCode = "";

        public static GenericUser localUser = new GenericUser();

        public const int MaxPlayer = 28;

        public const int SessionCodeLength = 4;
        
        public static void Reset()
        {
            sessionCode = "";
            localUser = new GenericUser();
        }
    }
    
}