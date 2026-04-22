namespace BibliotecaApi.Models;

/// <summary>
/// Representa uma resposta paginada para listas de dados.
/// </summary>
/// <typeparam name="T">Tipo dos itens retornados na lista.</typeparam>
public class RespostaPaginada<T>
{
    /// <summary>
    /// Lista de itens retornados na página atual.
    /// </summary>
    public List<T> Itens { get; set; } = new();

    /// <summary>
    /// Número da página atual.
    /// </summary>
    /// <example>1</example>
    public int Pagina { get; set; }

    /// <summary>
    /// Quantidade de itens por página.
    /// </summary>
    /// <example>10</example>
    public int TamanhoPagina { get; set; }

    /// <summary>
    /// Total de itens disponíveis no sistema.
    /// </summary>
    /// <example>100</example>
    public int TotalItens { get; set; }

    /// <summary>
    /// Total de páginas calculado com base no número de itens e tamanho da página.
    /// </summary>
    /// <example>10</example>
    public int TotalPaginas => (int)Math.Ceiling((double)TotalItens / TamanhoPagina);
}