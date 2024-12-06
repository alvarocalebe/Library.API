using Library.API.Entities;
using Library.API.Persistence;
using Microsoft.AspNetCore.Mvc;

// Para mais informações sobre como habilitar Web API para projetos vazios, visite https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.API.Controllers
{
    [Route("api/autores")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly LibraryDBContext _context;
        public AutoresController(LibraryDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obter todos os Autores cadastrados com sua lista de livros.
        /// </summary>
        /// <remarks>
        /// Retorna uma lista de autores com seus livros.
        /// Exemplo de resposta:
        /// 
        /// GET /api/autores
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
        /// <response code="200">Retorna uma lista de autores cadastrados com seus livros.</response>
        /// <response code="400">Erro ao processar a solicitação.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAll()
        {
            var autores = _context.Autores.ToList();
            return Ok(autores);
        }

        /// <summary>
        /// Obter um Autor específico pelo ID.
        /// </summary>
        /// <remarks>
        /// Retorna um Autor com sua lista de livros.
        /// Exemplo de resposta:
        /// 
        /// GET /api/autores/{id}
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
        /// <response code="200">Retorna o autor encontrado.</response>
        /// <response code="404">Autor não encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid id)
        {
            var autor = _context.Autores.SingleOrDefault(d => d.ID == id);
            if (autor == null)
            {
                return NotFound();
            }
            return Ok(autor);
        }

        /// <summary>
        /// Criar um novo Autor.
        /// </summary>
        /// <param name="autorDTO">Objeto contendo os dados do autor a ser criado.</param>
        /// <returns>Retorna os dados do autor criado.</returns>
        /// <remarks>
        /// Exemplo de corpo da solicitação:
        /// {
        ///   "nome": "João",
        ///   "sobreNome": "Silva",
        ///   "nacionalidade": "Brasileiro",
        ///   "idade": 35
        /// }
        /// </remarks>
        /// <response code="201">Autor criado com sucesso.</response>
        /// <response code="400">Se os dados fornecidos forem inválidos.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(AutorDTO autorDTO)
        {
            var autor = new Autor(Guid.NewGuid(), autorDTO.Nome, autorDTO.SobreNome, autorDTO.Nacionalidade, autorDTO.Idade);
            _context.Autores.Add(autor);
            return CreatedAtAction(nameof(GetById), new { id = autor.ID }, autor);
        }


        /// <summary>
        /// Atualizar um Autor existente.
        /// </summary>
        /// <param name="id">ID do Autor.</param>
        /// <param name="input">Objeto com os dados atualizados do autor.</param>
        /// <response code="204">Autor atualizado com sucesso.</response>
        /// <response code="400">Erro ao processar a solicitação.</response>
        /// <response code="404">Autor não encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(Guid id, AutorDTO input)
        {
            var autor = _context.Autores.SingleOrDefault(d => d.ID == id);
            if (autor == null)
            {
                return NotFound();
            }

            autor.Update(input.Nome, input.SobreNome, input.Nacionalidade, input.Idade);

            return NoContent();
        }

        /// <summary>
        /// Excluir um Autor pelo ID.
        /// </summary>
        /// <param name="id">ID do Autor a ser excluído.</param>
        /// <response code="204">Autor excluído com sucesso.</response>
        /// <response code="404">Autor não encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(Guid id)
        {
            var autor = _context.Autores.SingleOrDefault(d => d.ID == id);
            if (autor == null)
            {
                return NotFound();
            }

            _context.Autores.Remove(autor);
            return NoContent();
        }

        /// <summary>
        /// Criar um livro e adicioná-lo à lista de livros do Autor.
        /// </summary>
        /// <param name="id">ID do Autor.</param>
        /// <param name="livro">Dados do livro a ser adicionado.</param>
        /// <remarks>
        /// Exemplo de corpo de solicitação:
        /// {
        ///   "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///   "titulo": "chapeuzinho vermelho",
        ///   "categoria": "infantil",
        ///   "descricao": "é um livro que conta sobre uma menina que usa roupa vermelha",
        ///   "anoPublicacao": 1999
        /// }
        /// </remarks>
        /// <response code="204">Livro adicionado com sucesso.</response>
        [HttpPost("{id}/livros")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult PostLivro(Guid id, Livro livro)
        {
            var autor = _context.Autores.SingleOrDefault(d => d.ID == id);
            if (autor == null)
            {
                return NotFound();
            }

            _context.Livros.Add(livro);
            autor.Livros.Add(livro);

            return NoContent();
        }
    }
}
