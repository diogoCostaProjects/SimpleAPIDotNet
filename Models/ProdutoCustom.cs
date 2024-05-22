using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleApi.Models
{

    public class ProdutoCustom
    {
        public int Id { get; set; } // Propriedade para o identificador único do produto
        public string Nome { get; set; } // Propriedade para o nome do produto
        public decimal Preco { get; set; } // Propriedade para o preço do produto
        public int Categoria { get; set; }
        public string NomeCategoria { get; set; }

        // Se você estiver usando um banco de dados, geralmente você terá um campo para a chave primária (Id),
        // e outras propriedades para armazenar informações sobre o produto, como nome, preço, descrição, etc.

        // Você também pode adicionar validações, métodos auxiliares ou quaisquer outras funcionalidades necessárias ao modelo.
    }
}
