using System.Windows;
using System.Windows.Threading;
using TripleTea.Tables;

namespace TripleTea
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private TimeSpan elapsedTime;
        private DateTime startTime;

        private bool paused;
        private bool firstTime = true;

        public MainWindow()
        {
            InitializeComponent();

            using var db = new RecordsContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            var user = db.users.Add(new users { username = "brat", password = "pass12" } );
            db.SaveChanges();
            db.users.Add(new settings { user = user });
            db.SaveChanges();
            timer = new DispatcherTimer();
        }


        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timer.Tick += TimerTick;

            startTime = DateTime.Now;
            paused = false;
            firstTime = false;

            // Start the timer
            timer.Start();
        }

        private void EndBtn_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            paused = true;
            firstTime = true;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            // Calculate the elapsed time since the timer started
            elapsedTime = DateTime.Now - startTime;

            // Update the text block with the elapsed time
            TimePassedText.Text = elapsedTime.ToString(@"hh\:mm\:ss");
        }
    }
}