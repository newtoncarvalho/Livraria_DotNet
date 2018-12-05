using System;
namespace WebApiLivraria
{

    /*
     *  ISBN, Autor, Nome, Preço, Data Publicação, Imagem da Capa
     */
    public class Livro
    {
        public Livro()
        {
        }

        public String ISBN { get; set; }
        public String Autor { get; set; }
        public String Nome { get; set; }
        public Double Preco { get; set; }
        public DateTime DataPublicacao { get; set; }
        public byte[] Imagem { get; set;  }

    }
}
