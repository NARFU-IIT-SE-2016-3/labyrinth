using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
	void Start () 
	{

	}

	void Update () 
	{

	}

	void OnGUI()
	{
		GUI.Box(new Rect(Screen.width/2-100, Screen.height/2-150, 200, 300), "Меню");

		if(GUI.Button(new Rect(Screen.width/2-50, Screen.height/2-100, 100, 25), "Играть"))
		{
			Application.LoadLevel("Main");
		}

		if(GUI.Button(new Rect(Screen.width/2-50, Screen.height/2-50, 100, 25), "Настройки"))
		{

		}

		if(GUI.Button(new Rect(Screen.width/2-50, Screen.height/2, 100, 25), "Выход"))
		{
			Application.Quit();
		}
	}
}
