﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyeApiProyectoBackPeluqueriactoBackPeluqueria.Models
{
    [Table("Vista_Usuarios")]
    public class UsuarioView
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Nombre")]
        public string Nombre { get; set; }

        [Column("Apellidos")]
        public string Apellidos { get; set; }

        [Column("Telefono")]
        public string Telefono { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("ColorAvatar")]
        public string ColorAvatar { get; set; }

        [Column("Imagen")]
        public string Imagen { get; set; }

        [Column("Rol")]
        public string Rol { get; set; }
    }
}
