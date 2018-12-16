using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimulazioneWebApi.Model
{
    public partial class Dipartimenti
    {
        public Dipartimenti()
        {
            Impiegati = new HashSet<Impiegati>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }
        [Required]
        [StringLength(50)]
        public string Area { get; set; }
        public int IdManager { get; set; }

        [ForeignKey("IdManager")]
        [InverseProperty("Dipartimenti")]
        public Impiegati IdManagerNavigation { get; set; }
        [InverseProperty("IdDipartimentoNavigation")]
        public ICollection<Impiegati> Impiegati { get; set; }
    }
}
