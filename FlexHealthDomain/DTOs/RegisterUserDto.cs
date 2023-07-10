﻿using FlexHealthDomain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHealthDomain.DTOs
{
    public class RegisterUserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Nome { get; set; }

        public string Genero { get; set; }

        public DateTime Nascimento { get; set; }

        public string Rg { get; set; }

        public string Cpf { get; set; }

        public string PhoneNumber { get; set; }
        public string FotoPerfil { get; set; }

        public string Endereco { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public string Cnpj { get; set; }
        public string Crm { get; set; }
        public string Tipo { get; set; }
        public string Especialidade { get; set; }
        public string EstabelecimentoId { get; set; }

    }
}
