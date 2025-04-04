using ApiProyectoBackPeluqueria.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProyectoBackPeluqueria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {

        private RepositoryPeluqueria repo;

        public ClientesController(RepositoryPeluqueria repo)
        {
            this.repo = repo;
        }

        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetClientes()
        {
            var clientes = await this.repo.GetClientesAsync();
            return Ok(clientes);
        }

        [Authorize]
        [HttpDelete]
        [Route("[action]")]
        [Route("[action]/{id}")]
        public async Task<ActionResult> DeleteUsuario(int id)
        {
            var usuario = await this.repo.FindUsuario(id);
            if (usuario != null)
            {
                await this.repo.DeleteUsuarioAsync(id);
                return Ok();
            }
            else
            {
                return NotFound("Usuario no encontrado");
            }
        }
    }
}
