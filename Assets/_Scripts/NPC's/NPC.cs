﻿using UnityEngine;
using UnityEngine.UI;

using System.Xml;
using System.Linq;
using System.Collections.Generic;

[RequireComponent(typeof(NPCui))]
public abstract class NPC : MonoBehaviour 
{
    [SerializeField]protected string name;
    [SerializeField]protected float interactionRange;

    protected List<Message_node> messages = new List<Message_node>();

    protected CheckDistance2Player playerDistance;
    protected DialogueWindow dialogueWindow;
    protected NPCui ui;

    protected virtual void Start()
    {
        this.setReferences();
        this.loadDialogueFile(DialogueValues.dialogueDocument);
    }

    protected void setReferences()
    {
        this.dialogueWindow = GameObject.FindGameObjectWithTag(Tags.dialogueManager).GetComponent<DialogueWindow>();
        this.playerDistance = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<CheckDistance2Player>();
        this.ui = this.GetComponent<NPCui>();
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
            ui.setInteractionText("");
            return;
        }

        ui.setInteractionText("Press 'E' to interact");

        if (!Input.GetKeyDown(KeyCode.E))
            return;
        
        this.conversation();
    }

    public virtual void init(){} 
    public abstract void conversation();
}
