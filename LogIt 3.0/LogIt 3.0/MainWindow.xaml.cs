using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace LogIt3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string SessionIdPrefix = "Session ID: ";
        private readonly TemplateProvider _templateProvider;

        public MainWindow()
        {
            InitializeComponent();
            _templateProvider = new TemplateProvider();

            // Register paste handlers to preserve plain text formatting
            DataObject.AddPastingHandler(Log1Box, TextBoxPasting);
            DataObject.AddPastingHandler(Log2Box, TextBoxPasting);
        }

        /// <summary>
        /// Custom paste handler that forces plain text pasting
        /// </summary>
        private void TextBoxPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (sender is RichTextBox richTextBox)
            {
                string pasteText = e.DataObject.GetData(DataFormats.Text) as string;
                if (!string.IsNullOrEmpty(pasteText))
                {
                    richTextBox.Document.ContentEnd.InsertTextInRun(pasteText);
                    e.CancelCommand();
                }
            }
        }

        /// <summary>
        /// Gets the currently active RichTextBox based on selected tab
        /// </summary>
        private RichTextBox GetActiveLogBox()
        {
            return Tab1.IsSelected ? Log1Box : Log2Box;
        }

        /// <summary>
        /// Inserts text into the currently active log at the cursor position
        /// </summary>
        private void InsertTextToActiveLog(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            var logBox = GetActiveLogBox();

            logBox.BeginChange();

            // Clear selection if any
            if (!string.IsNullOrEmpty(logBox.Selection.Text))
            {
                logBox.Selection.Text = string.Empty;
            }

            // Insert text at current cursor position
            TextPointer insertPosition = logBox.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
            logBox.CaretPosition.InsertTextInRun(text);
            logBox.CaretPosition = insertPosition;

            logBox.EndChange();
            Keyboard.Focus(logBox);
        }

        /// <summary>
        /// Shows an input dialog and returns the user's response
        /// </summary>
        private string GetUserInput(string prompt, string title, string defaultValue = "Answer Goes Here")
        {
            return InputDialogWindow.Show(prompt, title, defaultValue);
        }

        // ===== Button Click Handlers =====

        private void VA_Click(object sender, RoutedEventArgs e)
        {
            string template = _templateProvider.GetTemplate("VA");
            InsertTextToActiveLog(template);
        }

        private void POS_Click(object sender, RoutedEventArgs e)
        {
            string userInput = GetUserInput("What parts are you sending?", "Parts Only Dispatch");

            if (!string.IsNullOrEmpty(userInput))
            {
                string template = _templateProvider.GetTemplate("POS");
                InsertTextToActiveLog(template + userInput + "\"");
            }
        }

        private void OST_Click(object sender, RoutedEventArgs e)
        {
            string userInput = GetUserInput("What parts are you sending?", "On Site Service Dispatch");

            if (!string.IsNullOrEmpty(userInput))
            {
                string template = _templateProvider.GetTemplate("OST");
                InsertTextToActiveLog(template + userInput + "\" via On Site Tech");
            }
        }

        private void HDD_Click(object sender, RoutedEventArgs e)
        {
            string userInput = GetUserInput("What did the customer say about software?", "HDD Questions");

            if (string.IsNullOrEmpty(userInput))
            {
                return;
            }

            // Determine which template to use based on checkbox state
            string templateKey;
            if (KYHD.IsChecked == true)
            {
                templateKey = "KYHD";
            }
            else if (L1Y.IsChecked == true)
            {
                templateKey = "L1Y";
            }
            else if (HDDOST.IsChecked == true)
            {
                templateKey = "HDDOST";
            }
            else
            {
                templateKey = "HDD";
            }

            string template = _templateProvider.GetTemplate(templateKey);
            InsertTextToActiveLog(template + userInput + "\"");
        }

        private void Plastics_Click(object sender, RoutedEventArgs e)
        {
            string userInput = GetUserInput("What did the customer say about plastics?", "Plastics Check");

            if (!string.IsNullOrEmpty(userInput))
            {
                string template = _templateProvider.GetTemplate("Plastics");
                InsertTextToActiveLog(template + userInput + "\"");
            }
        }

        private void FRU_Click(object sender, RoutedEventArgs e)
        {
            string template = _templateProvider.GetTemplate("FRU");
            InsertTextToActiveLog(template);
        }

        private void CIDAR_Click(object sender, RoutedEventArgs e)
        {
            string template = _templateProvider.GetTemplate("CIDAR");
            InsertTextToActiveLog(template);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            // Reset all checkboxes
            L1Y.IsChecked = false;
            HDDOST.IsChecked = false;
            KYHD.IsChecked = false;

            // Clear the active log and reset to default text
            var logBox = GetActiveLogBox();
            logBox.Document.Blocks.Clear();
            logBox.AppendText(SessionIdPrefix);
        }

        // ===== Checkbox Event Handlers (Mutual Exclusion) =====

        private void KYHD_Checked(object sender, RoutedEventArgs e)
        {
            L1Y.IsEnabled = false;
            HDDOST.IsEnabled = false;
        }

        private void KYHD_UnChecked(object sender, RoutedEventArgs e)
        {
            L1Y.IsEnabled = true;
            HDDOST.IsEnabled = true;
        }

        private void L1Y_Checked(object sender, RoutedEventArgs e)
        {
            KYHD.IsEnabled = false;
            HDDOST.IsEnabled = false;
        }

        private void L1Y_UnChecked(object sender, RoutedEventArgs e)
        {
            KYHD.IsEnabled = true;
            HDDOST.IsEnabled = true;
        }

        private void HDDOST_Checked(object sender, RoutedEventArgs e)
        {
            KYHD.IsEnabled = false;
            L1Y.IsEnabled = false;
        }

        private void HDDOST_UnChecked(object sender, RoutedEventArgs e)
        {
            KYHD.IsEnabled = true;
            L1Y.IsEnabled = true;
        }
    }
}
