
using System;
using Defective.JSON;

namespace Script.test
{
    public class Task
    {
        public String id { get; set; }
        public String idRisposta{ get; set; }
        public String destinatario{ get; set; }
        public int codTask{ get; set; }

        public Task(string nomeDestinatario, int codTask, string id = "", string idRisposta = "")
        {
            this.id = id;
            this.idRisposta = idRisposta;
            this.destinatario = nomeDestinatario;
            this.codTask = codTask;
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
