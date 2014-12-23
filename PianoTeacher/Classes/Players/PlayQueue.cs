using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using PDFW;
using PDFW.Classes;
using PDFW.SL;
using PDFW.SL.Classes;

namespace PianoTeacher
{
    public class PlayQueue
    {
        public Queue<PlayNotaModel> PlayNotaQueue = new Queue<PlayNotaModel>();
        public Timer timer { get; set; }

        public void PrepareTimer()
        {
            if (this.timer != null)
                return;

            this.timer = new Timer(250);
            //this.timer.Enabled=false;
            //this.timer.Stop();
            this.timer.Elapsed += (s, e) =>
            {
                //var viewModel = state as MainWindow_ViewModel;
                this.PlayNext();
            };
        }

        public void StartTimer(IEnumerable<PlayNotaModel> notaList)
        {
            PrepareTimer();

            this.StopTimer();
            this.PlayNotaQueue.Clear();
            this.PlayNotaQueue.AddRange(notaList, false);

            this.timer.Start();

        }

        public void StopTimer()
        {
            PrepareTimer();
            this.timer.Stop();
        }

        public decimal playedDuration = 0;
        public PlayNotaModel playingNota = null;
        public void PlayNext()
        {
            bool playNext = false;
            if (playingNota == null)
            {
                playNext = true;
            }
            else
            {
                playedDuration += (decimal)timer.Interval;

                var totalDuration = playingNota.Duration * 1000;
                if (playedDuration >= totalDuration)
                    playNext = true;
            }

            if (playNext)
            {
                if (this.PlayNotaQueue.Count > 0)
                {
                    playingNota = this.PlayNotaQueue.Dequeue();
                    playedDuration = 0;
                }
                else
                    this.StopTimer();
            }
            else
            {
                // play
            }
        }
    }
}
