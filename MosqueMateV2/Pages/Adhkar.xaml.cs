using MosqueMateV2.DataAccess.Models;
using MosqueMateV2.Domain.APIService;
using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Enums;
using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Domain.Repositories;
using MosqueMateV2.Extensions;
using MosqueMateV2.Helpers;
using MosqueMateV2.Properties;
using MosqueMateV2.Resources;
using Newtonsoft.Json;
using System.Windows;
using System.Windows.Controls;
using XamlAnimatedGif;
using Page = ModernWpf.Controls.Page;

namespace MosqueMateV2.Pages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Adhkar : Page
    {

        public string apiContent { get; set; } = string.Empty;
        public List<PrayerSlide> PrayerSlidesData { get; set; }
        RxTaskManger rxTaskManger;
        public Adhkar()
        {
            InitializeComponent();
            rxTaskManger = new();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var main = Application.Current.MainWindow;
           // main.MainFrame.Navigate(new Home());

        }
    }
}