﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DuplicatedEmailBadRequestException(string email) : BadRequestException ($"There are user allready Exists{email}")
    {
    }
}
