using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace RegexBuddy
{
    public interface IMainWindowViewModel : INotifyPropertyChanged
    {
        string Expression { get; set; }
        string From { get; set; }
        string Result { get; set; }
        bool HasErrors { get; set; }
        ObservableCollection<MatchResult> MatchResults { get; set; } 
        string ErrorMessage { get; set; }
        string ResultHighlight { get; set; }

        HighlightIndex [] Indexes { get; set; } 
    }
}
