using System.ComponentModel.DataAnnotations;
using TeamManager.Domain.Enum;

namespace TeamManager.Domain.DTOs;

public record AthleteCreateDto(
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Nome deve ter entre 2 e 100 caracteres")]
        string Name,
    [Required(ErrorMessage = "Data de nascimento é obrigatória")] DateTime BirthDay,
    [Range(1.0, 3.0, ErrorMessage = "Altura deve estar entre 1.0 e 3.0 metros")] float Height,
    [Range(30, 200, ErrorMessage = "Peso deve estar entre 30 e 200 kg")] float Weight,
    [Required(ErrorMessage = "Posição é obrigatória")] Positions Position
);

public record AthleteUpdateDto(
    [Required(ErrorMessage = "ID é obrigatório")] int Id,
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Nome deve ter entre 2 e 100 caracteres")]
        string Name,
    [Required(ErrorMessage = "Data de nascimento é obrigatória")] DateTime BirthDay,
    [Range(1.0, 3.0, ErrorMessage = "Altura deve estar entre 1.0 e 3.0 metros")] float Height,
    [Range(30, 200, ErrorMessage = "Peso deve estar entre 30 e 200 kg")] float Weight,
    [Required(ErrorMessage = "Posição é obrigatória")] Positions Position
);

public record AthleteResponseDto(
    int Id,
    string Name,
    DateTime BirthDay,
    int Age,
    float Height,
    float Weight,
    Positions Position,
    string PositionName,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    bool IsActive
);
