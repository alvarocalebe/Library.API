using Library.API.Entities;
using Library.API.Persistence;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.API.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        private readonly LibraryDBContext _context;
        public UsuariosController(LibraryDBContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Obter todos os Usuarios cadastrados com sua lista de reservas.
        /// </summary>
        /// <remarks>
        /// Retorna uma lista de Usuarios com seus livros reservados.
        /// Exemplo de resposta:
        /// 
        /// GET /api/usuarios
        ///        [
        ///         {
        ///   "id": "00000000-0000-0000-0000-000000000000",
        ///    "userName": "alvarocalebe",
        ///   "email": "string",
        ///    "reservas": [
        ///         {
        ///       "id": "4f7c0745-8789-4994-b368-3a76569f6040",
        ///      "livro": {
        ///       "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///       "titulo": "chapeuzinho vermelho",
        ///      "categoria": "infantil",
        ///       "descricao": "vermelho",
        ///      "anoPublicacao": 1999
        ///     },
        ///     "dataEmprestimo": "2024-12-06T08:24:43.4750583-03:00",
        ///       "dataDevolucao": "2024-12-13T08:24:43.4750583-03:00"
        ///     }
        ///   ]
        /// }
        ///]
        /// </remarks>
        /// <response code="200">Retorna uma lista de autores cadastrados com seus livros.</response>
        /// <response code="400">Erro ao processar a solicitação.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAll()
        {
            var usuario = _context.Usuarios.ToList();
            return Ok(usuario);
        }

        /// <summary>
        /// Obter um Usuario específico pelo ID.
        /// </summary>
        /// <remarks>
        /// Retorna um Usuario com sua lista de livros.
        /// Exemplo de resposta:
        /// 
        /// GET /api/usuarios/{id}
        /// [
        ///   {
        ///     "nome": "alvaro",
        ///     "nacionalidade": "brasileiro",
        ///     "idade": 21,
        ///     "livros": [
        ///       {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "titulo": "chapeuzinho vermelho",
        ///         "categoria": "infantil",
        ///         "descricao": "é um livro que conta sobre uma menina que usa roupa vermelha",
        ///         "anoPublicacao": 1999
        ///       }
        ///     ]
        ///   }
        /// ]
        /// </remarks>
        /// <response code="200">Retorna o Usuario encontrado.</response>
        /// <response code="404">Usuario não encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid id)
        {
            var usuario = _context.Usuarios.SingleOrDefault(d => d.ID == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }


        /// <summary>
        /// Criar um novo Usuario.
        /// </summary>
        /// <param name="usuarioDTO">Objeto contendo os dados do Usuario a ser criado.</param>
        /// <returns>Retorna os dados do Livro criado.</returns>
        /// <remarks>
        /// Exemplo de corpo da solicitação:
        ///       {
        ///    "userName": "alvarocalebe",
        ///   "email": "string",
        ///       }
        /// </remarks>
        /// <response code="201">Livro criado com sucesso.</response>
        /// <response code="400">Se os dados fornecidos forem inválidos.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(UsuarioDTO usuarioDTO)
        {
            var usuario = new Usuario(Guid.NewGuid(), usuarioDTO.UserName, usuarioDTO.Email);


            _context.Usuarios.Add(usuario);
            return CreatedAtAction(nameof(GetById), new { id = usuario.ID }, usuario);
        }

        /// <summary>
        /// Atualizar um Usuario existente.
        /// </summary>
        /// <param name="id">ID do Usuario.</param>
        /// <param name="input">Objeto com os dados atualizados do Usuario.</param>
        /// <response code="204">Usuario atualizado com sucesso.</response>
        /// <response code="400">Erro ao processar a solicitação.</response>
        /// <response code="404">Usuario não encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(Guid id, UsuarioDTO input)
        {
            var usuario = _context.Usuarios.SingleOrDefault(d => d.ID == id);
            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Update(input.UserName, input.Email);

            return NoContent();
        }


        /// <summary>
        /// Excluir um Usuario pelo ID.
        /// </summary>
        /// <param name="id">ID do Usuario a ser excluído.</param>
        /// <response code="204">Usuario excluído com sucesso.</response>
        /// <response code="404">Usuario não encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(Guid id)
        {
            var usuario = _context.Usuarios.SingleOrDefault(d => d.ID == id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            return NoContent();
        }

        /// <summary>
        /// Adiciona um livro à lista de reserva do Usuario.
        /// </summary>
        /// <param name="id">ID do Usuario que vai ser reservado.</param>
        /// <remarks>
        /// Exemplo de corpo de solicitação:
        /// {
        ///   "LivroID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        /// }
        /// </remarks>
        /// <response code="204">Reserva adicionada com sucesso.</response>
        [HttpPost("{id}/reservas")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult PostReservas(Guid id, ReservaDTO reservaDTO)
        {
            var livro = _context.Livros.SingleOrDefault(l => l.ID == reservaDTO.LivroID);
            
            var reserva = new Reserva(Guid.NewGuid(), livro);

            var usuario = _context.Usuarios.SingleOrDefault(d => d.ID == id);
            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Reservas.Add(reserva);
            return NoContent();
        }
    }
}
