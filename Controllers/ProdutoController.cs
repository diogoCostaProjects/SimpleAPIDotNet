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
        public async Task<ActionResult<List<Produto>>> GetAll()
        {
            var produtos = await _produtoService.GetAllProdutosAsync();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> Detalhes(int id)
        {
            var produto = await _produtoService.GetProdutoByIdAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> Adicionar(Produto produto)
        {
            var novoProduto = await _produtoService.CreateProdutoAsync(produto);
            // return CreatedAtAction(nameof(GetAll), new { id = novoProduto.Id }, novoProduto);
            return Json(new { success = true, msg = "Produto adicionado com sucesso" });
        }

        [HttpGet("preco-maior/{preco}")]
        public async Task<ActionResult<Produto>> PrecoMaior(decimal preco)
        {
            var produto = _produtoService.GetProdutosPrecoMaior(preco);
            if (produto == null)
            {
                return NotFound();
            }
            return Ok(produto);
        }
    }
}
