using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class StatSystem : MonoBehaviour {
	private Text StatText;
	private Image StatPanel;

	private Dictionary<string, int> stats;

	private bool isOpened;

	// Use this for initialization
	void Start () {
		StatPanel = GameObject.FindGameObjectWithTag("ui").transform.FindChild("StatPanel").GetComponent<Image>();
		StatText = StatPanel.transform.FindChild("StatText").GetComponent<Text>();
		isOpened = false;
		StatPanel.gameObject.SetActive(false);

		stats = new Dictionary<string, int>() {
			{ "collectedItems", 0 },
			{ "torchItems",  0 }
		};

		var player = GameObject.FindWithTag ("Player").GetComponent<Player> ();
		player.OnCollect.AddListener (UpdateCollectStat);
		player.OnTorchPlace.AddListener (UpdateTorchStat);
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.O))
		{
			ToggleStats ();
		}
	}
	
	public void UpdateCollectStat()
	{
		stats["collectedItems"]++;
	}

	public void UpdateTorchStat(){
		stats["torchItems"]++;
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
		return stats.Aggregate ("", (acc, cur) => {
			return acc + string.Format("{0}: {1}\n", cur.Key.ToUpper(), cur.Value);
		});
	}
}
