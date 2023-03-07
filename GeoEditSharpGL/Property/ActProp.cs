using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Microsoft.Office.Core;
using Newtonsoft.Json.Linq;

namespace Pexel
{
    //[Serializable]
    public class ActProp
    {



        public ActProp() { }
        public ActProp(string title, Prop prop, Actnum actnum, double def_value) 
        { 
            Init(title, prop, actnum, def_value); 
        }


        public string Title { set; get; } = string.Empty;
        public double[] Values { set; get; } = Array.Empty<double>();


        void Init(string title, Prop prop, Actnum actnum, double def_value)
        {
            Title = title;
            int nx = actnum.NX(), ny = actnum.NY(), nz = actnum.NZ();
            Values = new double[actnum.NValues];
            for (int k = 0; k < nz; ++k)
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                    {
                        int index = actnum.Values[i, j, k];
                        Values[index] = prop.Values[i, j, k];
                    }
            Values[0] = def_value;
        }




        /*
        //https://www.newtonsoft.com/json/help/html/SerializeToBson.htm
        public bool Save(string filename)
        {
            try
            {
                this.Indecies = IndeciesDictionary.Keys.ToList();

                MemoryStream ms = new MemoryStream();
                using (BsonWriter writer = new BsonWriter(ms))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(writer, this);
                }
                using (Stream stream = File.Open(filename, FileMode.Create))
                {
                    BinaryWriter binaryWriter = new BinaryWriter(stream);
                    binaryWriter.Write(ms.ToArray());
                    stream.Close();
                }
                this.Indecies = new List<Index3D>();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }


        public static bool Load(string filename, out PropsOpt2 props)
        {
            try
            {
                byte[] data = System.IO.File.ReadAllBytes(filename);
                MemoryStream ms = new MemoryStream(data);
                using (BsonReader reader = new BsonReader(ms))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    props = serializer.Deserialize<PropsOpt2>(reader);
                    int n = 0;
                    foreach (Index3D index in props.Indecies)
                        props.IndeciesDictionary.Add(index, n++);
                    props.Indecies = new List<Index3D>();
                }
            }
            catch (Exception ex)
            {
                props = null;
                return false;
            }
            return true;
        }

        */


        public Prop GetProp(Actnum actnum)
        {
            int nx = actnum.NX(), ny = actnum.NY(), nz = actnum.NZ();
            Prop result = new Prop(nx, ny, nz, 0, Title);
            for (int k = 0; k < nz; ++k)
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                    {
                        int index = actnum.Values[i, j, k];
                        result.Values[i, j, k] = Values[index];
                    }
            return result;
        }





        public double GetValue(Index3D index, Actnum actnum)
        {
            return GetValue(index.I, index.J, index.K, actnum);         
        }

        public double GetValue(int i, int j, int k, Actnum actnum)
        {
            int index = actnum.Values[i, j, k];
            return Values[index];
        }













        public void Write(BinaryWriter writer)
        {
            writer.Write(Title);
            writer.Write(Values.Length);
            for (int i = 0; i < Values.Length; i++)
                writer.Write(Values[i]);
        }




        public static ActProp Read(BinaryReader reader)
        {
            ActProp result = new ActProp();
            result.Title = reader.ReadString();
            int lenght = reader.ReadInt32();
            result.Values = new double[lenght];
            for(int i = 0;i < lenght; i++)
                result.Values[i] = reader.ReadDouble();
            return result;
        }





        public double DefaultValue
        {
            set { Values[0] = value; }
            get { return Values[0]; }
        }


        public double Min()
        {
            double result = Values[1];
            for (int i = 2; i < Values.Length; ++i)
                if (result > Values[i])
                    result = Values[i];
            return result;
        }


        public double Max()
        {
            double result = Values[1];
            for (int i = 2; i < Values.Length; ++i)
                if (result > Values[i])
                    result = Values[i];
            return result;
        }


    }
}
