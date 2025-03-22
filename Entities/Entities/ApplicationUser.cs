﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Enums;
using Microsoft.AspNetCore.Identity;

namespace Entities.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Column("USR_CPF")]
        public string CPf { get; set; }

        [Column("USR_TIPO")]
        public TipoUsuario? Tipo { get; set; }
    }
}
