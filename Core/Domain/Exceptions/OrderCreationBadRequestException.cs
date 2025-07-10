using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class OrderCreationBadRequestException() : BadRequestException("Order creation failed, please try again later.")
    {
    }
}
