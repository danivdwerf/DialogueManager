using UnityEngine;
using UnityEngine.UI;

public class NPCui : MonoBehaviour 
{
    [SerializeField]private Text interactionText;

    private void Start()
    {
        this.interactionText.text = "";
    }

    public void setInteractionText(string value)
    {
        this.interactionText.text = value;
    }
}
