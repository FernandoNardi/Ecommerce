using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{

    public class Cliente
    {

        public string _id { get; set; }

        [Required(ErrorMessage = "CPF obrigatório")]
        [StringLength(50, MinimumLength = 14,  ErrorMessage = "O cpf deve conter obrigatóriamente 11 números.")]
        [DisplayName("CPF")]
        public string cpf { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        [DisplayName("Nome")]
        public string nome { get; set; }

        [Required(ErrorMessage = "E-mail obrigatório")]
        [DisplayName("e-mail")]
        [RegularExpression(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$", ErrorMessage = "Informe um email válido")]
        public string email { get; set; }

        [Required(ErrorMessage = "Estado civil obrigatório")]
        [DisplayName("Estado civil")]
        public string estadocivil { get; set; }

        [DisplayName("Telefones")]
        public List<Telefone> telefones { get; set; }

        [Required(ErrorMessage = "Logradouro obrigatório")]
        [DisplayName("Logradouro")]
        public string logradouro { get; set; }

        [Required(ErrorMessage = "Somente número")]
        public Nullable<int> numero { get; set; }

        [Required(ErrorMessage = "Bairro obrigatório")]
        [DisplayName("Bairro")]
        public string bairro { get; set; }

        [Required(ErrorMessage = "Cidade obrigatório")]
        [DisplayName("Cidade")]
        public string cidade { get; set; }

        [Required(ErrorMessage = "Estado obrigatório")]
        [DisplayName("Estado")]
        public string estado { get; set; }
    }

    public class Telefone
    {
        //public Nullable<int> ddd { get; set; }
        public string numero { get; set; }
    }

}