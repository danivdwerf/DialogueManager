using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueWindow : MonoBehaviour 
{
    [SerializeField]private GameObject dialogueWindow;
    public GameObject Window{get{return dialogueWindow;}}

    [SerializeField]private Button[] optionButtons; 
    private List<Text> optionLabels = new List<Text>();

    private Text textComponent;

	private void Awake() 
    {
        textComponent = dialogueWindow.GetComponentInChildren<Text>();
        if (!textComponent)
            throw new MissingComponentException();

        for (var i = 0; i < optionButtons.Length; i++)
        {
            optionLabels.Add(optionButtons[i].GetComponentInChildren<Text>());
            optionButtons[i].gameObject.SetActive(false);
        }
        this.show(false);
	}

    public void show(bool value)
    {
        dialogueWindow.SetActive(value);
    }

    public void setText(string newText)
    {
        textComponent.text = newText;
        for (var i = 0; i < optionButtons.Length; i++)
            optionButtons[i].gameObject.SetActive(false);
    }

    public Button setOption(string label)
    {
        for (var i = 0; i < optionButtons.Length; i++)
        {
            if (optionButtons[i].gameObject.activeSelf)
                continue;

            optionButtons[i].gameObject.SetActive(true);
            optionLabels[i].text = label;
            return optionButtons[i];
        }
        return null;
    }
}
