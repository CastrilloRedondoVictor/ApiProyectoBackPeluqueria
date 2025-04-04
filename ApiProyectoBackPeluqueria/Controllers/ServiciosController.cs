using ApiProyectoBackPeluqueria.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NugetProyectoBackPeluqueria.Models;

namespace ApiProyectoBackPeluqueria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        private RepositoryPeluqueria repo;

        public ServiciosController(RepositoryPeluqueria repo)
        {
            this.repo = repo;
        }

        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetServicios()
        {
            var servicios = await this.repo.GetServiciosAsync();
            return Ok(servicios);
        }  
        
        [Authorize]
        [HttpGet]
        [Route("[action]")]
        [Route("[action]/{id}")]
        public async Task<ActionResult> FindServicio(int id)
        {
            var servicio = await this.repo.FindServicioAsync(id);
            if (servicio != null)
            {
                return Ok(servicio);
            }
            else
            {
                return NotFound("Servicio no encontrado");
            }
        }

        [Authorize]
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> InsertarServicio(Servicio servicio)
        {
            await this.repo.InsertarServicioAsync(servicio);
            return Ok();
        }

        [Authorize]
        [HttpPut]
        [Route("[action]")]
        public async Task<ActionResult> UpdateServicio(Servicio servicio)
        {
            await this.repo.ActualizarServicioAsync(servicio);
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        [Route("[action]")]
        [Route("[action]/{id}")]
        public async Task<ActionResult> DeleteServicio(int id)
        {
            var servicio = await this.repo.FindServicioAsync(id);
            if (servicio != null)
            {
                await this.repo.EliminarServicioAsync(id);
                return Ok();
            }
            else
            {
                return NotFound("Servicio no encontrado");
            }
        }

    }
}
