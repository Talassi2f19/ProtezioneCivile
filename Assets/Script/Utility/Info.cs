// ReSharper disable CommentTypo IdentifierTypo

namespace Script.Utility
{
    public static class Info
    {
        //public const string DBUrl = "https://prova-3266d-default-rtdb.europe-west1.firebasedatabase.app/";
        public const string DBUrl = "https://prtcv-de7d0-default-rtdb.europe-west1.firebasedatabase.app/";

        public static string SessionCode = "";

        public static User LocalUser = new User();

        public const int MaxPlayer = 30;

        public const int SessionCodeLength = 4;
        
        public struct GameStatus
        {
            public const string WaitPlayer = "0";
            public const string GenericWait = "1";
            public const string Candidatura = "2";
            public const string Votazione = "3";
            public const string Gioco = "4";
            public const string End = "-1";
        }

        public static void Reset()
        {
            SessionCode = "";
            LocalUser = new User();
        }
    }
    
}