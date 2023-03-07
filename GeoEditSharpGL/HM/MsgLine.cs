using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Pexel.HM.HistMatching;

namespace Pexel.HM
{
    public class MsgLine
    {
        public MsgLine() { }
        public MsgLine(DateTime dt, HMMessageType msgType, string text)
        {
            Dt = dt;
            MsgType = msgType;
            Text = text;
        }


        public DateTime Dt { set; get; }
        public HMMessageType MsgType { set; get; }
        public string Text { set; get; }
    }
}
