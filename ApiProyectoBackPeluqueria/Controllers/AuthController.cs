﻿using ApiProyectoBackPeluqueria.Helpers;
using ApiProyectoBackPeluqueria.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NugetProyectoBackPeluqueria.Models;
using System.IdentityModel.Tokens.Jwt;

namespace ApiProyectoBackPeluqueria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private RepositoryPeluqueria repo;

        private HelperActionServicesOAuth helper;

        public AuthController(RepositoryPeluqueria repo, HelperActionServicesOAuth helper)
        {
            this.repo = repo;
            this.helper = helper;
        }

    
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Login(LoginModel usuario)
        {
            Usuario usuarioLogueado = await repo.GetUsuarioByEmailAsync(usuario.Email);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(usuario.Password, usuarioLogueado.Contrasena))
            {
                return Unauthorized();
            }
            else
            {
                SigningCredentials credentials = new SigningCredentials(this.helper.GetKeyToken(), SecurityAlgorithms.HmacSha256);
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: this.helper.Issuer,
                    audience: this.helper.Audience,
                    signingCredentials: credentials,
                    expires: DateTime.Now.AddMinutes(60),
                    notBefore: DateTime.UtcNow
                );
                var user = new { Nombre = usuarioLogueado.Nombre, Id = usuarioLogueado.Id, Imagen = usuarioLogueado.Imagen, IdRolUsuario = usuarioLogueado.IdRolUsuario };
                return Ok(new { authToken = new JwtSecurityTokenHandler().WriteToken(token), user = user });
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Register(Usuario usuario)
        {
            await this.repo.RegisterAsync(usuario);
            return Ok();
        }
    }
}
