using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApiLivraria.Dominio
{
    public class LivroDbContext : DbContext
    {
        public DbSet<Livro> Livros { get; set; }

    }
}
