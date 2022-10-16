using System.IO;
using Newtonsoft.Json;
using System.Xml.Serialization;
using Formatting = Newtonsoft.Json.Formatting;

namespace Generics
{
    public interface ISerializer<T> where T : Plant
    {
        void SerializeBox(Box<T> box, string filename);
        Box<T> DeserializeBox(string filename);
    }

    public class Json<T> : ISerializer<T> where T : Plant
    {
        public void SerializeBox(Box<T> box, string filename)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    writer.Write(JsonConvert.SerializeObject(box, Formatting.Indented));
                    writer.Close();
                }
            }
            catch
            {
                throw new FileNotFoundException();
            }
        }
        
        public Box<T> DeserializeBox(string filename)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    string box = reader.ReadToEnd();
                    reader.Close();
                    return JsonConvert.DeserializeObject<Box<T>>(box);
                }
            }
            catch
            {
                throw new FileNotFoundException();
            }
            
        }
    }
    
    public class Xml<T> : ISerializer<T> where T : Plant
    {
        public void SerializeBox(Box<T> box, string filename)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Box<T>));
                    serializer.Serialize(writer, box);
                    writer.Close();
                }
            }
            catch
            {
                throw new FileNotFoundException();
            }
        }
        
        public Box<T> DeserializeBox(string filename)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Box<T>));
                    Box<T> box = serializer.Deserialize(reader) as Box<T>;
                    reader.Close();
                    return box;
                }
            }
            catch
            {
                throw new FileNotFoundException();
            }
        }
    }

    public class PlayerXml
    {
        public void Serialize(Player player, string filename)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Player));
                    serializer.Serialize(writer, player);
                    writer.Close();
                }
            }
            catch
            {
                throw new FileNotFoundException();
            }
        }
        
        public Player Deserialize(string filename)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Player));
                    Player box = serializer.Deserialize(reader) as Player;
                    reader.Close();
                    return box;
                }
            }
            catch
            {
                throw new FileNotFoundException();
            }
        }
    }
}