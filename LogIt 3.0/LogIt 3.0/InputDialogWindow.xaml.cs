using System;
using System.Windows;

namespace LogIt3
{
    /// <summary>
    /// A custom WPF input dialog that replaces VB InputBox
    /// </summary>
    public partial class InputDialogWindow : Window
    {
        public string ResponseText { get; private set; }

        public InputDialogWindow(string prompt, string title, string defaultValue = "")
        {
            InitializeComponent();
            Title = title;
            PromptText.Text = prompt;
            InputTextBox.Text = defaultValue;
            InputTextBox.Focus();
            InputTextBox.SelectAll();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            ResponseText = InputTextBox.Text;
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ResponseText = string.Empty;
            DialogResult = false;
            Close();
        }

        /// <summary>
        /// Static method to show dialog and get result (similar to InputBox interface)
        /// </summary>
        public static string Show(string prompt, string title, string defaultValue = "")
        {
            var dialog = new InputDialogWindow(prompt, title, defaultValue);
            return dialog.ShowDialog() == true ? dialog.ResponseText : string.Empty;
        }
    }
}
