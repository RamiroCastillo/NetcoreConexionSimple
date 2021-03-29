using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using examenBancoBcp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace examenBancoBcp.Controllers
{
    public class MovimientoController : Controller
    {
        public string connection = "Data Source=DESKTOP-VFJ4Q33\\SQLSERVER2019;Initial Catalog=ExamenBcp;User ID=Usuario;Password=Password";
        // GET: Movimiento
        public ActionResult Index()
        {
            return View();
        }

        // GET: Movimiento/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Movimiento/Create
        public ActionResult Create()
        {
            MovimientoModel movimiento = new MovimientoModel();
            movimiento.Nro_Cuenta.Cuentas = cargarCuentas( connection );
            return View( movimiento );
        }

        // POST: Movimiento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovimientoModel movimientoModel)
        {
            string tipo;
            decimal monto;
            movimientoModel.Nro_Cuenta.Cuentas = cargarCuentas(connection);
            var selectedItem = movimientoModel.Nro_Cuenta.Cuentas.Find(p => p.Value == movimientoModel.Nro_Cuenta.Nro_cuenta.ToString());
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                sqlConnection.Open();
                string query = "insert into dbo.MOVIMIENTOS ( NRO_CUENTA,FECHA,TIPO,MONTO ) values ( @NroCuenta,@Fecha,@Tipo,@Monto)";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                selectedItem.Selected = true;
                cmd.Parameters.AddWithValue("@NroCuenta", selectedItem.Value);
                tipo = "A";
                monto = movimientoModel.Monto;
                if (Request.Form["Debitar"].Equals("Debitar") )
                {
                    tipo = "D";
                    monto = movimientoModel.Monto * -1;

                }
                cmd.Parameters.AddWithValue("@Monto", monto);
                cmd.Parameters.AddWithValue("@Tipo", tipo);
                cmd.Parameters.AddWithValue("@Fecha", DateTime.Now.ToString());
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index","Cuenta");
        }

        private static List<SelectListItem> cargarCuentas(string con)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            using (SqlConnection sqlConnection = new SqlConnection(con))
            {
                using (SqlCommand sqlCommand = new SqlCommand("select NRO_CUENTA from dbo.CUENTAS"))
                {
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlConnection.Open();
                        sqlDataAdapter.SelectCommand = sqlCommand;
                        SqlDataReader sdr = sqlCommand.ExecuteReader();
                        while (sdr.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Text = sdr["NRO_CUENTA"].ToString(),
                                Value = sdr["NRO_CUENTA"].ToString(),
                                Selected = false
                            });
                        }
                    }
                    //sqlConnection.Close();
                }
            }
            return items;
        }

        // GET: Movimiento/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Movimiento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Movimiento/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Movimiento/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}