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
    public class SongModel : BaseViewModel
    {
        public string Code { get { return GetValue<string>("Code"); } set { SetValue("Code", value); } }
        public string Description { get { return GetValue<string>("Description"); } set { SetValue("Description", value); } }
        public string NotaList { get { return GetValue<string>("NotaList"); } set { SetValue("NotaList", value); } }
    }
}
