using Microsoft.AspNetCore.Mvc;
using Estoque.Data;
using System.Net.Http;
using System.Threading.Tasks;

namespace Estoque.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly EstoqueDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public ProdutosController(EstoqueDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        // POST: api/Produtos
        [HttpPost]
        public async Task<IActionResult> PostProduto([FromBody] Produto novoProduto)
        {
            if (novoProduto == null)
            {
                return BadRequest("Produto inválido.");
            }

            // Verificar se o usuário existe no microsserviço de Autenticação
            var client = _httpClientFactory.CreateClient("AutenticacaoAPI");
            var response = await client.GetAsync($"/api/Usuarios/{novoProduto.UsuarioId}");

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest($"Usuário com ID {novoProduto.UsuarioId} não encontrado.");
            }

            // Associar o produto ao usuário
            _context.Produtos.Add(novoProduto);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetProduto), new { id = novoProduto.Id }, novoProduto);
        }

        // GET: api/Produtos/{id}
        [HttpGet("{id}")]
        public IActionResult GetProduto(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);
            if (produto == null)
            {
                return NotFound($"Produto com ID {id} não encontrado.");
            }

            return Ok(produto);
        }

        // PUT: api/Produtos/{id}
        [HttpPut("{id}")]
        public IActionResult PutProduto(int id, [FromBody] Produto produtoAtualizado)
        {
            var produtoExistente = _context.Produtos.FirstOrDefault(p => p.Id == id);
            if (produtoExistente == null)
            {
                return NotFound($"Produto com ID {id} não encontrado.");
            }

            produtoExistente.Nome = produtoAtualizado.Nome;
            produtoExistente.Quantidade = produtoAtualizado.Quantidade;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Produtos/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteProduto(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);
            if (produto == null)
            {
                return NotFound($"Produto com ID {id} não encontrado.");
            }

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok("Produto excluído com sucesso!");
        }
    }
}
