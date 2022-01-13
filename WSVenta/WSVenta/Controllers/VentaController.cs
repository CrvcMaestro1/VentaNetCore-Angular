using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVenta.Models;
using WSVenta.Models.Request;
using WSVenta.Models.Response;

namespace WSVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VentaController : ControllerBase
    {
        private readonly DBVentaContext _dbcontext;

        public VentaController()
        {
            _dbcontext = new DBVentaContext();
        }

        [HttpPost]
        public IActionResult Add(VentaRequest model)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                using var transaction = _dbcontext.Database.BeginTransaction();
                try
                {
                    var venta = new Venta();
                    venta.Total = model.Conceptos.Sum(d => d.Cantidad * d.PrecioUnitario);
                    venta.Fecha = DateTime.Now;
                    venta.IdCliente = model.IdCliente;
                    _dbcontext.Venta.Add(venta);
                    _dbcontext.SaveChanges();
                    foreach (var modelConcepto in model.Conceptos)
                    {
                        var concepto = new Models.Concepto();
                        concepto.Cantidad = modelConcepto.Cantidad;
                        concepto.IdProducto = modelConcepto.IdProducto;
                        concepto.PrecioUnitario = modelConcepto.PrecioUnitario;
                        concepto.Importe = modelConcepto.Importe;
                        concepto.IdVenta = venta.Id;
                        _dbcontext.Concepto.Add(concepto);
                        _dbcontext.SaveChanges();
                    }
                    transaction.Commit();
                    respuesta.Exito = 1;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }
            return Ok(respuesta);
        }
    }
}
