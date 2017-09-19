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

public abstract class NPC : MonoBehaviour 
{
    [SerializeField]private string name;
    [SerializeField]private float interactionRange;

    protected List<Message_node> messages = new List<Message_node>();
    protected CheckDistance2Player playerDistance;
    protected DialogueWindow dialogueWindow;

    protected virtual void Start()
    {
        this.dialogueWindow = GameObject.FindGameObjectWithTag(Tags.dialogueManager).GetComponent<DialogueWindow>();
        this.playerDistance = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<CheckDistance2Player>();

    }

    protected void loadDialogueFile()
    {
        XmlDocument xmlDoc = new XmlDocument();
        TextAsset temp = Resources.Load("dialogue") as TextAsset;
        if (!temp)
            throw new XmlException("Failed to load xml file");

        xmlDoc.LoadXml(temp.text);
        XmlNodeList npcList = xmlDoc.GetElementsByTagName("npc");

        for (var i = 0; i < npcList.Count; i++)
        {
            if (npcList[i].Attributes["name"].Value != this.name)
                continue;
            
            XmlNodeList test = npcList[i].ChildNodes;
            for(var j = 0; j < test.Count; j++)
            {
                if (test[j].Name == "message")
                    messages.Add(new Message_node(test[j].InnerText));
                
                
            }
        }
    }

    protected virtual void Update()
    {   
        if (!Input.GetKeyDown(KeyCode.E))
            return;

        if(!playerDistance.inRange(this.transform.position, this.interactionRange))
            return;
        
        this.conversation();
    }

    public virtual void init(){}
    public abstract void conversation();
}
