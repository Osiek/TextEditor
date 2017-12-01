using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace TextEditor
{
    class DocumentManager
    {
        string filePath;
        RichTextBox body;
        public bool BodyTextChanged { get; set; }

        public object RichTextBoxStreamType { get; private set; }

        public DocumentManager(RichTextBox body)
        {
            filePath = null;
            this.body = body;
        }

        public void OpenDocument()
        {
            FileStream readText;
            filePath = getPathFromDialogWindow();
            if(filePath != null)
            {
                readText = readFromFile();
                TextRange textSelection = new TextRange(body.Document.ContentStart, body.Document.ContentEnd);
                textSelection.Load(readText, DataFormats.Rtf);
                BodyTextChanged = false;
            }
        }

        public async void SaveDocumentAs()
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Text files (*.rtf)|*.rtf";
            Nullable<bool> result = fileDialog.ShowDialog();
            string bodyText;

            if(result == true)
            {
                filePath = fileDialog.FileName;
                bodyText = new TextRange(body.Document.ContentStart, body.Document.ContentEnd).Text;
                using(StreamWriter outputFile = new StreamWriter(filePath))
                {
                    await outputFile.WriteAsync(bodyText);
                }
            }
        }

        public async void SaveDocument()
        {
            if(System.IO.File.Exists(filePath))
            {
                string bodyText = new TextRange(body.Document.ContentStart, body.Document.ContentEnd).Text;
                using (StreamWriter outputFile = new StreamWriter(filePath))
                {
                    await outputFile.WriteAsync(bodyText);
                }
            } else
            {
                SaveDocumentAs();
            }
        }

        public void NewDocument()
        {
            filePath = null;
            body.Document = new FlowDocument();
            BodyTextChanged = false;
        }

        public void TextChanged()
        {
            BodyTextChanged = true;
        }

        public void SetFontFamily(string FontFamily)
        {
            if (this.body.Selection.IsEmpty)
                this.body.FontFamily = new System.Windows.Media.FontFamily(FontFamily);
            else
                this.body.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, new System.Windows.Media.FontFamily(FontFamily));
        }

        public void SetFontSize(double FontSize)
        {
            if (this.body.Selection.IsEmpty)
                this.body.FontSize = FontSize;
            else
                this.body.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, FontSize);
        }

        public FileStream readFromFile()
        {
            FileStream fs = File.Open(filePath, FileMode.Open);

            return fs;
        }

        public void saveToFile()
        {

        }

        public string getPathFromDialogWindow()
        {
            string initialDirectory = @"C:\Users\ajzo-12\Desktop\TextEditor\TextEditor";
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Text files (*.rtf)|*.rtf";
            if (Directory.Exists(initialDirectory))
            {
                fileDialog.InitialDirectory = initialDirectory;
            }
            else
            {
                fileDialog.InitialDirectory = "C:\\";
            }

            Nullable<bool> result = fileDialog.ShowDialog();
            if (result == true) return filePath = fileDialog.FileName;

            return null;
        }

    }
}
