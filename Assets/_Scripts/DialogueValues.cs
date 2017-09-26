using UnityEngine;

using System.Xml;
using System.Collections.Generic;

public struct Option_node
{
    private string option;
    public string Option{get{return option;}}

    private int response;
    public int Response{get{return response;}}

    public Option_node(string option, int response)
    {
        this.option = option;
        this.response = response;
    }
};

public struct Message_node
{
    private string message;
    public string Message{get{return message;}}

    private List<Option_node> options;
    public List<Option_node> Options{get{return options;}}

    public Message_node(string message)
    {
        this.message = message;
        options = new List<Option_node>();
    }

    public void addOption(Option_node option)
    {
        options.Add(option);
    }
};

public static class DialogueValues
{
    public static XmlDocument dialogueDocument = LoadXMLFile.loadFromURL("http://freetimedev.com/resources/projects/dialogue/dialogue.xml");
}
