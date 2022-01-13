using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVenta.Models;
using WSVenta.Models.Request;

namespace WSVenta.Services
{
    public class VentaService : IVentaService
    {
        private readonly DBVentaContext _dbcontext;

        public VentaService()
        {
            _dbcontext = new DBVentaContext();
        }

        public void Add(VentaRequest model)
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
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw new Exception("Ocurrió un error en la inserción");
            }
        }
    }
}
