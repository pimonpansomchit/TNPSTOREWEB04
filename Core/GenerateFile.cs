using System.Xml.Serialization;
namespace TNPSTOREWEB.Core
{
    public class GenerateFile
    {
        public  void SaveToXml<T>(List<T> data, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, data);
            }
        }


    }
}
