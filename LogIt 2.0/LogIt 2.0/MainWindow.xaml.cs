using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualBasic;

namespace LogIt_2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataObject.AddPastingHandler(Log1Box, new DataObjectPastingEventHandler(TextBoxPasting));
            DataObject.AddPastingHandler(Log2Box, new DataObjectPastingEventHandler(TextBoxPasting));
        }

        private void TextBoxPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (sender is RichTextBox)
            {
                string lPastingText = e.DataObject.GetData(DataFormats.Text) as string;
                (sender as RichTextBox).Document.ContentEnd.InsertTextInRun(lPastingText);
                e.CancelCommand();
            }
        }

        private void CheckBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (CheckBox.MouseDownEvent.Equals(true))
            {
                case true:
                    if (L1Y.IsChecked == true)
                    {
                        HDDOST.IsEnabled = false;
                        KYHD.IsEnabled = false;
                    }

                    if (HDDOST.IsChecked == true)
                    {
                        L1Y.IsEnabled = false;
                        KYHD.IsEnabled = false;
                    }

                    if (KYHD.IsChecked == true)
                    {
                        L1Y.IsEnabled = false;
                        HDDOST.IsEnabled = false;
                    }
                    break;

                default:
                    L1Y.IsEnabled = true;
                    HDDOST.IsEnabled = true;
                    KYHD.IsEnabled = true;
                    break;
            }
        }

        public static string GoGetIt(string nodeTitle)
        {
            XmlDocument xdXml = new XmlDocument();

            try
            {
                xdXml.Load("Settings\\LogItWording.xml");
            }
            catch (Exception)
            {
                //Xceed.Wpf.Toolkit.MessageBox.Show("Wording File is Missing!");
                MessageBoxResult result = MessageBox.Show("Wording File Is Missing!", "Missing Files!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }

            //Make a nodelist
            XmlNodeList xnNodes = xdXml.SelectNodes("/Wording/Phrase");

            string appended = "";

            //Walk through the list
            foreach (XmlNode node in xnNodes)
            {
                var nodeToQuery = node[nodeTitle].InnerText;
                if (nodeToQuery != null)
                    appended += nodeToQuery;
            }
            return appended;
        }


        private void VA_Click(object sender, RoutedEventArgs e)
        {
            if (Tab1.IsSelected)
            {
                if (string.IsNullOrEmpty((GoGetIt("VA"))))
                {
                    return;
                }
                Log1Box.BeginChange();
                if (Log1Box.Selection.Text != string.Empty)
                {
                    Log1Box.Selection.Text = string.Empty;
                }
                TextPointer tp = Log1Box.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
                Log1Box.CaretPosition.InsertTextInRun((GoGetIt("VA")));
                Log1Box.CaretPosition = tp;
                Log1Box.EndChange();
                Keyboard.Focus(Log1Box);
            }
            else
            {
                if (string.IsNullOrEmpty((GoGetIt("VA"))))
                {
                    return;
                }
                Log2Box.BeginChange();
                if (Log2Box.Selection.Text != string.Empty)
                {
                    Log2Box.Selection.Text = string.Empty;
                }
                TextPointer tp = Log2Box.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
                Log2Box.CaretPosition.InsertTextInRun((GoGetIt("VA")));
                Log2Box.CaretPosition = tp;
                Log2Box.EndChange();
                Keyboard.Focus(Log2Box);
            }
        }

        private void POS_Click(object sender, RoutedEventArgs e)
        {
            var result = Microsoft.VisualBasic.Interaction.InputBox("What parts are you sending?", "Parts Only Dispatch", "Answer Goes Here");
            if (result.Length > 0)
                if (Tab1.IsSelected)
                {
                    if (string.IsNullOrEmpty((GoGetIt("POS") + result + "\"")))
                    {
                        return;
                    }
                    Log1Box.BeginChange();
                    if (Log1Box.Selection.Text != string.Empty)
                    {
                        Log1Box.Selection.Text = string.Empty;
                    }
                    TextPointer tp = Log1Box.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
                    Log1Box.CaretPosition.InsertTextInRun((GoGetIt("POS") + result + "\""));
                    Log1Box.CaretPosition = tp;
                    Log1Box.EndChange();
                    Keyboard.Focus(Log1Box);
                }
                else
                {
                    if (string.IsNullOrEmpty((GoGetIt("POS") + result + "\"")))
                    {
                        return;
                    }
                    Log2Box.BeginChange();
                    if (Log2Box.Selection.Text != string.Empty)
                    {
                        Log2Box.Selection.Text = string.Empty;
                    }
                    TextPointer tp = Log2Box.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
                    Log2Box.CaretPosition.InsertTextInRun((GoGetIt("POS") + result + "\""));
                    Log2Box.CaretPosition = tp;
                    Log2Box.EndChange();
                    Keyboard.Focus(Log2Box);
                }
        }

        private void OST_Click(object sender, RoutedEventArgs e)
        {
            var result = Microsoft.VisualBasic.Interaction.InputBox("What parts are you sending?", "On Site Service Dispatch", "Answer Goes Here");
            if (result.Length > 0)
                if (Tab1.IsSelected)
                {
                    if (string.IsNullOrEmpty((GoGetIt("OST") + result + "\" via On Site Tech")))
                    {
                        return;
                    }
                    Log1Box.BeginChange();
                    if (Log1Box.Selection.Text != string.Empty)
                    {
                        Log1Box.Selection.Text = string.Empty;
                    }
                    TextPointer tp = Log1Box.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
                    Log1Box.CaretPosition.InsertTextInRun((GoGetIt("OST") + result + "\" via On Site Tech"));
                    Log1Box.CaretPosition = tp;
                    Log1Box.EndChange();
                    Keyboard.Focus(Log1Box);
                }
                else
                {
                    if (string.IsNullOrEmpty((GoGetIt("OST") + result + "\" via On Site Tech")))
                    {
                        return;
                    }
                    Log2Box.BeginChange();
                    if (Log2Box.Selection.Text != string.Empty)
                    {
                        Log2Box.Selection.Text = string.Empty;
                    }
                    TextPointer tp = Log2Box.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
                    Log2Box.CaretPosition.InsertTextInRun((GoGetIt("OST") + result + "\" via On Site Tech"));
                    Log2Box.CaretPosition = tp;
                    Log2Box.EndChange();
                    Keyboard.Focus(Log2Box);
                }
        }

        private void HDD_Click(object sender, RoutedEventArgs e)
        {
            var result = Microsoft.VisualBasic.Interaction.InputBox("What did the customer say about software?", "HDD Questions", "Answer Goes Here");
            if (result.Length > 0)
                if (Tab1.IsSelected)
                {
                    if (KYHD.IsChecked == true)
                    {
                        if (string.IsNullOrEmpty((GoGetIt("KYHD") + result + "\"")))
                        {
                            return;
                        }
                        Log1Box.BeginChange();
                        if (Log1Box.Selection.Text != string.Empty)
                        {
                            Log1Box.Selection.Text = string.Empty;
                        }
                        TextPointer tp = Log1Box.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
                        Log1Box.CaretPosition.InsertTextInRun((GoGetIt("KYHD") + result + "\""));
                        Log1Box.CaretPosition = tp;
                        Log1Box.EndChange();
                        Keyboard.Focus(Log1Box);
                    }

                    if (L1Y.IsChecked == true)
                    {
                        if (string.IsNullOrEmpty((GoGetIt("L1Y") + result + "\"")))
                        {
                            return;
                        }
                        Log1Box.BeginChange();
                        if (Log1Box.Selection.Text != string.Empty)
                        {
                            Log1Box.Selection.Text = string.Empty;
                        }
                        TextPointer tp = Log1Box.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
                        Log1Box.CaretPosition.InsertTextInRun((GoGetIt("L1Y") + result + "\""));
                        Log1Box.CaretPosition = tp;
                        Log1Box.EndChange();
                        Keyboard.Focus(Log1Box);
                    }

                    if (HDDOST.IsChecked == true)
                    {
                        if (string.IsNullOrEmpty((GoGetIt("HDDOST") + result + "\"")))
                        {
                            return;
                        }
                        Log1Box.BeginChange();
                        if (Log1Box.Selection.Text != string.Empty)
                        {
                            Log1Box.Selection.Text = string.Empty;
                        }
                        TextPointer tp = Log1Box.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
                        Log1Box.CaretPosition.InsertTextInRun((GoGetIt("HDDOST") + result + "\""));
                        Log1Box.CaretPosition = tp;
                        Log1Box.EndChange();
                        Keyboard.Focus(Log1Box);
                    }

                    if (HDDOST.IsChecked == false && L1Y.IsChecked == false && KYHD.IsChecked == false)
                    {
                        if (string.IsNullOrEmpty((GoGetIt("HDD") + result + "\"")))
                        {
                            return;
                        }
                        Log1Box.BeginChange();
                        if (Log1Box.Selection.Text != string.Empty)
                        {
                            Log1Box.Selection.Text = string.Empty;
                        }
                        TextPointer tp = Log1Box.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
                        Log1Box.CaretPosition.InsertTextInRun((GoGetIt("HDD") + result + "\""));
                        Log1Box.CaretPosition = tp;
                        Log1Box.EndChange();
                        Keyboard.Focus(Log1Box);
                    }
                }
                else
                {
                    if (KYHD.IsChecked == true)
                    {
                        if (string.IsNullOrEmpty((GoGetIt("KYHD") + result + "\"")))
                        {
                            return;
                        }
                        Log2Box.BeginChange();
                        if (Log2Box.Selection.Text != string.Empty)
                        {
                            Log2Box.Selection.Text = string.Empty;
                        }
                        TextPointer tp = Log2Box.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
                        Log2Box.CaretPosition.InsertTextInRun((GoGetIt("KYHD") + result + "\""));
                        Log2Box.CaretPosition = tp;
                        Log2Box.EndChange();
                        Keyboard.Focus(Log2Box);
                    }

                    if (L1Y.IsChecked == true)
                    {
                        if (string.IsNullOrEmpty((GoGetIt("L1Y") + result + "\"")))
                        {
                            return;
                        }
                        Log2Box.BeginChange();
                        if (Log2Box.Selection.Text != string.Empty)
                        {
                            Log2Box.Selection.Text = string.Empty;
                        }
                        TextPointer tp = Log2Box.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
                        Log2Box.CaretPosition.InsertTextInRun((GoGetIt("L1Y") + result + "\""));
                        Log2Box.CaretPosition = tp;
                        Log2Box.EndChange();
                        Keyboard.Focus(Log2Box);
                    }

                    if (HDDOST.IsChecked == true)
                    {
                        if (string.IsNullOrEmpty((GoGetIt("HDDOST") + result + "\"")))
                        {
                            return;
                        }
                        Log2Box.BeginChange();
                        if (Log2Box.Selection.Text != string.Empty)
                        {
                            Log2Box.Selection.Text = string.Empty;
                        }
                        TextPointer tp = Log2Box.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
                        Log2Box.CaretPosition.InsertTextInRun((GoGetIt("HDDOST") + result + "\""));
                        Log2Box.CaretPosition = tp;
                        Log2Box.EndChange();
                        Keyboard.Focus(Log2Box);
                    }

                    if (HDDOST.IsChecked == false && L1Y.IsChecked == false && KYHD.IsChecked == false)
                    {
                        if (string.IsNullOrEmpty((GoGetIt("HDD") + result + "\"")))
                        {
                            return;
                        }
                        Log2Box.BeginChange();
                        if (Log2Box.Selection.Text != string.Empty)
                        {
                            Log2Box.Selection.Text = string.Empty;
                        }
                        TextPointer tp = Log2Box.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
                        Log2Box.CaretPosition.InsertTextInRun((GoGetIt("HDD") + result + "\""));
                        Log2Box.CaretPosition = tp;
                        Log2Box.EndChange();
                        Keyboard.Focus(Log2Box);
                    }
                }
            }

        private void Plastics_Click(object sender, RoutedEventArgs e)
        {
            var result = Microsoft.VisualBasic.Interaction.InputBox("What did the customer say about plastics?", "Plastics Check", "Answer Goes Here");
            if (result.Length > 0)
                if (Tab1.IsSelected)
                {
                    if (string.IsNullOrEmpty((GoGetIt("Plastics") + result + "\"")))
                    {
                        return;
                    }
                    Log1Box.BeginChange();
                    if (Log1Box.Selection.Text != string.Empty)
                    {
                        Log1Box.Selection.Text = string.Empty;
                    }
                    TextPointer tp = Log1Box.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
                    Log1Box.CaretPosition.InsertTextInRun((GoGetIt("Plastics") + result + "\""));
                    Log1Box.CaretPosition = tp;
                    Log1Box.EndChange();
                    Keyboard.Focus(Log1Box);
                }
                else
                {
                    if (string.IsNullOrEmpty((GoGetIt("Plastics") + result + "\"")))
                    {
                        return;
                    }
                    Log2Box.BeginChange();
                    if (Log2Box.Selection.Text != string.Empty)
                    {
                        Log2Box.Selection.Text = string.Empty;
                    }
                    TextPointer tp = Log2Box.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
                    Log2Box.CaretPosition.InsertTextInRun((GoGetIt("Plastics") + result + "\""));
                    Log2Box.CaretPosition = tp;
                    Log2Box.EndChange();
                    Keyboard.Focus(Log2Box);
                }
        }

        private void FRU_Click(object sender, RoutedEventArgs e)
        {
            if (Tab1.IsSelected)
                {
                    if (string.IsNullOrEmpty((GoGetIt("FRU"))))
                    {
                        return;
                    }
                    Log1Box.BeginChange();
                    if (Log1Box.Selection.Text != string.Empty)
                    {
                        Log1Box.Selection.Text = string.Empty;
                    }
                    TextPointer tp = Log1Box.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
                    Log1Box.CaretPosition.InsertTextInRun((GoGetIt("FRU")));
                    Log1Box.CaretPosition = tp;
                    Log1Box.EndChange();
                    Keyboard.Focus(Log1Box);
                }
                else
                {
                    if (string.IsNullOrEmpty((GoGetIt("FRU"))))
                    {
                        return;
                    }
                    Log2Box.BeginChange();
                    if (Log2Box.Selection.Text != string.Empty)
                    {
                        Log2Box.Selection.Text = string.Empty;
                    }
                    TextPointer tp = Log2Box.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
                    Log2Box.CaretPosition.InsertTextInRun((GoGetIt("FRU")));
                    Log2Box.CaretPosition = tp;
                    Log2Box.EndChange();
                    Keyboard.Focus(Log2Box);
                }
        }

        private void CIDAR_Click(object sender, RoutedEventArgs e)
        {
            if (Tab1.IsSelected)
            {
                if (string.IsNullOrEmpty((GoGetIt("CIDAR"))))
                {
                    return;
                }
                Log1Box.BeginChange();
                if (Log1Box.Selection.Text != string.Empty)
                {
                    Log1Box.Selection.Text = string.Empty;
                }
                TextPointer tp = Log1Box.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
                Log1Box.CaretPosition.InsertTextInRun((GoGetIt("CIDAR")));
                Log1Box.CaretPosition = tp;
                Log1Box.EndChange();
                Keyboard.Focus(Log1Box);
            }
            else
            {
                if (string.IsNullOrEmpty((GoGetIt("CIDAR"))))
                {
                    return;
                }
                Log2Box.BeginChange();
                if (Log2Box.Selection.Text != string.Empty)
                {
                    Log2Box.Selection.Text = string.Empty;
                }
                TextPointer tp = Log2Box.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
                Log2Box.CaretPosition.InsertTextInRun((GoGetIt("CIDAR")));
                Log2Box.CaretPosition = tp;
                Log2Box.EndChange();
                Keyboard.Focus(Log2Box);
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            L1Y.IsChecked = false;
            HDDOST.IsChecked = false;
            KYHD.IsChecked = false;

            if (Tab1.IsSelected)
            {
                Log1Box.Document.Blocks.Clear();
                Log1Box.AppendText("Session ID: ");
            }
            else
            {
                Log2Box.Document.Blocks.Clear();
                Log2Box.AppendText("Session ID: ");
            }
        }

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