using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace examenBancoBcp.Models
{
    public class MovimientoModel
    {
        public string Tipo { get; set; }
        public decimal Monto { get; set; }
        public string Fecha  { get; set; }
        public ListaCuenta Nro_Cuenta { get; set; }
        public MovimientoModel()
        {
            Nro_Cuenta = new ListaCuenta();
        }


    }
}
