using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using SpotifyLyricsFinder.APIs.Apiseeds;
using SpotifyLyricsFinder.APIs.Genius;
using SpotifyLyricsFinder.APIs.Spotify;

namespace SpotifyLyricsFinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SpotifyData _spotifyData = new SpotifyData();
        ApiseedsData _apiseedsData = new ApiseedsData();
        GeniusData _geniusData = new GeniusData();

        RootObjectSpotify songsSpotify;
        RootObjectApiseeds songsApiseeds;
        RootObjectGenius songsGenius;

        public MainWindow()
        {
            InitializeComponent();
        }


        private async void SearchButton_OnClick(object sender, RoutedEventArgs e)
        {
            string songNameSearch = SearchTextBox.Text;
            if (songNameSearch == "")
            {
                songsList.ItemsSource = null;
                return;
            }

            try
            {
                songsSpotify = await _spotifyData.SearchSongs(songNameSearch);
            }
            catch
            {

            }
            if (songsSpotify != null)
                songsList.ItemsSource = songsSpotify.tracks.items;
        }


        private void SearchTextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SearchButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }


        private async void SongsList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
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
            string lyrics = "";


            //firstly search for lyrics in Apiseeds API
            try
            {
                songsApiseeds = await _apiseedsData.SearchLyrics(songName, songArtist);
                lyrics = songsApiseeds.result.track.text;
            }
            catch
            {
                lyrics = "";
            }
            if (!String.IsNullOrWhiteSpace(lyrics))
            {
                LyricsTextBlock.Text = lyrics;
                return;
            }


            //secondly search for lyrics in genius.com
            try
            {
                songsGenius = await _geniusData.SearchSongs(songArtist, songName);
                var url = songsGenius.response.hits[0].result.url;
                GeniusScrap geniusScrap = new GeniusScrap();
                lyrics = await geniusScrap.ScrapLyrics(url);
            }
            catch
            {
                lyrics = "";
            }
            if (!String.IsNullOrWhiteSpace(lyrics))
            {
                LyricsTextBlock.Text = lyrics;
                return;
            }

            //if there weren't any lyrics in both APIs
            LyricsTextBlock.Text = "Couldn't find the lyrics :(";
        }


        //couldn't do it in constructor, as GetNewToken() is async function (because it uses POST request)
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _spotifyData.spotifyApi.GetNewToken();
        }
    }
}
