using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestas.Services
{
    public interface IAvoidDuplicateCrossCuttingConcerns
    {
        List<string> AppliedCrossCuttingConcerns { get; }
    }
}
