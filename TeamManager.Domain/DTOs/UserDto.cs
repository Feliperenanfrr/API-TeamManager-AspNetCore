using System.ComponentModel.DataAnnotations;

namespace TeamManager.Domain.DTOs;

public record UserCreateDto(
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Nome deve ter entre 2 e 100 caracteres")]
        string Name,
    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email deve ter um formato válido")]
    [StringLength(256, ErrorMessage = "Email deve ter no máximo 256 caracteres")]
        string Email,
    [Required(ErrorMessage = "Senha é obrigatória")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Senha deve ter entre 6 e 100 caracteres")]
        string Password
);

public record UserUpdateDto(
    [Required(ErrorMessage = "ID é obrigatório")] int Id,
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Nome deve ter entre 2 e 100 caracteres")]
        string Name,
    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email deve ter um formato válido")]
    [StringLength(256, ErrorMessage = "Email deve ter no máximo 256 caracteres")]
        string Email
);

public record UserChangePasswordDto(
    [Required(ErrorMessage = "ID é obrigatório")] int Id,
    [Required(ErrorMessage = "Senha atual é obrigatória")] string CurrentPassword,
    [Required(ErrorMessage = "Nova senha é obrigatória")]
    [StringLength(
        100,
        MinimumLength = 6,
        ErrorMessage = "Nova senha deve ter entre 6 e 100 caracteres"
    )]
        string NewPassword,
    [Required(ErrorMessage = "Confirmação da nova senha é obrigatória")]
    [property: Compare("NewPassword", ErrorMessage = "Nova senha e confirmação devem ser iguais")]
        string ConfirmNewPassword
);

public record UserResponseDto(
    int Id,
    string Name,
    string Email,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    bool IsActive
);

public record UserLoginDto(
    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email deve ter um formato válido")]
        string Email,
    [Required(ErrorMessage = "Senha é obrigatória")] string Password
);
