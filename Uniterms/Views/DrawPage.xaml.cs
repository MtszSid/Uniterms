using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;
using Uniterms.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Uniterms.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DrawPage : Page
    {
        public DrawPage()
        {
            ViewModel = new DrawViewModel();
            this.InitializeComponent();
        }

        private async void AddParallelOperation_Click(object sender, RoutedEventArgs e)
        {
            var content = new ParallelOperationContent();

            var result = await NewDalog("Dodaj operację zrównoleglania.", content);

            if (result is ContentDialogResult.Primary && !string.IsNullOrEmpty(content.Left) && !string.IsNullOrEmpty(content.Right))
            {
                ViewModel.NewParallel(content.Left, content.Right, content.Separator);
                ParallelRep.Visibility = Visibility.Visible;
                if (ViewModel.Sequence is not null)
                {
                    SubstituteButton.IsEnabled = true;

                }
            }
            else if (result is ContentDialogResult.Primary && (string.IsNullOrEmpty(content.Left) || string.IsNullOrEmpty(content.Right)))
            {
                InfoBar.Message = "Wprowadzany uniterm nie może być pusty.";
                InfoBar.IsOpen = true;
            }
        }

        private async void AddSequenceOperation_Click(object sender, RoutedEventArgs e)
        {
            var content = new SequenceOperationContent();

            var result = await NewDalog("Dodaj operację sekwencjonowania.", content);

            if (result is ContentDialogResult.Primary && !string.IsNullOrEmpty(content.Left) && !string.IsNullOrEmpty(content.Right))
            {
                ViewModel.NewSequence(content.Left, content.Right, content.Separator);
                SequenceRep.Visibility = Visibility.Visible;
                if (ViewModel.Parallel is not null)
                {
                    SubstituteButton.IsEnabled = true;
                }
            }
            else if (result is ContentDialogResult.Primary && (string.IsNullOrEmpty(content.Left) || string.IsNullOrEmpty(content.Right)))
            {
                InfoBar.Message = "Wprowadzany uniterm nie może być pusty.";
                InfoBar.IsOpen = true;   
            }
        }
        private async Task<ContentDialogResult> NewDalog(string title, object content)
        {
            ContentDialog dialog = new ContentDialog();

            dialog.XamlRoot = this.XamlRoot;
            dialog.Title = title;
            dialog.PrimaryButtonText = "Dodaj";
            dialog.CloseButtonText = "Anuluj";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = content;

            return await dialog.ShowAsync();
        }

        private async void SubstituteOperation_Click(object sender, RoutedEventArgs e)
        {
            var content = new SubstitutionPanel();

            ContentDialog dialog = new ContentDialog();

            dialog.XamlRoot = this.XamlRoot;
            dialog.Title = "Zamiana unitermu.";
            dialog.PrimaryButtonText = "Lewy";
            dialog.SecondaryButtonText = "Prawy";
            dialog.CloseButtonText = "Anuluj";
            dialog.DefaultButton = ContentDialogButton.Close;
            dialog.Content = content;

            var result = await dialog.ShowAsync();

            if (result is ContentDialogResult.Primary)
            {
                ViewModel.SetLeftOfParallel();
            }
            else if (result is ContentDialogResult.Secondary)
            {
                ViewModel.SetRightOfParallel();
            }
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            InfoTip.IsOpen = true;
        }
    }
}
