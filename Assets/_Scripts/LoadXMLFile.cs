using System.Xml;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;

public static class LoadXMLFile
{
    public static XmlDocument load(string file)
    {
        XmlDocument xmlDoc = new XmlDocument();
        TextAsset temp = Resources.Load(file) as TextAsset;
        if (!temp)
            throw new XmlException("Failed to load xml file");
        xmlDoc.LoadXml(temp.text);
        return xmlDoc;
    }
}
