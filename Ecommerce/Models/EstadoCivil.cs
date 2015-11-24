using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class EstadoCivil
    {
        public EstadoCivil(string id, string descricao) {
            this.Id = id;
            this.Descricao = descricao;
        }

        public string Id { get; set; }
        public string Descricao { get; set; }

        public readonly static IEnumerable<EstadoCivil> est = new Collection<EstadoCivil>
        {            
            new EstadoCivil("Casado", "Casado"),
            new EstadoCivil("Divorciado", "Divorciado"),
            new EstadoCivil("Solteiro", "Solteiro")
        };
    }
    
}