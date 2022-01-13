using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVenta.Models;
using WSVenta.Models.Response;
using WSVenta.Models.Request;
using Microsoft.AspNetCore.Authorization;

namespace WSVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        private readonly DBVentaContext _dbcontext;

        public ClienteController()
        {
            _dbcontext = new DBVentaContext();
        }

        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                var listaClientes = _dbcontext.Cliente.OrderByDescending(d => d.Id).ToList();
                oRespuesta.Exito = 1;
                oRespuesta.Data = listaClientes;
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [HttpPost]
        public IActionResult Add(ClienteRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                Cliente oCliente = new Cliente { Nombre = oModel.Nombre };
                _dbcontext.Cliente.Add(oCliente);
                _dbcontext.SaveChanges();
                oRespuesta.Exito = 1;
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [HttpPut]
        public IActionResult Edit(ClienteRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                Cliente oCliente = _dbcontext.Cliente.Find(oModel.Id);
                oCliente.Nombre = oModel.Nombre;
                _dbcontext.Entry(oCliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _dbcontext.SaveChanges();
                oRespuesta.Exito = 1;
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                Cliente oCliente = _dbcontext.Cliente.Find(Id);
                _dbcontext.Remove(oCliente);
                _dbcontext.SaveChanges();
                oRespuesta.Exito = 1;
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }
    }
}
