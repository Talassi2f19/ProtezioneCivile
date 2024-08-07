using System;
using Defective.JSON;
using Proyecto26;

// ReSharper disable CommentTypo IdentifierTypo StringLiteralTypo
namespace TEST
{
    public class Task
    {
        public String id { get ;}
        public String idRisposta{ get; }
        public String destinatario{ get; }
        public int codTask{ get; }

        public Task(string nomeDestinatario, int codTask, string id = "", string idRisposta = "")
        {
            this.id = id;
            this.idRisposta = idRisposta;
            this.destinatario = nomeDestinatario;
            this.codTask = codTask;
            RestClient.Get("das");
        }

        public Task()
        {
        }

        public override string ToString()
        {
            return $"{nameof(id)}: {id}, {nameof(idRisposta)}: {idRisposta}, {nameof(destinatario)}: {destinatario}, {nameof(codTask)}: {codTask}";
        }

        public JSONObject GetTaks()
        {
            JSONObject json = new JSONObject();
            json.AddField("IdRisposta",idRisposta);
            json.AddField("Destinatario",destinatario);
            json.AddField("CodTask",codTask);
            return json;
        }
    }
}
