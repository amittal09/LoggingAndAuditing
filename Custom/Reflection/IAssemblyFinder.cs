﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Vestas.Reflection
{
    public interface IAssemblyFinder
    {
        List<Assembly> GetAllAssemblies();
    }
}
