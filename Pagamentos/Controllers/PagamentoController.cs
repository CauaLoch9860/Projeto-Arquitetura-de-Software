using Microsoft.AspNetCore.Mvc;
using Pagamentos.Data;
using Pagamentos.Models; // Certifique-se de que o modelo "Pagamento" está no namespace correto

namespace Pagamentos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        private readonly PagamentoDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public PagamentoController(PagamentoDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        // Método POST para criar um pagamento
        [HttpPost]
        public async Task<IActionResult> PostPagamento([FromBody] Pagamento pagamento)
        {
            if (pagamento == null)
            {
                return BadRequest("Dados do pagamento não fornecidos.");
            }

            // Verificar se o UsuárioId existe no microserviço de Autenticação
            var client = _httpClientFactory.CreateClient("AutenticacaoAPI");
            var response = await client.GetAsync($"/api/Usuarios/{pagamento.UsuarioId}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound($"Usuário com ID {pagamento.UsuarioId} não encontrado.");
            }

            // Adicionar o pagamento à base de dados
            _context.Pagamentos.Add(pagamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPagamento), new { id = pagamento.Id }, pagamento);
        }

        // Método GET para retornar os pagamentos
        [HttpGet("{id}")]
        public IActionResult GetPagamento(int id)
        {
            var pagamento = _context.Pagamentos.FirstOrDefault(p => p.Id == id);
            if (pagamento == null)
            {
                return NotFound($"Pagamento com ID {id} não encontrado.");
            }

            return Ok(pagamento);
        }

        // Método GET para listar todos os pagamentos
        [HttpGet]
        public IActionResult GetPagamentos()
        {
            var pagamentos = _context.Pagamentos.ToList();
            return Ok(pagamentos);
        }
        
                // PUT: api/Pagamentos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPagamento(int id, [FromBody] Pagamento pagamentoAtualizado)
        {
            if (pagamentoAtualizado == null)
            {
                return BadRequest("Dados do pagamento não fornecidos.");
            }

            var pagamentoExistente = await _context.Pagamentos.FindAsync(id);
            if (pagamentoExistente == null)
            {
                return NotFound($"Pagamento com ID {id} não encontrado.");
            }

            // Atualizar as propriedades do pagamento
            pagamentoExistente.UsuarioId = pagamentoAtualizado.UsuarioId;
            pagamentoExistente.Valor = pagamentoAtualizado.Valor;
            pagamentoExistente.DataPagamento = pagamentoAtualizado.DataPagamento;

            await _context.SaveChangesAsync();

            return NoContent(); // Sucesso sem conteúdo a retornar
        }

                // DELETE: api/Pagamentos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePagamento(int id)
        {
            var pagamento = await _context.Pagamentos.FindAsync(id);
            if (pagamento == null)
            {
                return NotFound($"Pagamento com ID {id} não encontrado.");
            }

            _context.Pagamentos.Remove(pagamento);
            await _context.SaveChangesAsync();

            return Ok("Pagamento excluído com sucesso!");
        }

        
    }
}
