using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace examenBancoBcp.Models
{
    public class ListaCuenta
    {
        public string Nro_cuenta { get; set; }
        public List<SelectListItem> Cuentas { get; set; }

    }
}
