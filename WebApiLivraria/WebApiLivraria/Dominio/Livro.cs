using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiLivraria.Dominio
{

    /*
     *  ISBN, Autor, Nome, Preço, Data Publicação, Imagem da Capa
     */
    public class Livro
    {
        public Livro()
        {
        }

        [Key]
        [Required(AllowEmptyStrings=false, ErrorMessage ="ISBN parametro obrigatorio")]
        [StringLength(13)]
        public String ISBN { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Autor parametro obrigatorio")]
        public String Autor { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Nome parametro obrigatorio")]
        public String Nome { get; set; }

        public Double Preco { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Data de Publicacao parametro obrigatorio")]
        public DateTime DataPublicacao { get; set; }

        public byte[] Imagem { get; set;  }
    }
}
