using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SimpleApi.Services;
using SimpleApi.Models;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost]
        [Route("novo")]
        public async Task<ActionResult<Produto>> Adicionar(Produto produto)
        {
            var novoProduto = await _produtoService.adicionaPorFavor(produto);
            // return CreatedAtAction(nameof(GetAll), new { id = novoProduto.Id }, novoProduto);
            return Json(new { success = true, msg = "Produto adicionado com sucesso" });
        }

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

        [HttpPost("update")]
        public async Task<ActionResult<Produto>> Update(Produto in_produto)
        {
            var verificaProduto = await _produtoService.GetProdutoByIdAsync(in_produto.Id);
            if (verificaProduto == null)
            {
                throw new KeyNotFoundException("produto n�o encontrado.");
            }

            verificaProduto.Nome = in_produto.Nome;
            verificaProduto.Preco = in_produto.Preco;
            verificaProduto.Categoria = in_produto.Categoria;

            await _produtoService.atualizaAgora(verificaProduto);

            return Json(new { success = true, msg = "Produto "+@verificaProduto.Nome+" atualizado com sucesso" });
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

        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<List<Produto>>> Listar()
        {
            var produtos = await _produtoService.GetAllProdutosAsync();

            ViewBag.produtosView = produtos;

            return View();
        }

        [HttpGet]
        [Route("novo-produto")]
        public IActionResult Novo()
        {
            return View();
        }

        public ProdutoService Get_produtoService()
        {
            return _produtoService;
        }

        [HttpGet]
        [Route("editar-produto/{item_id}")]
        public async Task<ActionResult<Produto>> Editar(int item_id)
        {
            Produto item = await _produtoService.GetProdutoByIdAsync(item_id);
            ViewBag.item = item;
            
            return View();
        }

        [HttpPost]
        [Route("editar-produto")]
        public async Task<ActionResult<Produto>> EditarSave()
        {
            var verificaProduto = await _produtoService.GetProdutoByIdAsync(int.Parse(Request.Form["Id"]));
            if (verificaProduto == null)
            {
                throw new KeyNotFoundException("produto não encontrado.");
            }

            verificaProduto.Nome = Request.Form["Nome"];
            verificaProduto.Preco = decimal.Parse(Request.Form["Preco"]);
            verificaProduto.Categoria = int.Parse(Request.Form["Categoria"]);

            await _produtoService.atualizaAgora(verificaProduto);

            return View();
        }

        [HttpGet]
        [Route("novo-produto-web")]
        public IActionResult NovoWeb()
        {
            return View();
        }

        [HttpPost]
        [Route("novo-produto-web-add")]
        public async Task<ActionResult<Produto>> NovoProdutoWebAdd()
        {
            Produto produto =  new Produto();

            produto.Nome =              Request.Form["Nome"];
            produto.Preco =             decimal.Parse(Request.Form["Preco"]);
            produto.Categoria =         int.Parse(Request.Form["Categoria"]);

            await _produtoService.adicionaPorFavor(produto);

            return RedirectToAction("Todos");
        }
    }
}
