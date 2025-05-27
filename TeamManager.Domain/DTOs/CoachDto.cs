using System.ComponentModel.DataAnnotations;
using TeamManager.Domain.Enum;

namespace TeamManager.Domain.DTOs;

public record CoachCreateDto(
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Nome deve ter entre 2 e 100 caracteres")]
        string Name,
    [Required(ErrorMessage = "Função é obrigatória")] CoachRole Role,
    [Required(ErrorMessage = "Data de nascimento é obrigatória")] DateTime BirthDay
);

public record CoachUpdateDto(
    [Required(ErrorMessage = "ID é obrigatório")] int Id,
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Nome deve ter entre 2 e 100 caracteres")]
        string Name,
    [Required(ErrorMessage = "Função é obrigatória")] CoachRole Role,
    [Required(ErrorMessage = "Data de nascimento é obrigatória")] DateTime BirthDay
);

public record CoachResponseDto(
    int Id,
    string Name,
    CoachRole Role,
    string RoleName,
    DateTime BirthDay,
    int Age,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    bool IsActive
);
