﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class UserNotFoundException (string email): NotFoundException ($" user with Email {email} Not found")
    {
    }
}
