using Library.API.Entities;
using Library.API.Persistence;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.API.Controllers
{
    [Route("api/livros")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly LibraryDBContext _context;
        public LivrosController(LibraryDBContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Obter todos os Livros cadastrados.
        /// </summary>
        /// <remarks>
        /// Retorna uma lista seus livros.
        /// Exemplo de resposta:
        /// 
        /// GET /api/livros
        /// [
        ///   {
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
        /// <response code="200">Retorna uma lista de livros.</response>
        /// <response code="400">Erro ao processar a solicitação.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAll()
        {
            var livro = _context.Livros.ToList();
            return Ok(livro);
        }

        /// <summary>
        /// Obter um Livro específico pelo ID.
        /// </summary>
        /// <remarks>
        /// Retorna um livro.
        /// Exemplo de resposta:
        /// 
        /// GET /api/livros/{id}
        /// [
        ///   {
        ///     "livro": [
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
        /// <response code="200">Retorna o autor encontrado.</response>
        /// <response code="404">Autor não encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid id)
        {
            var livro = _context.Livros.SingleOrDefault(d => d.ID == id);
            if (livro == null)
            {
                return NotFound();
            }
            return Ok(livro);
        }
        /// <summary>
        /// Criar um novo Livro.
        /// </summary>
        /// <param name="livroDTO">Objeto contendo os dados do Livro a ser criado.</param>
        /// <returns>Retorna os dados do Livro criado.</returns>
        /// <remarks>
        /// Exemplo de corpo da solicitação:
        ///       {
        ///         "titulo": "chapeuzinho vermelho",
        ///         "categoria": "infantil",
        ///         "descricao": "é um livro que conta sobre uma menina que usa roupa vermelha",
        ///         "anoPublicacao": 1999
        ///       }
        /// </remarks>
        /// <response code="201">Livro criado com sucesso.</response>
        /// <response code="400">Se os dados fornecidos forem inválidos.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(LivroDTO livroDTO)
        {
            var livro = new Livro(Guid.NewGuid(), livroDTO.Titulo,livroDTO.Categoria,livroDTO.Descricao, livroDTO.AnoPublicacao);
            _context.Livros.Add(livro);
            return CreatedAtAction(nameof(GetById), new { id = livro.ID }, livro);
        }

        /// <summary>
        /// Atualizar um Livro existente.
        /// </summary>
        /// <param name="id">ID do Livro.</param>
        /// <param name="input">Objeto com os dados atualizados do Livro.</param>
        /// <response code="204">Livro atualizado com sucesso.</response>
        /// <response code="400">Erro ao processar a solicitação.</response>
        /// <response code="404">Livro não encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(Guid id, LivroDTO input)
        {
            var livro = _context.Livros.SingleOrDefault(d => d.ID == id);
            if (livro == null)
            {
                return NotFound();
            }

            livro.Update(input.Titulo, input.Categoria, input.Descricao, input.AnoPublicacao);

            return NoContent();
        }

        /// <summary>
        /// Excluir um Livro pelo ID.
        /// </summary>
        /// <param name="id">ID do Livro a ser excluído.</param>
        /// <response code="204">Livro excluído com sucesso.</response>
        /// <response code="404">Livro não encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(Guid id)
        {
            var livro = _context.Livros.SingleOrDefault(d => d.ID == id);
            if (livro == null)
            {
                return NotFound();
            }

            _context.Livros.Remove(livro);
            return NoContent();
        }


    }
}

