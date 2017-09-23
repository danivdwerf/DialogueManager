using UnityEngine;
using UnityEngine.UI;

using System.Xml;
using System.Linq;
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
    [SerializeField]protected string name;
    [SerializeField]protected float interactionRange;
    protected Text interactionText;
    protected bool textActive;

    protected List<Message_node> messages = new List<Message_node>();

    protected CheckDistance2Player playerDistance;
    protected DialogueWindow dialogueWindow;

    protected virtual void Start()
    {
        this.dialogueWindow = GameObject.FindGameObjectWithTag(Tags.dialogueManager).GetComponent<DialogueWindow>();
        this.playerDistance = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<CheckDistance2Player>();
        this.interactionText = this.gameObject.GetComponentInChildren<Text>();
        this.interactionText.text = "";
        this.textActive = false;
        this.loadDialogueFile(LoadXMLFile.load("dialogue"));
    }

    protected void loadDialogueFile(XmlDocument xmlDoc)
    {
        XmlNodeList npcList = xmlDoc.GetElementsByTagName("npc");

        for (var i = 0; i < npcList.Count; i++)
        {
            if (npcList[i].Attributes["name"].Value != this.name)
                continue;
            
            XmlNodeList npcs = npcList[i].ChildNodes;
            for(var j = 0; j < npcs.Count; j++)
            {
                if (npcs[j].Name == "message")
                    messages.Add(new Message_node(npcs[j].InnerText));
                
                if (npcs[j].Name == "option")
                {
                    int parentMessage = int.Parse(npcs[j].Attributes["for"].Value);
                    int responseMessage;
                    if (!int.TryParse(npcs[j].Attributes["response"].Value, out responseMessage))
                        responseMessage = -1;
                    string option = npcs[j].InnerText;

                    messages[parentMessage].addOption(new Option_node(option, responseMessage));
                }
            }
        }
    }

    protected virtual void showMessage(int index)
    {
        dialogueWindow.setText(this.messages[index].Message);
        var options = this.messages[index].Options;

        for(var i = 0; i < options.Count; i++)
        {
            var test = i;
            var response = options[test].Response;
            Button option = dialogueWindow.setOption(options[i].Option);

            if (response < 0)
                option.onClick.AddListener(delegate(){dialogueWindow.show(false);});
            else
                option.onClick.AddListener(delegate(){this.showMessage(response);});
        }
        dialogueWindow.show(true);
    }

    protected virtual void Update()
    {   
        if (!playerDistance.inRange(this.transform.position, this.interactionRange))
        {
            if (textActive)
            {
                this.interactionText.text = "";
                textActive = false;
            }
            return;
        }

        if (!textActive)
        {
            this.interactionText.text = "Press 'E' to interact";
            textActive = true;
        }

        if (!Input.GetKeyDown(KeyCode.E))
            return;
        
        this.conversation();
    }

    public virtual void init(){} 
    public abstract void conversation();
}
