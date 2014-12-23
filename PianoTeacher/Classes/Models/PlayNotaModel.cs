using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDFW;
using PDFW.Classes;
using PDFW.SL;

namespace PianoTeacher
{
    public enum PlayStateEnum
    {
        NotPlayed,
        Playing,
        Played
    }

    public class PlayNotaModel : BaseViewModel
    {
        public int ID { get { return GetValue<int>("ID"); } set { SetValue("ID", value); } }
        public string Code { get { return GetValue<string>("Code"); } set { SetValue("Code", value); } }
        public decimal Duration { get { return GetValue<decimal>("Duration"); } set { SetValue("Duration", value); } }

        public bool IsNext { get { return GetValue<bool>("IsNext"); } set { SetValue("IsNext", value); } }

        public PlayStateEnum PlayState { get { return GetValue<PlayStateEnum>("PlayState"); } set { SetValue("PlayState", value); } }

        public PlayNotaModel()
        {
            this.PlayState = PlayStateEnum.NotPlayed;
        }
    }
}
