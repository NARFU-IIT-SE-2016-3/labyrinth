using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class DialogLoader : MonoBehaviour
{
	public void Start()
    {
        var dialog = ReadXml("sample.xml");
	}

    public XmlDocument ReadXml(string fileName)
    {
        var xml = new XmlDocument();
        xml.Load("Assets/Dialogs/" + fileName);
        return xml;
    }
}
