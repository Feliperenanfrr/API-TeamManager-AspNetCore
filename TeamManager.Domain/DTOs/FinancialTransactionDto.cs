using System.ComponentModel.DataAnnotations;

namespace TeamManager.Domain.DTOs;

public record FinancialTransactionCreateDto(
    [Required(ErrorMessage = "A data da transação é obrigatória")] DateTime Date,
    
    [Required(ErrorMessage = "O valor da transação é obrigatório")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
    double Amount,
    
    [Required(ErrorMessage = "O tipo de transação é obrigatório")] bool TypeTransaction,
    
    [Required(ErrorMessage = "O motivo da transação é obrigatório")]
    [StringLength(500, MinimumLength = 3, ErrorMessage = "O motivo deve ter entre 3 e 500 caracteres")]
    string Reason
);

public record FinancialTransactionUpdateDto(
    [Required(ErrorMessage = "O ID da transação é obrigatório")] int Id,

    [Required(ErrorMessage = "A data da transação é obrigatória")] DateTime Date,

    [Required(ErrorMessage = "O valor da transação é obrigatório")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
    double Amount,

    [Required(ErrorMessage = "O tipo de transação é obrigatório")] bool TypeTransaction,

    [Required(ErrorMessage = "O motivo da transação é obrigatório")]
    [StringLength(500, MinimumLength = 3, ErrorMessage = "O motivo deve ter entre 3 e 500 caracteres")]
    string Reason
);

public record FinancialTransactionResponseDto(
    int Id,
    DateTime Date,
    double Amount,
    bool TypeTransaction,
    string Reason,
    DateTime CreatedAt
)
{
    public string TypeDescription => TypeTransaction ? "Entrada" : "Saída";
}