using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml;

namespace LogIt3
{
    /// <summary>
    /// Handles loading and retrieving text templates from XML configuration
    /// </summary>
    public class TemplateProvider
    {
        private const string ConfigFilePath = "Settings\\LogItWording.xml";
        private readonly Dictionary<string, string> _templates;

        public TemplateProvider()
        {
            _templates = new Dictionary<string, string>();
            LoadTemplates();
        }

        /// <summary>
        /// Loads templates from the XML configuration file
        /// </summary>
        private void LoadTemplates()
        {
            var xmlDoc = new XmlDocument();

            try
            {
                // Try to load from relative path
                string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigFilePath);

                if (!File.Exists(fullPath))
                {
                    throw new FileNotFoundException($"Template file not found at: {fullPath}");
                }

                xmlDoc.Load(fullPath);

                // Select all Phrase nodes
                XmlNodeList phraseNodes = xmlDoc.SelectNodes("/Wording/Phrase");

                if (phraseNodes == null || phraseNodes.Count == 0)
                {
                    throw new XmlException("No Phrase nodes found in the template file");
                }

                // Extract each template type
                foreach (XmlNode phraseNode in phraseNodes)
                {
                    foreach (XmlNode childNode in phraseNode.ChildNodes)
                    {
                        if (!string.IsNullOrWhiteSpace(childNode.InnerText))
                        {
                            _templates[childNode.Name] = childNode.InnerText;
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(
                    $"Template file is missing!\n\n{ex.Message}\n\nThe application will now close.",
                    "Missing Configuration",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
            catch (XmlException ex)
            {
                MessageBox.Show(
                    $"Error loading template file:\n\n{ex.Message}\n\nThe application will now close.",
                    "Configuration Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unexpected error loading templates:\n\n{ex.Message}\n\nThe application will now close.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// Gets a template by its key name
        /// </summary>
        /// <param name="templateKey">The template key (e.g., "VA", "POS", etc.)</param>
        /// <returns>The template text, or empty string if not found</returns>
        public string GetTemplate(string templateKey)
        {
            if (_templates.TryGetValue(templateKey, out string template))
            {
                return template;
            }

            MessageBox.Show(
                $"Template '{templateKey}' not found in configuration file.",
                "Template Not Found",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            return string.Empty;
        }

        /// <summary>
        /// Checks if a template exists
        /// </summary>
        public bool HasTemplate(string templateKey)
        {
            return _templates.ContainsKey(templateKey);
        }
    }
}
