using ConsoleApp;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Music> musicList = new List<Music>();
        public int currentMusicId = 0;
        public MainWindow()
        {
            InitializeComponent();
            musicList = GetMusicList(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Data.txt");
            RefreshMusic(currentMusicId);
        }
        public static List<Music> GetMusicList(string path)
        {
            List<Music> musicList = new List<Music>();
            StreamReader readText = new StreamReader(path);

            while (!readText.EndOfStream)
            {
                string artist = readText.ReadLine();
                string album = readText.ReadLine();
                int songsNumber = int.Parse(readText.ReadLine());
                int year = int.Parse(readText.ReadLine());
                int downloadNumber = int.Parse(readText.ReadLine());

                string skipRecord = readText.ReadLine();

                Music music = new Music(artist, album, songsNumber, year, downloadNumber);
                musicList.Add(music);
            }

            readText.Close();

            return musicList;
        }

        public void RefreshMusic(int id)
        {
            if(id < 0)
            {
                id = musicList.Count-1;
                currentMusicId = id;
            }

            if(id == musicList.Count)
            {
                id = 0;
                currentMusicId = id;
            }

            Artist.Content = musicList[id].artist;
            Album.Content = musicList[id].album;
            SongsNumber.Content = musicList[id].songsNumber + " utworów";
            Year.Content = musicList[id].year;
            DownloadNumber.Content = musicList[id].downloadNumber;
        }
        private void DownloadBTN_Click(object sender, RoutedEventArgs e)
        {
            Music music = musicList[currentMusicId];
            music.downloadNumber += 1;
            musicList[currentMusicId] = music;
            RefreshMusic(currentMusicId);
        }

        private void PreviousMusicBTN_Click(object sender, RoutedEventArgs e)
        {
            RefreshMusic(--currentMusicId);
        }

        private void NextMusicBTN_Click(object sender, RoutedEventArgs e)
        {
            RefreshMusic(++currentMusicId);
        }
    }
}