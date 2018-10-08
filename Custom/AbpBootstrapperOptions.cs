﻿using Custom.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class AbpBootstrapperOptions
    {
        public bool DisableAllInterceptors { get; set; }

        public IIocManager IocManager { get; set; }

        public AbpBootstrapperOptions()
        {
            IocManager = IocManager.Instance;
        }
    }
}