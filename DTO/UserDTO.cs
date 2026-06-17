using DTO;

namespace DTO
{
    public record UserDTO(
     int UserId,

     string UserName,

     string FirstName,

     string LastName,

     bool IsAdmin,

     ICollection<OrderDTO> Orders
    );
}
