public class Guy : NPC 
{
    protected override void Start()
    {
        base.Start();
    }

    public override void conversation()
    {
        dialogueWindow.setText("Talk to me baby");
        dialogueWindow.show(true);
    }
}
