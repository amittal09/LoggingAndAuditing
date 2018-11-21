using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Vestas
{
    public class DisposeAction : IDisposable
    {
        public static readonly DisposeAction Empty = new DisposeAction(null);

        private Action _action;

       
        public DisposeAction([CanBeNull] Action action)
        {
            _action = action;
        }

        public void Dispose()
        {
            var action = Interlocked.Exchange(ref _action, null);
            action?.Invoke();
        }
    }
}
