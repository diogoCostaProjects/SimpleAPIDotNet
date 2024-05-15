using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleApi.Data;
using SimpleApi.Models;

namespace SimpleApi.Services
{
    public class ProdutoService
    {
        private readonly ApplicationDbContext _context;

        public ProdutoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Produto>> GetAllProdutosAsync()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Produto> GetProdutoByIdAsync(int id)
        {
            return await _context.Produtos.FindAsync(id);
        }

        public async Task<Produto> CreateProdutoAsync(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public IEnumerable<Produto> GetProdutosPrecoMaior(decimal precoMinimo)
        {
            var query = @"
                        SELECT p.id, p.nome, p.preco, c.nome AS categoria 
                        FROM produtos AS p 
                        INNER JOIN categoria AS c ON c.id = p.categoria
                        WHERE p.preco > {0}";

            var produtosComCategorias = _context.Produtos
                                   .FromSqlRaw(query, precoMinimo)
                                   .ToList();
            
            return produtosComCategorias;
        }
    }
}
