using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public record UserDTO
    (
        short UserId,
        [EmailAddress]
        string UserName
    );
}
