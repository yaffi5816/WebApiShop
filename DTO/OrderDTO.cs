using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public record OrderDTO
    (
        short OrderId,
        short UserId,
        DateOnly OrderDate,
        short OrderSum,
        short OrderItemId,
        string ProductName,
        string ImgUrl
    );
}
