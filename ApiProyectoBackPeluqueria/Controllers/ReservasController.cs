using ApiProyectoBackPeluqueria.Helpers;
using ApiProyectoBackPeluqueria.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NugetProyectoBackPeluqueria.Models;

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

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> FindReserva(int id)
        {
            ReservaView reserva = await this.repo.FindReservaAsync(id);
            return Ok(reserva);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetProximaReservaUsuario(int id)
        {
            Reserva reserva = await this.repo.GetProximaReservaUsuarioAsync(id);
            return Ok(reserva);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetServicioReserva(int id)
        {
            string servicio = await this.repo.GetServicioReservaAsync(id);
            return Ok(servicio);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> AgregarDisponibilidadRango(DateTime fechaInicio, DateTime fechaFin)
        {
            await this.repo.AgregarDisponibilidadRangoAsync(fechaInicio, fechaFin);
            return Ok();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> ObtenerCitasConHoras()
        {
            var reservas = await this.repo.ObtenerCitasConHoras();
            return Ok(reservas);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> ObtenerDiasDisponibles()
        {
            var dias = await this.repo.ObtenerDiasDisponibles();
            return Ok(dias);
        }
    }
}
