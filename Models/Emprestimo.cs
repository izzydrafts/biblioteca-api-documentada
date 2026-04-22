using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Models;

/// <summary>
/// Representa o empréstimo de um livro para um usuário.
/// </summary>
public class Emprestimo
{
    /// <summary>
    /// Identificador único do empréstimo.
    /// </summary>
    /// <example>1</example>
    public int Id { get; set; }

    /// <summary>
    /// Identificador do livro que está sendo emprestado.
    /// </summary>
    /// <example>10</example>
    [Required]
    public int LivroId { get; set; }

    /// <summary>
    /// Nome do usuário que realizou o empréstimo.
    /// </summary>
    /// <example>João Silva</example>
    [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
    [StringLength(200)]
    public string NomeUsuario { get; set; } = string.Empty;

    /// <summary>
    /// Data em que o empréstimo foi realizado.
    /// </summary>
    /// <example>2026-04-22T10:00:00Z</example>
    public DateTime DataEmprestimo { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Data prevista para devolução do livro (14 dias após o empréstimo).
    /// </summary>
    /// <example>2026-05-06T10:00:00Z</example>
    public DateTime DataDevolucaoPrevista { get; set; } = DateTime.UtcNow.AddDays(14);

    /// <summary>
    /// Data em que o livro foi realmente devolvido.
    /// </summary>
    /// <example>2026-05-05T15:30:00Z</example>
    public DateTime? DataDevolucaoEfetiva { get; set; }

    /// <summary>
    /// Situação atual do empréstimo.
    /// </summary>
    /// <example>Ativo</example>
    public StatusEmprestimo Status { get; set; } = StatusEmprestimo.Ativo;
}

/// <summary>
/// Define os possíveis estados de um empréstimo.
/// </summary>
public enum StatusEmprestimo
{
    /// <summary>
    /// Empréstimo em andamento.
    /// </summary>
    Ativo,

    /// <summary>
    /// Livro já foi devolvido.
    /// </summary>
    Devolvido,

    /// <summary>
    /// Prazo de devolução foi ultrapassado.
    /// </summary>
    Atrasado
}