using Custom.Services;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom.Aspects
{
    internal static class ApplicationCrossCuttingConcerns
    {
        public const string Auditing = "AppAuditing";

        public static bool IsApplied([NotNull] object obj, [NotNull] string concern)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (concern == null)
            {
                throw new ArgumentNullException(nameof(concern));
            }

            return (obj as IAvoidDuplicateCrossCuttingConcerns)?.AppliedCrossCuttingConcerns.Contains(concern) ?? false;
        }
    }
}
