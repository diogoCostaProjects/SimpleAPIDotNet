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
    }
}
