using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace examenBancoBcp.Models
{
    public class CuentaModel
    {
        public string NumeroCuenta { get; set; }
        public ListaMoneda Moneda { get; set; }
        public string Tipo { get; set; } 
        public string Nombre { get; set; }
        public CuentaModel()
        {
            Moneda = new ListaMoneda();
        }

    }
}
