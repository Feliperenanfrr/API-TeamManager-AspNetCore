using System.ComponentModel.DataAnnotations;
using TeamManager.Domain.Enum;

namespace TeamManager.Domain.DTOs;

public record TrainCreateDto(
    [Required(ErrorMessage = "Data do treino é obrigatória")] DateTime Date,
    [Required(ErrorMessage = "Tipo de treino é obrigatório")] TypeWorkout TypeWorkou,
    [Required(ErrorMessage = "Quantidade de atletas é obrigatória")]
    [Range(1, 100, ErrorMessage = "Quantidade de atletas deve estar entre 1 e 100")]
        int QuantityAthletes
);

public record TrainUpdateDto(
    [Required(ErrorMessage = "ID é obrigatório")] int Id,
    [Required(ErrorMessage = "Data do treino é obrigatória")] DateTime Date,
    [Required(ErrorMessage = "Tipo de treino é obrigatório")] TypeWorkout TypeWorkou,
    [Required(ErrorMessage = "Quantidade de atletas é obrigatória")]
    [Range(1, 100, ErrorMessage = "Quantidade de atletas deve estar entre 1 e 100")]
        int QuantityAthletes
);

public record TrainResponseDto(
    int Id,
    DateTime Date,
    TypeWorkout TypeWorkou,
    string TypeTrainName,
    int QuantityAthletes,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    bool IsActive
);
