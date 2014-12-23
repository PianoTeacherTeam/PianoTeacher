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
    public class NotaModel : BaseViewModel
    {
        public string Code { get { return GetValue<string>("Code"); } set { SetValue("Code", value); } }
        public bool IsPressed { get { return GetValue<bool>("IsPressed"); } set { SetValue("IsPressed", value); } }
    }
}
