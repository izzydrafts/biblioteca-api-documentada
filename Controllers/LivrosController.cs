using BibliotecaApi.Data;
using BibliotecaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Controllers;

/// <summary>
/// Gerencia os livros do acervo da biblioteca.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Tags("Livros")]
public class LivrosController : ControllerBase
{
    private readonly BibliotecaRepository _repo;

    public LivrosController(BibliotecaRepository repo) => _repo = repo;

    /// <summary>
    /// Lista todos os livros com paginação.
    /// </summary>
    /// <param name="pagina">Número da página (padrão: 1).</param>
    /// <param name="tamanhoPagina">Quantidade de itens por página (máximo: 50).</param>
    /// <returns>Lista paginada de livros.</returns>
    /// <response code="200">Retorna a lista de livros paginada.</response>
    [HttpGet]
    [ProducesResponseType(typeof(RespostaPaginada<Livro>), StatusCodes.Status200OK)]
    public IActionResult ObterTodos(
        [FromQuery] int pagina = 1,
        [FromQuery] int tamanhoPagina = 10)
    {
        tamanhoPagina = Math.Min(tamanhoPagina, 50);

        var todos = _repo.ObterLivros();

        var paginados = todos
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToList();

        var resposta = new RespostaPaginada<Livro>
        {
            Itens = paginados,
            Pagina = pagina,
            TamanhoPagina = tamanhoPagina,
            TotalItens = todos.Count
        };

        return Ok(resposta);
    }

    /// <summary>
    /// Obtém um livro pelo ID.
    /// </summary>
    /// <param name="id">Identificador do livro.</param>
    /// <returns>Livro encontrado.</returns>
    /// <response code="200">Livro encontrado.</response>
    /// <response code="404">Livro não encontrado.</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Livro), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult ObterPorId(int id)
    {
        var livro = _repo.ObterLivroPorId(id);

        if (livro == null)
            return NotFound(new { mensagem = $"Livro com ID {id} não encontrado." });

        return Ok(livro);
    }

    /// <summary>
    /// Busca livros com base em filtros opcionais.
    /// </summary>
    /// <param name="titulo">Título do livro (opcional).</param>
    /// <param name="genero">Gênero literário (opcional).</param>
    /// <param name="autorId">ID do autor (opcional).</param>
    /// <returns>Lista de livros que atendem aos critérios.</returns>
    /// <response code="200">Lista de livros filtrados.</response>
    [HttpGet("buscar")]
    [ProducesResponseType(typeof(List<Livro>), StatusCodes.Status200OK)]
    public IActionResult Buscar(
        [FromQuery] string? titulo,
        [FromQuery] string? genero,
        [FromQuery] int? autorId)
    {
        var resultados = _repo.BuscarLivros(titulo, genero, autorId);
        return Ok(resultados);
    }

    /// <summary>
    /// Cadastra um novo livro.
    /// </summary>
    /// <param name="livro">Dados do livro a ser cadastrado.</param>
    /// <returns>Livro criado.</returns>
    /// <response code="201">Livro criado com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    [HttpPost]
    [ProducesResponseType(typeof(Livro), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Criar([FromBody] Livro livro)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var criado = _repo.CriarLivro(livro);
        return CreatedAtAction(nameof(ObterPorId), new { id = criado.Id }, criado);
    }

    /// <summary>
    /// Atualiza os dados de um livro existente.
    /// </summary>
    /// <param name="id">ID do livro.</param>
    /// <param name="livro">Novos dados do livro.</param>
    /// <returns>Sem conteúdo em caso de sucesso.</returns>
    /// <response code="204">Livro atualizado com sucesso.</response>
    /// <response code="404">Livro não encontrado.</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Atualizar(int id, [FromBody] Livro livro)
    {
        if (!_repo.AtualizarLivro(id, livro))
            return NotFound(new { mensagem = $"Livro com ID {id} não encontrado." });

        return NoContent();
    }

    /// <summary>
    /// Remove um livro do sistema.
    /// </summary>
    /// <param name="id">ID do livro a ser removido.</param>
    /// <returns>Sem conteúdo em caso de sucesso.</returns>
    /// <response code="204">Livro removido com sucesso.</response>
    /// <response code="404">Livro não encontrado.</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Remover(int id)
    {
        if (!_repo.RemoverLivro(id))
            return NotFound(new { mensagem = $"Livro com ID {id} não encontrado." });

        return NoContent();
    }
}