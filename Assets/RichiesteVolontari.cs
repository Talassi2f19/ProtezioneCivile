using Proyecto26;
using Script.Utility;
using UnityEngine;

public class RichiesteVolontari : MonoBehaviour
{
    private int GenCode(int code)
    {
        switch (code)
        {
            case 60:
                return 70;
            case 61:
                return 71;
            case 62:
                return 72;
            case 63:
                return 73;
            case 64:
                return 74;
            case 70:
                return 75;
            case 71:
                return 76;
            case 72:
                return 77;
            case 73:
                return 78;
            case 74:
                return 79;
            case 75:
                return 80;
            case 76:
                return 81;
            case 77:
                return 82;
            case 78:
                return 83;
            case 79:
                return 84;
            case 80:
                return 85;
            case 81:
                return 86;
            case 82:
                return 87;
            case 83:
                return 88;
            case 84:
                return 89;
            default:
                return -100;
        }
    }

    public void ConfermaRichiesta(int code)
    {
        RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":"+GenCode(code)+"}").Catch(Debug.Log);
    }
    
}
