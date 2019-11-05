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
                if (String.Compare(phrases[m],prefix, StringComparison.OrdinalIgnoreCase) < 0)
                    left = m;
                else
                    right = m;
            }

            while (right < phrases.Count 
                   && phrases[right].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                right++;
            return right;
        }
    }
}