using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SimpleApi.Services;
using SimpleApi.Models;

namespace SimpleApi.Controllers
{
    [ApiController]
    [Route("api/produto")]
    public class ProdutoController : Controller
    {
        public ProdutoController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }
        private readonly ProdutoService _produtoService;

        [HttpGet]
        [Route("todos")]
        // public IActionResult Todos()
        // {
        //     // var produtos = new List<object>
        //     // {
        //     //     new { Id = 1, Nome = "Produto 1", Preco = 9.99 },
        //     //     new { Id = 2, Nome = "Produto 2", Preco = 14.99 },
        //     //     new { Id = 3, Nome = "Produto 3", Preco = 19.99 }
        //     // };

        //     var produtos = await _produtoService.GetAllProdutosAsync();
        //     return Ok(produtos);
        // }

        public async Task<ActionResult<List<Produto>>> GetAll()
        {
            var produtos = await _produtoService.GetAllProdutosAsync();
            return Ok(produtos);
        }
    }
}
