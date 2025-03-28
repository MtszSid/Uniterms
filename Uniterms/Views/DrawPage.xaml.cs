using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Uniterms.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Threading.Tasks;
using Uniterms.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Uniterms.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DrawPage : Page
    {
        private DrawViewModel _viewModel;

        public DrawPage()
        {
            _viewModel= new DrawViewModel();
            this.InitializeComponent();
        }

        private async void AddParallelOperation_Click(object sender, RoutedEventArgs e)
        {
            var content = new ParallelOperationContent();

            var result = await NewDalog("Dodaj operację zrównoleglania.", content);
            
            if (result is ContentDialogResult.Primary)
            {
                ParallelText.Text = content.Left + content.Separator + content.Right;
                _viewModel.NewParallel(content.Left, content.Right, content.Separator);
            }
        }

        private async Task<ContentDialogResult> NewDalog(string title, object content)
        {
            ContentDialog dialog = new ContentDialog();

            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            dialog.XamlRoot = this.XamlRoot;
            //dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "Dodaj operację zrównoleglania.";
            dialog.PrimaryButtonText = "Dodaj";
            dialog.CloseButtonText = "Anuluj";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = content;

            return await dialog.ShowAsync();
        }
    }
}
