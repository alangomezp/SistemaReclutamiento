//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistemaReclutamiento.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PerfilesUsuarios
    {
        public int Id { get; set; }
        public int PerfilId { get; set; }
        public int UsuarioId { get; set; }
    
        public virtual Perfiles Perfiles { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}