﻿using Custom.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Custom.Auditing
{
    public interface IConventionalRegistrationContext
    {
        Assembly Assembly { get; }
       
        IIocManager IocManager { get; }
       
        ConventionalRegistrationConfig Config { get; }
    }
}