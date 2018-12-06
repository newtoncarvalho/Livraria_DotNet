using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using WebApiLivraria.Dominio;

namespace WebApiLivraria.Controllers
{
    [EnableQuery(PageSize =20)]
    public class LivrosController : ODataController
    {
        private readonly LivroDbContext livroDdContext;

        public LivrosController(LivroDbContext dbContext)
        {
            this.livroDdContext = dbContext;
        }

        [HttpGet]
        public IQueryable<Livro> Get()
        {
            IQueryable<Livro> query = livroDdContext.Livros.AsQueryable();
            return query;
        }

        [HttpPost]
        public IActionResult Post([FromBody]Livro livro)
        {
            String key = livro.ISBN;

            if (key == null || key.Length == 0)
                return BadRequest("ISBN nao informado");

            Livro livroPersistido = this.livroDdContext.Livros.FirstOrDefault(l => l.ISBN == key);
            if (livroPersistido != null)
                return BadRequest("Ja existe um livro cadastrado para o ISBN informado");

            this.livroDdContext.Livros.Add(livro);
            this.livroDdContext.SaveChangesAsync();

            return Created(livro);
        }
    }
}
