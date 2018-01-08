using System.Xml.Serialization;
using UnityEngine;
using System.Collections.Generic;



[XmlRoot("TileCollection")]
public class TileContainer {

    [XmlArray("Tiles")]
    [XmlArrayItem("Tile")]
    public List<TileXml> Tiles = new List<TileXml>();

    public static TileContainer Load()
    {
        TextAsset mace = Resources.Load("mace") as TextAsset;
        var serializer = new XmlSerializer(typeof(TileContainer));
        using (var stream = new System.IO.StringReader(mace.text))
        {
            return serializer.Deserialize(stream) as TileContainer;
        }
        
    }

    
}
