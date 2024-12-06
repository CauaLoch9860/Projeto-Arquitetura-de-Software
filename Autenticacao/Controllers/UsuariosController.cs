using Microsoft.AspNetCore.Mvc;
using Autenticacao.Data; // Para ApplicationDbContext e Usuario do banco
using Autenticacao.Models; // Para Usuario do modelo recebido no request
using System.Linq;

namespace Autenticacao.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public IActionResult GetUsuarios()
        {
            var usuarios = _context.Usuarios.ToList();
            if (!usuarios.Any())
            {
                return NotFound("Nenhum usuário cadastrado.");
            }

            return Ok(usuarios);
        }

        // GET: api/Usuarios/{id}
        [HttpGet("{id}")]
        public IActionResult GetUsuario(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
            {
                return NotFound($"Usuário com ID {id} não encontrado.");
            }

            return Ok(usuario);
        }

        // POST: api/Usuarios
        [HttpPost]
        public IActionResult PostUsuario([FromBody] Autenticacao.Models.Usuario modeloUsuario)
        {
            if (modeloUsuario == null)
            {
                return BadRequest("Usuário inválido.");
            }

            // Converte o modelo para a entidade do banco
            var usuarioEntidade = new Autenticacao.Data.Usuario
            {
                Nome = modeloUsuario.Nome,
                Email = modeloUsuario.Email,
                Senha = modeloUsuario.Senha
            };

            _context.Usuarios.Add(usuarioEntidade);
            _context.SaveChanges();

            return Ok("Usuário cadastrado com sucesso!");
        }

        // PUT: api/Usuarios/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUsuario(int id, [FromBody] Autenticacao.Models.Usuario modeloUsuario)
        {
            var usuarioEntidade = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuarioEntidade == null)
            {
                return NotFound($"Usuário com ID {id} não encontrado.");
            }

            // Atualiza os dados do usuário com base no modelo recebido
            usuarioEntidade.Nome = modeloUsuario.Nome;
            usuarioEntidade.Email = modeloUsuario.Email;
            usuarioEntidade.Senha = modeloUsuario.Senha;

            _context.SaveChanges();

            return Ok("Usuário atualizado com sucesso!");
        }

        // DELETE: api/Usuarios/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario(int id)
        {
            var usuarioEntidade = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuarioEntidade == null)
            {
                return NotFound($"Usuário com ID {id} não encontrado.");
            }

            _context.Usuarios.Remove(usuarioEntidade);
            _context.SaveChanges();

            return Ok("Usuário excluído com sucesso!");
        }
    }
}
