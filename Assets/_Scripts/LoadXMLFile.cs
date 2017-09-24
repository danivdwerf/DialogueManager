using System;
using System.Xml;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;

public static class LoadXMLFile
{
    public static XmlDocument loadFromResource(string file)
    {
        XmlDocument xmlDoc = new XmlDocument();
        TextAsset temp = Resources.Load(file) as TextAsset;
        if (!temp)
            throw new XmlException("Failed to load xml file");
        try
        {
            xmlDoc.LoadXml(temp.text);
            return xmlDoc;
        }
        catch(Exception e)
        {
            throw new Exception("failed to load xml: " + e);
            return null;
        }
    }

    public static XmlDocument loadFromURL(string url)
    {
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.Load(url);
            return xmlDoc;
        }
        catch(Exception e)
        {
            throw new Exception("failed to load xml: " + e);
            return null;
        }
    }
}
