namespace Pagamentos.Models
{
    public class Pagamento
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }  // Relacionado ao microserviço de Autenticação
        public decimal Valor { get; set; }
        public DateTime DataPagamento { get; set; }
    }
}
