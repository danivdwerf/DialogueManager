using UnityEngine;
using UnityEngine.UI;

public class DialogueWindow : MonoBehaviour 
{
    [SerializeField]private GameObject dialogueWindow;
    private Text textComponent;

	private void Awake() 
    {
        textComponent = dialogueWindow.GetComponentInChildren<Text>();
        if (!textComponent)
            throw new MissingComponentException();
        this.show(false);
	}

    public void show(bool value)
    {
        dialogueWindow.SetActive(value);
    }

    public void setText(string newText)
    {
        textComponent.text = newText;
    }
}
