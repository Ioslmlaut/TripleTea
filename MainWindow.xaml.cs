using System.IO;
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
        private DateTime endTime;

        private bool paused;
        private bool firstTime = true;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            using var db = new RecordsContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var s = DateTime.UtcNow;
            Thread.Sleep(3000);
            var e = DateTime.UtcNow;
            

            var user_list = new List<users>()
            {
                new users { username = "brat" },
                new users { username = "kart" },
                new users { username = "ofel" }
            };
            db.users.AddRange(user_list);
            db.SaveChanges();

            computers acer = new computers { name = "acer_laptop", users = user_list };
            computers asus = new computers { name = "asus_laptop", users = [user_list[2]] };
            db.computers.AddRange([acer, asus]);
            db.SaveChanges();

            var records_list = new List<records>()
            {
                new records { start_time = s, end_time = e, duration = (e-s), computer = acer, user = user_list[0] },
                new records { start_time = s, end_time = e, duration = (e-s), computer = acer, user = user_list[1] },
                new records { start_time = s, end_time = e, duration = (e-s), computer = asus, user = user_list[2] }
            };
            db.records.AddRange(records_list);
            db.SaveChanges();

            timer = new DispatcherTimer();
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var win = new UserSelectionWindow();
            win.ShowDialog();
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timer.Tick += TimerTick;

            startTime = DateTime.Now;

            // Start the timer
            timer.Start();
        }

        private void EndBtn_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();

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