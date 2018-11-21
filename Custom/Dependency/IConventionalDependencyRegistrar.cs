﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestas.Dependency
{
    public interface IConventionalDependencyRegistrar
    {
        void RegisterAssembly(IConventionalRegistrationContext context);
    }
}
