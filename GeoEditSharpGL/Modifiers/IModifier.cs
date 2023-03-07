using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace Pexel.Modifiers
{
    interface IModifier
    {
        string Title { set; get; }
        bool Use { set; get; }


        bool Apply(Grid grid, ref Prop prop);
        bool Write(BinaryWriter writer);
        bool Read(BinaryReader reader);
    }
}
