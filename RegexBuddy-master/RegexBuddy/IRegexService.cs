using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegexBuddy
{
    interface IRegexService
    {
        IList<MatchResult> Match(string expression, string what, out string errorMessage);
    }
}
