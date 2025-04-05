using ApiProyectoBackPeluqueria.Helpers;
using ApiProyectoBackPeluqueria.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NugetProyectoBackPeluqueria.Models;

namespace ApiProyectoBackPeluqueria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementController : ControllerBase
    {

        private RepositoryPeluqueria repo;

        private HelperActionServicesOAuth helper;

        public ManagementController(RepositoryPeluqueria repo, HelperActionServicesOAuth helper)
        {
            this.repo = repo;
            this.helper = helper;
        }

        [Authorize]
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult> GetPerfil(int id)
        {
            Usuario usuario = await this.repo.FindUsuario(id);
            return Ok(usuario);
        }

        [Authorize]
        [HttpPut]
        [Route("[action]")]
        public async Task<ActionResult> UpdateUsuario(Usuario usuario)
        {
            await this.repo.UpdateUsuarioAsync(usuario);
            return Ok();
        }
    }
}
