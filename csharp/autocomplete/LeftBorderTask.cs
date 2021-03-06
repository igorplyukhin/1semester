﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Autocomplete
{
    public class LeftBorderTask
    {
        public static int GetLeftBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            if (left == right - 1)
                return left;
            var middle = left + (right - left) / 2;
            return String.Compare(phrases[middle], prefix, StringComparison.OrdinalIgnoreCase) < 0
                ? GetLeftBorderIndex(phrases, prefix, middle, right)
                : GetLeftBorderIndex(phrases, prefix, left, middle);
        }
    }
}