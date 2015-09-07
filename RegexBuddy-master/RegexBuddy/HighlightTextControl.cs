using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace RegexBuddy
{
    public class HighlightIndex
    {
        //public static DependencyProperty IndexProperty =
        //          DependencyProperty.Register("Index", typeof(int),
        //          typeof(HighlightIndex), new UIPropertyMetadata(0));

        //public static DependencyProperty LengthProperty =
        //          DependencyProperty.Register("Length", typeof(int),
        //          typeof(HighlightIndex), new UIPropertyMetadata(0));

        public int Length
        {
            //get { return (int)GetValue(IndexProperty); }
            //set { SetValue(IndexProperty, value); }
            get; set;
        }

        public int Index
        {
            //get { return (int) GetValue(LengthProperty); }
            //set { SetValue(LengthProperty, value); }
            get; set;
        }
    }

    public class HighlightTextControl : ContentControl
    {
        static HighlightTextControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HighlightTextControl),
                new FrameworkPropertyMetadata(typeof(HighlightTextControl)));
        }

        public HighlightTextControl()
        {
            DependencyPropertyDescriptor dpd;
            dpd = DependencyPropertyDescriptor.FromProperty(
                IndexesProperty, typeof(HighlightTextControl));

            dpd.AddValueChanged(this, (sender, args) =>
            {
                UpdateContent();
            });

            DependencyPropertyDescriptor textDescriptor = DependencyPropertyDescriptor.FromProperty(
                TextProperty, typeof(HighlightTextControl));

            textDescriptor.AddValueChanged(this, (sender, args) =>
            {
                UpdateContent();
            });
        }

        private void UpdateContent()
        {
            Content = null;

            var textBlock = new TextBlock();
            textBlock.TextWrapping = TextWrapping.Wrap;
            string escapedXml = SecurityElement.Escape(Text);

            if (escapedXml == null) return;

            while (escapedXml.IndexOf("|~S~|") != -1)
            {
                //up to |~S~| is normal
                textBlock.Inlines.Add(new Run(escapedXml.Substring(0, escapedXml.IndexOf("|~S~|"))));
                //between |~S~| and |~E~| is highlighted
                textBlock.Inlines.Add(new Run(escapedXml.Substring(escapedXml.IndexOf("|~S~|") + 5,
                                          escapedXml.IndexOf("|~E~|") - (escapedXml.IndexOf("|~S~|") + 5))) { FontWeight = FontWeights.Bold, Background = Brushes.Yellow });
                //the rest of the string (after the |~E~|)
                escapedXml = escapedXml.Substring(escapedXml.IndexOf("|~E~|") + 5);
            }

            if (escapedXml.Length > 0)
            {
                textBlock.Inlines.Add(new Run(escapedXml));
            }


            Content = textBlock;
        }

        public HighlightIndex [] Indexes
        {
            get { return (HighlightIndex[])this.GetValue(IndexesProperty); }
            set { this.SetValue(IndexesProperty, value); }
        }
        public static readonly DependencyProperty IndexesProperty = DependencyProperty.Register(
          "Indexes", typeof(HighlightIndex[]), typeof(HighlightTextControl), new PropertyMetadata(null));

        public string Text
        {
            get { return (string)this.GetValue(TextProperty); }
            set { this.SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
          "Text", typeof(string), typeof(HighlightTextControl), new PropertyMetadata(null));
    }
}
