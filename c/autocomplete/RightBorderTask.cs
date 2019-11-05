using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete
{
    public class RightBorderTask
    {
        public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {				
            while (right != left + 1)
            {
                var m = left + (right - left) / 2;
                if (String.Compare(phrases[m],prefix, StringComparison.OrdinalIgnoreCase) < 0
                    || phrases[m].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    left = m;
                else
                    right = m;
            }

            return right;
        }
    }
}