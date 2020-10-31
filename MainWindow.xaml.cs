using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using SpotifyLyricsFinder.APIs;

namespace SpotifyLyricsFinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SpotifyData _spotifyData = new SpotifyData();
        ApiseedsData _apiseedsData = new ApiseedsData();

        RootObjectSpotify songsSpotify;
        RootObjectApiseeds songsApiseeds;

        public MainWindow()
        {          
            InitializeComponent();           
        }


        private void SearchButton_OnClick(object sender, RoutedEventArgs e)
        {
            string songNameSearch = SearchTextBox.Text;
            if (songNameSearch == "")
            {
                songsList.ItemsSource = null;
                return;
            }

            songsSpotify = _spotifyData.searchSongs(songNameSearch);
            if(songsSpotify != null)
                songsList.ItemsSource = songsSpotify.tracks.items;
        }


        private void SearchTextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SearchButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }


        private void SongsList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            var selectedIndex = songsList.SelectedIndex;

            if (selectedIndex < 0) return;

            string songName = songsSpotify.tracks.items[selectedIndex].name;
            string songArtist = songsSpotify.tracks.items[selectedIndex].artists[0].name;

            TitleTextBox.Text = songName;

            //one song may have more than 1 artist
            string artists = "";
            foreach (var artist in songsSpotify.tracks.items[selectedIndex].artists)
            {
                artists = artists + artist.name + ", ";
            }

            ArtistTextBox.Text = artists.Substring(0, artists.Length - 2);

            try
            {
                songsApiseeds = _apiseedsData.searchLyrics(songName, songArtist);
                LyricsTextBlock.Text = songsApiseeds.result.track.text;
            }
            catch (NullReferenceException)
            {
                LyricsTextBlock.Text = "Couldn't find the lyrics :(";
            }
        }
    }
}
