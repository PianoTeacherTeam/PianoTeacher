using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using PDFW;
using PDFW.Classes;
using PDFW.SL;
using PDFW.SL.Classes;
using System.Windows;
using System.Windows.Threading;
using System.Media;
using System.Windows.Media;
using System.Reflection;
using System.IO;

namespace PianoTeacher
{
    public class Player
    {

        public MediaPlayer mediaPlayer=new MediaPlayer();

        public void Play(PlayNotaModel pNota, Dispatcher dispatcher, onCompletedEventHandler onCompleted)
        {
            var ts = new ParameterizedThreadStart((notaObj) =>
            {
                //pNota.IsPlaying = true;

                var nota = notaObj as PlayNotaModel;

                //SystemSounds.Beep.Play();
                var fileName=GetFileForNota(pNota.Code);

                dispatcher.Invoke(new Action<string>((pFileName) =>
                {
                    mediaPlayer.Open(new Uri(pFileName));
                    mediaPlayer.Play();
                }), new object[] { fileName });


                var duration = (int)(nota.Duration * 500);
                Thread.Sleep(duration);

                dispatcher.Invoke(() =>
                    {
                        onCompleted.CallOnCompleted(true, null);
                    });                
            });


            var th = new Thread(ts);
            th.Start(pNota);                       
        }

        private string GetFileForNota(string notaCode)
        {
            var dir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            dir = dir.RemoveIfEndsWith(@"\");
            
            if (dir.ToLower().EndsWith(@"\debug"))
                dir = dir.RemoveIfEndsWith(@"\Debug");

            if (dir.ToLower().EndsWith(@"\bin"))
                dir = dir.RemoveIfEndsWith(@"\bin");

            dir = dir.AppendIfNotEndsWith(@"\");
            dir += @"Audio\BassFlute\wav\";

            var index = notaCode.GetNotaIndex();

            var fileName = index + "." + notaCode + ".wav";
            var path = dir + fileName;

            return path;
        }
    }
}
