using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Pexel
{
    public class ActProps
    {
        public DateTime LastWriteTime { set; get; } = DateTime.Now;
        public Dictionary<string, ActProp> Items { set; get; } = new Dictionary<string, ActProp>();


        /*
        public static bool ReadBinary(string file, out PropsOpt3 result)
        {
            try
            {
                byte[] data = System.IO.File.ReadAllBytes(file);
                MemoryStream ms = new MemoryStream(data);
                using (BsonReader reader = new BsonReader(ms))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    result = serializer.Deserialize<PropsOpt3>(reader);
                }
                result.LastWriteTime = File.GetLastWriteTime(file);
            }
            catch (Exception ex)
            {
                result = null;
                return false;
            }
            return true;
        }




        public bool WriteBinary(string file)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                using (BsonWriter writer = new BsonWriter(ms))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(writer, this);
                }
                using (Stream stream = File.Open(file, FileMode.Create))
                {
                    BinaryWriter binaryWriter = new BinaryWriter(stream);
                    binaryWriter.Write(ms.ToArray());
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        */





        public bool WriteBinary(string file)
        {
            try
            {
                using (Stream stream = File.Open(file, FileMode.Create))
                using (BinaryWriter binaryWriter = new BinaryWriter(stream))
                {
                    binaryWriter.Write(Items.Values.Count);
                    foreach (ActProp item in Items.Values)
                        item.Write(binaryWriter);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }



        public static bool ReadBinary(string file, out ActProps result)
        {
            try
            {
                result = new ActProps
                {
                    Items = new Dictionary<string, ActProp>(),
                    LastWriteTime = File.GetLastWriteTime(file)
                };
                using (var stream = File.Open(file, FileMode.Open))
                using (var reader = new BinaryReader(stream))
                {
                    int count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        ActProp item = ActProp.Read(reader);
                        result.Items.Add(item.Title, item);
                    }
                }
            }
            catch (Exception ex)
            {
                result = null;
                return false;
            }
            return true;
        }












    }
}
