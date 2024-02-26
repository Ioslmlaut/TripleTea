using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace TripleTea
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer clock;
        private DispatcherTimer timer;
        private DateTime startTime;

        private bool paused;

        public MainWindow()
        {
            InitializeComponent();

            // Create a DispatcherTimer to update the time
            ClockText.Text = DateTime.Now.ToString("HH:mm:ss ddd dd/MM/yyyy");

            timer = new DispatcherTimer();
            clock = new DispatcherTimer();
            clock.Interval = TimeSpan.FromSeconds(1); // Update every second
            clock.Tick += Timer_Tick;
            clock.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Update the text block with the current time
            ClockText.Text = DateTime.Now.ToString("HH:mm:ss ddd dd/MM/yyyy");
        }

        private void StartEndBtn_Click(object sender, RoutedEventArgs e)
        {
            if (StartEndBtn.Content.ToString() == "Start")
            {
                StartTimer();
                StartEndBtn.Content = "End";
            }
            else
            {
                StopTimer();
                StartEndBtn.Content = "Start";
            }

        }

        private void StartTimer()
        {
            timer.Interval = TimeSpan.FromSeconds(0);
            timer.Tick += Timer_Ticks;

            startTime = DateTime.Now;

            // Start the timer
            timer.Start();
        }
        private void StopTimer()
        {
            timer.Stop();
            paused = false;
        }

        private void Timer_Ticks(object sender, EventArgs e)
        {
            // Calculate the elapsed time since the timer started
            TimeSpan elapsedTime = DateTime.Now - startTime;

            // Update the text block with the elapsed time
            TimePassedText.Text = elapsedTime.ToString(@"hh\:mm\:ss\:ff");
        }

        private void PauseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!paused)
            {
                timer.Stop();
                paused = true;
            }
            else
            {
                timer.Start();
                paused = false;
            }
        }
    }
}