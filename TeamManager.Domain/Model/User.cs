using TeamManager.Domain.Common;

namespace TeamManager.Domain.Model;

public class User : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = "Sem Nome";
    public string Email { get; set; }
    public string Password { get; set; }
}
