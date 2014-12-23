using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDFW;
using PDFW.Classes;
using PDFW.SL;
using PDFW.SL.Classes;

namespace PianoTeacher
{
    public static class Utils
    {
        public static void HandleCompleted(this bool result, DExceptionList expList)
        {
            if (!result)
                expList.ThrowIfRequired();
        }
    }
}
