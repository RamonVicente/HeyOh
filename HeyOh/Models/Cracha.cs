using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HeyOh.Models
{

    public class Cracha{

        public HttpPostedFileBase layoutCracha { get; set; }
        public HttpPostedFileBase logoMarca { get; set; }
        [DataType(DataType.MultilineText)]
        public string participantes { get; set; }

        //trata os dados de participante do form e cria uma lista
        public List<Participante> getParticipantesLista([DataType(DataType.MultilineText)] string participantes) {
            List<Participante> participantesLista = new List<Participante>();

            participantes.ToString();
            return participantesLista;
        }

        public void converteParaPDF(){

        }

    }
}