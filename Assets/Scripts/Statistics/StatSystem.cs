using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatSystem : MonoBehaviour {
	private Text StatText;
	private Image StatPanel;

	private int collectedItems = 0;
	private bool isOpened = false;

	// Use this for initialization
	void Start () {
		StatPanel = GameObject.FindGameObjectWithTag("ui").transform.FindChild("StatPanel").GetComponent<Image>();
		StatText = StatPanel.transform.FindChild("StatText").GetComponent<Text>();
		StatPanel.gameObject.SetActive(false);
	}
	
	public void UpdateCollectStatistics()
	{
		collectedItems++;
	}

	public void ToggleStats() {
		if (isOpened) {
			StatPanel.gameObject.SetActive(false);
		} else {
			StatText.text = GetStatText ();
			StatPanel.gameObject.SetActive(true);
		}

		isOpened = !isOpened;
	}

	private string GetStatText()
	{
		return "Collected items: " + collectedItems;
	}
}
