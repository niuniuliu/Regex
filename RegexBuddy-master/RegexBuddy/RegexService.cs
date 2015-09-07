using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegexBuddy
{
    public class RegexService : IRegexService
    {
        public IList<MatchResult> Match(string expression, string what, out string errorMessage)
        {
            var results = new List<MatchResult>();
            Regex regex;

            try
            {
                regex = new Regex(expression);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return results;
            }

            var matches = regex.Matches(what ?? "");

            results.AddRange(matches.Cast<Match>().Where(match => match.Length > 0).Select(m => new MatchResult {Index = m.Index, Length = m.Length, Value = m.Value}));

            errorMessage = "";
            return results;
        }

    }
}
