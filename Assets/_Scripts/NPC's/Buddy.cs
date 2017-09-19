using UnityEngine;

using System.Xml;
using System.Collections.Generic;

public class Buddy : NPC 
{
    protected override void Start()
    {
        base.Start();
        base.loadDialogueFile();
        print(this.messages.Count);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void conversation()
    {
        for (var i = 0; i < this.messages.Count; i++)
        {
            print(messages[i].Message);
        }
    }
}
