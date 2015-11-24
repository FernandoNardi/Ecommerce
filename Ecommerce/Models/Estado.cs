using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class Estado
    {

        public string Sigla { get; set; }
        public string Nome { get; set; }
        
        public Estado(string sigla, string nome) {            
            this.Sigla = sigla;
            this.Nome = nome;
        }

        public readonly static IEnumerable<Estado> estados = new Collection<Estado>
        {
            new Estado("AC", "Acre"),
            new Estado("AL", "Alagoas"),
            new Estado("AP", "Amapá"),
            new Estado("AM", "Amazonas"),
            new Estado("BH", "Bahia"),
            new Estado("CE", "Ceará"),
            new Estado("DF", "Distrito Federal"),
            new Estado("ES", "Espírito Santo"),
            new Estado("GO", "Goiás"),
            new Estado("MA", "Maranhão"),
            new Estado("MT", "Mato Grosso"),
            new Estado("MS", "Mato Grosso do Sul"),
            new Estado("MG", "Minas Gerais"),
            new Estado("PA", "Para"),
            new Estado("PB", "Paraiba"),
            new Estado("PR", "Parana"),
            new Estado("PE", "Pernambuco"),
            new Estado("PI", "Piauí"),
            new Estado("RJ", "Rio de Janeiro"),
            new Estado("RN", "Rio Grande do Norte"),
            new Estado("RS", "Rio Grande do Sul"),
            new Estado("RO", "Rondônia"),
            new Estado("RR", "Roraima"),
            new Estado("SC", "Santa Catarina"),
            new Estado("SP", "São Paulo"),
            new Estado("SE", "Sergipe"),
            new Estado("TO", "Tocantins")            
        };

    }
}