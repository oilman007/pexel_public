using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace Pexel.Modifiers
{



    abstract class Modifier
    {
        public Modifier()
        {
            Title = "";
            Use = true;
        }
        public string Title { set; get; }
        public bool Use { set; get; }
        public byte Type { protected set; get; }


        public bool Apply(Grid grid, ref Prop prop)
        {
            return true;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Type);
        }



        const byte RadiusModifierType        = 0;
        const byte InterpolationModifierType = 1;
        public static bool Read(BinaryReader reader)
        {
            byte type = reader.ReadByte();
            return true;
        }
    }
}
