using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Mmc.Mspace.RegularInspectionModule.Views
{
    /// <summary>
    /// VideoPlayView.xaml 的交互逻辑
    /// </summary>
    public partial class VideoPlayView
    {
        

        public VideoPlayView()
        {
            InitializeComponent();

            /*    if (Media.Source != null)
                {
                    ProgressSlider.Value = this.Media.Position.TotalSeconds;
                }*/

            //axMediaPlayer.=
            this.Media.Play();
            playBtn.Content = Helpers.ResourceHelper.FindKey("Puase");
            this.Media.ToolTip = Helpers.ResourceHelper.FindKey("ClickPuase");
        }

        private void stopBtn_Click(object sender, RoutedEventArgs e)
        {
           this.Media.Position = Media.Position - TimeSpan.FromSeconds(10);
        }
        private void mediaElement_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PlayerPause();
        }
        private void PlayerPause()
        {
           // SetPlayer(true);
            if (playBtn.Content.ToString() == "播放")
            {
                this.Media.Play();
                playBtn.Content = Helpers.ResourceHelper.FindKey("Puase");
                this.Media.ToolTip = Helpers.ResourceHelper.FindKey("ClickPuase");
                if (this.Media.Position.TotalSeconds < this.Media.NaturalDuration.TimeSpan.TotalSeconds)
                {
                    this.Media.Play();
                    playBtn.Content = Helpers.ResourceHelper.FindKey("Puase");
                    this.Media.ToolTip = Helpers.ResourceHelper.FindKey("ClickPuase");
                }
                  
               
                    else if (this.Media.Position.TotalSeconds == this.Media.NaturalDuration.TimeSpan.TotalSeconds)
                    {
                        TimeSpan ts = new TimeSpan(0);//mediaElement.Position = new TimeSpan((new DateTime(0, 0, 0, 0, 0, 0)).Ticks);
                    this.Media.Position = ts;//new TimeSpan((new DateTime(0, 0, 0, 0, 0, 0)).Ticks);
                       this.Media.Play();
                        playBtn.Content = Helpers.ResourceHelper.FindKey("Puase");
                    this.Media.ToolTip = Helpers.ResourceHelper.FindKey("ClickPuase");
                }
               // MessageBox.Show(Convert.ToString( this.Media.Position.TotalSeconds));
               // MessageBox.Show(Convert.ToString(this.Media.NaturalDuration.TimeSpan.TotalSeconds));
            }
            else
            {
                this.Media.Pause();
                playBtn.Content = Helpers.ResourceHelper.FindKey("Play");
                this.Media.ToolTip = Helpers.ResourceHelper.FindKey("ClicPlay");
            }
        }

        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            PlayerPause();
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Media.Position = this.Media.Position - TimeSpan.FromSeconds(10);
        }

        private void forwardBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Media.Position = this.Media.Position + TimeSpan.FromSeconds(10);
        }
      /*  DispatcherTimer timer = null;
        private void timer_tick(object sender, EventArgs e)
        {
            ProgressSlider.Value = this.Media.Position.TotalSeconds;
        }*/
        private void sliderPosition_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
          //  this.Media.Position = TimeSpan.FromSeconds(ProgressSlider.Value);
        }
        private TimeSpan TotalTime;
        private DispatcherTimer timerVideoTime;
        private void Media_MediaOpened(object sender, RoutedEventArgs e)
          { 
              TotalTime = this.Media.NaturalDuration.TimeSpan;

              // Create a timer that will update the counters and the time slider
              timerVideoTime = new DispatcherTimer();
              timerVideoTime.Interval = TimeSpan.FromSeconds(1);
              timerVideoTime.Tick += new EventHandler(timer_Tick);
              timerVideoTime.Start();
    }
      void timer_Tick(object sender, EventArgs e)
      {
          if (this.Media.Position.TotalSeconds == this.Media.NaturalDuration.TimeSpan.TotalSeconds)
          {

              playBtn.Content = Helpers.ResourceHelper.FindKey("Play");
              this.Media.ToolTip =  Helpers.ResourceHelper.FindKey("ClicPlay");
            }
            /*else if(this.Media.Position.TotalSeconds < this.Media.NaturalDuration.TimeSpan.TotalSeconds)
          {

              playBtn.Content = "暂停";
              this.Media.ToolTip = "点击暂停";
          }*/
          // Check if the movie finished calculate it's total time
          if (this.Media.NaturalDuration.TimeSpan.TotalSeconds > 0)
          {
              if (TotalTime.TotalSeconds > 0)
              {
                  //dating time slider
                 // if()
                    double timeresult = ProgressSlider.Value - this.Media.Position.TotalSeconds / TotalTime.TotalSeconds;
                    if(Math.Abs(timeresult)>0.02)
                    {
                        ProgressSlider.Value = this.Media.Position.TotalSeconds / TotalTime.TotalSeconds;
                    }
                   
              }
          }
      }
}
}
