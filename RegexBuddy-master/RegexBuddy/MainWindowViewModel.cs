using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using RegexBuddy.Annotations;

namespace RegexBuddy
{
    class MainWindowViewModel : IMainWindowViewModel
    {
        private IRegexService _service;
        public MainWindowViewModel(IRegexService regexService)
        {
            _service = regexService;

            //ResultHighlight = "|~S~|Abc|~E~| some more text |~S~|highlighted|~E~|";
        }

        private string _expression;
        public string Expression { get { return _expression; } set { _expression = value; OnPropertyChanged(); } }

        public string From
        {
            get { return _from; }
            set { _from = value; OnPropertyChanged(); }
        }

        public string Result
        {
            get { return _result; }
            set { _result = value; OnPropertyChanged();}
        }

        public bool HasErrors
        {
            get { return _hasErrors; }
            set { _hasErrors = value; OnPropertyChanged(); }
        }

        public ObservableCollection<MatchResult> MatchResults
        {
            get { return _matchResults ?? (MatchResults = new ObservableCollection<MatchResult>(new List<MatchResult>())); }
            set { _matchResults = value; OnPropertyChanged(); }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public string ResultHighlight
        {
            get { return _resultHighlight; }
            set { _resultHighlight = value; OnPropertyChanged();}
        }

        public HighlightIndex [] Indexes
        {
            get { return _indexes ?? (Indexes = new HighlightIndex[]{}); }
            set { _indexes = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private ICommand _clickCommand;
        private bool _canExecute = true;
        private string _from;
        private string _result;
        private ObservableCollection<MatchResult> _matchResults;
        private string _errorMessage;
        private bool _hasErrors;
        private string _description;
        private string _resultHighlight;
        private HighlightIndex [] _indexes;

        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(ConvertAction, _canExecute));
            }
        }

        private void ConvertAction()
        {
            // here we can check the value in expression
            //MatchResults.Clear();

            string errorMessage;
            MatchResults = new ObservableCollection<MatchResult>(_service.Match(Expression, From, out errorMessage));

            BuildResultHighlight();

            ErrorMessage = errorMessage;
            HasErrors = errorMessage.Length > 0;

            Indexes = MatchResults.Select(m => new HighlightIndex {Index = m.Index, Length = m.Length}).ToArray();
        }

        private void BuildResultHighlight()
        {
            var highlightString = new StringBuilder();
            highlightString.Append(From);

            foreach (var result in MatchResults.OrderByDescending(p => p.Index))
            {
                highlightString.Insert(result.Index + result.Length, "|~E~|");
                highlightString.Insert(result.Index, "|~S~|");
            }

            ResultHighlight = highlightString.ToString();

            //ResultHighlight = "|~S~|Abc|~E~| some more text |~S~|highlighted|~E~|";
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
