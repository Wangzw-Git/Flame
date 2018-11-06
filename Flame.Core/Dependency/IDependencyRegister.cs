﻿using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame.Core.Dependency
{
    public interface IDependencyRegister
    {
        void Register(ContainerBuilder builder);
    }
}
