using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimulazioneWebApi.Model
{
    public partial class Impiegati
    {
        public Impiegati()
        {
            Dipartimenti = new HashSet<Dipartimenti>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }
        [Required]
        [StringLength(50)]
        public string Cognome { get; set; }
        [Column(TypeName = "date")]
        public DateTime DataNascita { get; set; }
        public int? IdDipartimento { get; set; }
        [Column(TypeName = "date")]
        public DateTime DataAssunzione { get; set; }

        [ForeignKey("IdDipartimento")]
        [InverseProperty("Impiegati")]
        public Dipartimenti IdDipartimentoNavigation { get; set; }
        [InverseProperty("IdManagerNavigation")]
        public ICollection<Dipartimenti> Dipartimenti { get; set; }
    }
}
