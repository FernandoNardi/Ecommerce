using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class Resultado
    {
        public bool erro { get; set; }
        public string mensagem { get; set; }
        public List<Cliente> lista { get; set; }        
    }
}