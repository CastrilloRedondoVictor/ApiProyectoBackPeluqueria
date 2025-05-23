﻿using ApiProyectoBackPeluqueria.Helpers;
using ApiProyectoBackPeluqueria.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NugetProyectoBackPeluqueria.Models;
using Newtonsoft.Json.Linq;


namespace ApiProyectoBackPeluqueria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {

        private RepositoryPeluqueria repo;

        private HelperActionServicesOAuth helper;

        public ReservasController(RepositoryPeluqueria repo, HelperActionServicesOAuth helper)
        {
            this.repo = repo;
            this.helper = helper;
        }

        [Authorize]
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> InsertarReserva(ReservaModel reserva)
        {
            await this.repo.InsertarReservaAsync(reserva);
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        [Route("[action]")]
        [Route("[action]/{id}")]
        public async Task<ActionResult> DeleteReserva(int id)
        {
            var reserva = await this.repo.FindReservaAsync(id);
            if (reserva != null)
            {
                await this.repo.EliminarReservaAsync(id);
                return Ok();
            }
            else
            {
                return NotFound("Reserva no encontrada");
            }
        }

        [Authorize]
        [HttpGet]
        [Route("[action]")]
        [Route("[action]/{id}")]
        public async Task<ActionResult> FindReserva(int id)
        {
            ReservaView reserva = await this.repo.FindReservaAsync(id);
            return Ok(reserva);
        }

        [Authorize]
        [HttpGet]
        [Route("[action]")]
        [Route("[action]/{id}")]
        public async Task<ActionResult> GetProximaReservaUsuario(int id)
        {
            Reserva reserva = await this.repo.GetProximaReservaUsuarioAsync(id);
            return Ok(reserva);
        }

        [Authorize]
        [HttpGet]
        [Route("[action]")]
        [Route("[action]/{id}")]
        public async Task<ActionResult> GetServicioReserva(int id)
        {
            string servicio = await this.repo.GetServicioReservaAsync(id);
            return Ok(servicio);
        }

        [Authorize]
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> AgregarDisponibilidadRango([FromBody] AgregarDisponibilidadModel body)
        {
            DisponibilidadResponse response = await this.repo.AgregarDisponibilidadRangoAsync(
                body.FechaInicio,
                body.FechaFin
            );

            // Devolver como un objeto JSON con las propiedades esperadas
            return Ok(response);
        }


        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> ObtenerCitasConHoras()
        {
            var reservas = await this.repo.ObtenerCitasConHoras();
            return Ok(reservas);
        }


        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> ObtenerDiasDisponibles()
        {
            var dias = await this.repo.ObtenerDiasDisponibles();
            return Ok(dias);
        }

        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> ObtenerReservasClientes()
        {
            var reservas = await this.repo.ObtenerReservasClientesAsync();
            return Ok(reservas);
        }

        [Authorize]
        [HttpPost]
        [Route("[action]/{servicioId}")]
        public async Task<ActionResult> ObtenerHorariosDisponiblesPorFecha(int servicioId, [FromBody] string fecha)
        {
            var fechaDate = DateTime.Parse(fecha);
            var reservas = await this.repo.ObtenerHorariosDisponiblesPorFechaAsync(servicioId, fechaDate);
            return Ok(reservas);
        }
    }
}
