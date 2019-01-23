﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MacConverterUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private string RemoveUnwantedCharacters(string input, IEnumerable<char> allowedCharacters)
        {
            {
                var filtered = input.ToCharArray()
                    .Where(c => allowedCharacters.Contains(c))
                    .ToArray();

                return new String(filtered);
            }
        }

        public string inputMac { get; set; }

        public string outputMac { get; set; }

        public string allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        private async void none_to_2dash(object obj)

        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal,() =>{
                inputMac = macTxt.Text;
                string filteredString = RemoveUnwantedCharacters(inputMac, allowedCharacters);
                filteredString = filteredString.Insert(2, "-");
                filteredString = filteredString.Insert(5, "-");
                filteredString = filteredString.Insert(8, "-");
                filteredString = filteredString.Insert(11, "-");
                filteredString = filteredString.Insert(14, "-");
                outputMac = filteredString;
                resultTxt.Text = outputMac;
            });
        }

        private async void none_to_2colon(object obj)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                inputMac = macTxt.Text;
                string filteredString = RemoveUnwantedCharacters(inputMac, allowedCharacters);
                filteredString = filteredString.Insert(2, ":");
                filteredString = filteredString.Insert(5, ":");
                filteredString = filteredString.Insert(8, ":");
                filteredString = filteredString.Insert(11, ":");
                filteredString = filteredString.Insert(14, ":");
                outputMac = filteredString;
                resultTxt.Text = outputMac;
            });
        }

        private async void none_to_4dot(object obj)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                inputMac = macTxt.Text;
                string filteredString = RemoveUnwantedCharacters(inputMac, allowedCharacters);
                filteredString = filteredString.Insert(4, ".");
                filteredString = filteredString.Insert(9, ".");
                outputMac = filteredString;
                resultTxt.Text = outputMac;
            }
);
        }

        private async void ConvertSingle_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(macTxt.Text))
            {
                var dialog = new MessageDialog("Missing" +
                    " MAC, check data entry");
                await dialog.ShowAsync();
                return;
            }

            if (radDash.IsChecked == true)
            {
                Thread thread = new Thread(none_to_2dash);
                thread.IsBackground = true;
                thread.Start();
            }

            if (radColon.IsChecked == true)
            {
                Thread thread = new Thread(none_to_2colon);
                thread.IsBackground = true;
                thread.Start();
            }

            if (rad4Dot.IsChecked == true)
            {
                Thread thread = new Thread(none_to_4dot);
                thread.IsBackground = true;
                thread.Start();
            }

        }


        #region Batch Pivot
        private string _sourcefile = null;

        private string _outfile = null;

        public string sourcefile
        {
            get { return _sourcefile; }
            set { _sourcefile = value; }
        }

        public string outfile
        {
            get { return _outfile; }
            set { _outfile = value; }
        }

        private async void Browser1_Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            picker.FileTypeFilter.Add(".txt");
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                _sourcefile = file.Path;
                srcTxtBlk.Text = sourcefile;
            }

        }

        private async void Browser2_Button_ClickAsync(object sender, RoutedEventArgs e)
        {

            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            picker.FileTypeFilter.Add(".txt");
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                _outfile = file.Path;
                outTxtBlock.Text = outfile;
            }
           
        }

        private void Convert_macs_button_Click(object sender, RoutedEventArgs e)
        {


            List<string> results = new List<string>();

            foreach (string line in File.ReadLines(sourcefile))
            {
                if (radDash2.IsChecked == true)
                {
                    inputMac = line;
                    string filteredString = RemoveUnwantedCharacters(inputMac, allowedCharacters);
                    filteredString = filteredString.Insert(2, "-");
                    filteredString = filteredString.Insert(5, "-");
                    filteredString = filteredString.Insert(8, "-");
                    filteredString = filteredString.Insert(11, "-");
                    filteredString = filteredString.Insert(14, "-");
                    outputMac = filteredString;
                    results.Add(outputMac);

                }

                if (radColon2.IsChecked == true)
                {
                    inputMac = line;
                    string filteredString = RemoveUnwantedCharacters(inputMac, allowedCharacters);
                    filteredString = filteredString.Insert(2, ":");
                    filteredString = filteredString.Insert(5, ":");
                    filteredString = filteredString.Insert(8, ":");
                    filteredString = filteredString.Insert(11, ":");
                    filteredString = filteredString.Insert(14, ":");
                    outputMac = filteredString;
                    results.Add(outputMac);
                }

                if (rad4Dot2.IsChecked == true)
                {
                    inputMac = line;
                    string filteredString = RemoveUnwantedCharacters(inputMac, allowedCharacters);
                    filteredString = filteredString.Insert(4, ".");
                    filteredString = filteredString.Insert(9, ".");
                    outputMac = filteredString;
                    results.Add(outputMac);
                }
            }

            System.IO.File.WriteAllLines(outfile, results);

            string sometext = System.IO.File.ReadAllText(outfile);

            resultBoxTxt.Text = sometext;

        }


        #endregion

    }
}