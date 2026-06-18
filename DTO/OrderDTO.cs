using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public record OrderDTO(
     int OrderId,

     int UserId,

     DateOnly OrderDate,

     double OrderSum,

     ICollection<OrderItemDTO> OrderItems

     );
}
