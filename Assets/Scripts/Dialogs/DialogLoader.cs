using UnityEngine;
using UnityEngine.UI;

public class DialogLoader : MonoBehaviour
{
    public TextAsset DialogFile;
    private Text DialogText;
    private Image Panel;

    [HideInInspector]
    public string[] DialogLines;

    private int currentLine;

	public void Start()
    {
        Panel = GameObject.FindGameObjectWithTag("ui").transform.FindChild("DialogPanel").GetComponent<Image>();
        DialogText = Panel.transform.FindChild("DialogText").GetComponent<Text>();

        DialogLines = DialogFile.text.Split('\n');
        ResetDialog();
	}

    public void NextLine()
    {
        DialogText.text = DialogLines[currentLine];
        Panel.gameObject.SetActive(true);


        if (currentLine < DialogLines.Length - 1)
        {
            currentLine++;
        }
        else
        {
            currentLine = 0;
        }
    }

    public void ResetDialog()
    {
        currentLine = 0;
        Panel.gameObject.SetActive(false);
    }
}
