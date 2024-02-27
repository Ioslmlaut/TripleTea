using System.Windows;
using System.Windows.Threading;

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

           timer = new DispatcherTimer();
        }


        private void StartPauseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (StartPauseBtn.Content.ToString() == "Start") // first time start
            {
                timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
                timer.Tick += TimerTick;

                startTime = DateTime.Now;
                paused = false;
                firstTime = false;

                // Start the timer
                timer.Start();
                StartPauseBtn.Content = "Pause";
            }
            else if (StartPauseBtn.Content.ToString() == "Resume") // pressed resume
            {
                startTime = DateTime.Now - elapsedTime;
                timer.Start();
                paused = false;
                StartPauseBtn.Content = "Pause";
            }
            else // pressed pause
            {
                timer.Stop();
                paused = true;
                StartPauseBtn.Content = "Resume";
            }
        }

        private void EndBtn_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            paused = true;
            firstTime = true;
            StartPauseBtn.Content = "Start";
        }

        private void TimerTick(object sender, EventArgs e)
        {
            // Calculate the elapsed time since the timer started
            elapsedTime = DateTime.Now - startTime;

            // Update the text block with the elapsed time
            TimePassedText.Text = elapsedTime.ToString(@"hh\:mm\:ss\:ff");
        }
    }
}