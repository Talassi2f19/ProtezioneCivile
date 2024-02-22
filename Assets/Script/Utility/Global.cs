// ReSharper disable IdentifierTypo
namespace Script.Utility
{
    //Questa classe contiene tutte le possibili costanti legate a percorsi relativi del DB e altro
    public struct Global
    {
        public const string MissioniFolder = "Missioni";
        public const string FasiFolder = "Fasi";
        public const string RuoliFolder = "Ruoli";
        public const string PlayerFolder = "Players";
        public const string CandidatiFolder = "Candidati";
       
        public const string GameStatusCodeKey = "GameStatus";
        public const string NomePlayerKey = "Name";
        public const string RuoloPlayerKey = "Role";
        public const string CoordPlayerKey = "Coord";
        public const string IsCompletedKey = "IsCompleted";
        public const string NomeMissioneKey = "NomeMissione";
    }

    public struct Scene
    {
        private const string Folder = "_Scenes";
        public struct User
        {
            private const string UFolder = "User"; 
            public const string Login = Scene.Folder + "/" + Scene.User.UFolder + "/login" ;
            public const string Game = Scene.Folder + "/" + Scene.User.UFolder + "/game" ;
            public const string Elezioni = Scene.Folder + "/" + Scene.User.UFolder + "/elezioni" ;
            public const string AttesaRuoli = Scene.Folder + "/" + Scene.User.UFolder + "/attesaRuoli" ;
            public const string RisultatiElezioni = Scene.Folder + "/" + Scene.User.UFolder + "/risultatiElezioni" ;
            public const string SelezioneCoc = Scene.Folder + "/" + Scene.User.UFolder + "/selezioneCOC" ;
        }
        public struct Master
        {
            private const string MFolder = "Master";
            public const string Start = Scene.Folder + "/" + Scene.Master.MFolder + "/start" ;
            public const string RisultatiElezioni = Scene.Folder + "/" + Scene.Master.MFolder + "/risultatiElezioni" ;
            public const string PlayerLogin = Scene.Folder + "/" + Scene.Master.MFolder + "/playerLogin" ;
            public const string Game = Scene.Folder + "/" + Scene.Master.MFolder + "/game" ;
            public const string Elezioni = Scene.Folder + "/" + Scene.Master.MFolder + "/elezioni" ;
        }
    }
    
    public struct GameStatus
    {
        public const string WaitPlayer = "0";
        public const string Candidatura = "1";
        public const string Votazione = "2";
        public const string RisultatiElezioni = "3";
        public const string Gioco = "4";
        public const string End = "-1";
    }
}