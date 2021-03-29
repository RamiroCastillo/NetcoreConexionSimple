using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace examenBancoBcp.Models
{
    public class ListaMoneda
    {
        public List<SelectListItem> Monedas { get; set; }
        public string Moneda { get; set; }
        public string Descripcion { get; set; }

    }
    //@Html.DropDownList("Monedas",items,"Selecciona una tipo de moneda",new { @class="form-control" })

}
