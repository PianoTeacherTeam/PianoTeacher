using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDFW;
using PDFW.Classes;

namespace PianoTeacher
{
    public static class NotaExtensions
    {
        public static string[] NoteList = new string[] { "DO", "RE", "Mİ", "FA", "SOL", "LA", "Sİ" };

        public static int GetNotaIndex(this string pCode)
        {
            var code = pCode.NVLStr().ToUpper();

            if (NoteList.Contains(code))
                return NoteList.IndexOfIEnumerable(code) + 1;

            return -1;
        }
    }
}
