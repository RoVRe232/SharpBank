﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBank.Services.Interfaces
{
    public interface ILoginService
    {
        bool Authorize();
        void Signout();
    }
}
