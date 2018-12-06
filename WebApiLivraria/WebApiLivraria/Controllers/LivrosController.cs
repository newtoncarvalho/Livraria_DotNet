using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            // Analisa o objeto informado
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            String key = livro.ISBN;

            Livro livroPersistido = this.livroDdContext.Livros.FirstOrDefault(l => l.ISBN == key);
            if (livroPersistido != null)
                return BadRequest("Ja existe um livro cadastrado para o ISBN informado");

            this.livroDdContext.Livros.Add(livro);
            this.livroDdContext.SaveChangesAsync();

            /**
             * O primeiro parametro corresponde a uma URI de retorno
             * Ja o segundo e o Bean propriamente dito
             */
            return Created($"?key={livro.ISBN}", livro);            
        }

        [HttpPut]
        public IActionResult Put([FromBody]Livro livro)
        {
            // Analisa o objeto informado
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            String key = livro.ISBN;

            Livro livroPersistido = this.livroDdContext.Livros.FirstOrDefault(l => l.ISBN == key);
            if (livroPersistido == null)
                return NotFound("Nao existe livro cadastrado para o ISBN informado [" + key + "]");

            /**
             * A entidade ja existe no Banco, Portanto ja estah (tracked) "amarrado" no
             * contexto de persistencia
             */
            this.livroDdContext.Entry(livro).State = EntityState.Modified;
            this.livroDdContext.SaveChangesAsync();

            return Updated(livro);
        }

        public IActionResult Delete([FromODataUri] String key)
        {
            var livro = this.livroDdContext.Livros.FindAsync(key);
            if (livro == null)
            {
                return NotFound("Livro nao encontrado para o ISBN inforamado");
            }


            this.livroDdContext.Remove(livro);
            this.livroDdContext.SaveChangesAsync();
            return StatusCode((int)System.Net.HttpStatusCode.NoContent);
        }
    }
}
