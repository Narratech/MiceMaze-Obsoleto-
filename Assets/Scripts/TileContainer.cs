using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


[XmlRoot("TileCollection")]
public class TileContainer
{
    [XmlArray("Tiles")]
    [XmlArrayItem("Tile")]
    public List<Tile> m_Tiles = new List<Tile>();


    public static TileContainer Load(string path)
    {
        var serializer = new XmlSerializer(typeof(TileContainer));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as TileContainer;
        }
    }

}