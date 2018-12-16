using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimulazioneWebApi.Model.DTOs
{
    public class ImpiegatoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public DateTime DataNascita { get; set; }
        public int? IdDipartimento { get; set; }
        public DateTime DataAssunzione { get; set; }

        public ImpiegatoDTO (Impiegati imp)
        {
            Id = imp.Id;
            Nome = imp.Nome;
            Cognome = imp.Cognome;
            DataNascita = imp.DataNascita;
            IdDipartimento = imp.IdDipartimento;
            DataAssunzione = imp.DataAssunzione;
        }
    }
}
