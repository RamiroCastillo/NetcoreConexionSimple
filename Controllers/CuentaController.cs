using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using examenBancoBcp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace examenBancoBcp.Controllers
{
    public class CuentaController : Controller
    {
        public string connection = "Data Source=DESKTOP-VFJ4Q33\\SQLSERVER2019;Initial Catalog=ExamenBcp;User ID=Usuario;Password=Password";
        // GET: Cuenta
        [HttpGet]
        public ActionResult Index()
        {
            DataTable tablaCuentas = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                sqlConnection.Open();
                string query = "select c.NRO_CUENTA, m.MONEDA, c.NOMBRE, m.DESCRIPCION, c.TIPO, c.SALDO FROM dbo.CUENTAS c INNER JOIN MONEDAS m on(c.MONEDA = m.MONEDA)";
                SqlDataAdapter adaptador = new SqlDataAdapter(query, sqlConnection);
                adaptador.Fill(tablaCuentas);
            }
            return View(tablaCuentas);
        }

        // GET: Cuenta/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cuenta/Create
        public ActionResult Create()
        {
            CuentaModel cuenta = new CuentaModel();
            cuenta.Moneda.Monedas = cargarMonedas( connection );
            //List<ListaMoneda> lista = new List<ListaMoneda>();

            //ViewBag.items = items;
            return View(cuenta);
        }

        private static List<SelectListItem> cargarMonedas( string con )
        {
            List<SelectListItem> items = new List<SelectListItem>();
            using (SqlConnection sqlConnection = new SqlConnection(con))
            {
                using (SqlCommand sqlCommand = new SqlCommand("select * from dbo.MONEDAS"))
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
                                Text = sdr["DESCRIPCION"].ToString(),
                                Value = sdr["MONEDA"].ToString(),
                                Selected = false
                            });
                        }
                    }
                    //sqlConnection.Close();
                }
            }
            return items;
        }

        // POST: Cuenta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CuentaModel cuentaModel)
        {
            string tipo;
            cuentaModel.Moneda.Monedas = cargarMonedas( connection );
            var selectedItem = cuentaModel.Moneda.Monedas.Find(p => p.Value == cuentaModel.Moneda.Moneda.ToString());
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    sqlConnection.Open();
                    string query = "insert into CUENTAS ( NRO_CUENTA,MONEDA,TIPO,NOMBRE ) values ( @NroCuenta,@Moneda,@Tipo,@nombre)";
                    SqlCommand cmd = new SqlCommand(query, sqlConnection);
                    cmd.Parameters.AddWithValue("@NroCuenta", cuentaModel.NumeroCuenta);
                    selectedItem.Selected = true;
                    cmd.Parameters.AddWithValue("@Moneda", selectedItem.Value);
                    if( cuentaModel.NumeroCuenta.Length == 14 )
                    {
                        tipo = "Ahorro";
                    }
                    else
                    {
                        tipo = "Corriente";
                    }
                    cmd.Parameters.AddWithValue("@Tipo", tipo);
                    cmd.Parameters.AddWithValue("@nombre", cuentaModel.Nombre);
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction(nameof(Index));
        }
        
        // GET: Cuenta/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cuenta/Edit/5
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

        // GET: Cuenta/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cuenta/Delete/5
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