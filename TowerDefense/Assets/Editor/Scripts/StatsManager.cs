using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System;
using System.Globalization;
using System.Collections.Generic;

using UnityEditor;


public class StatsManager : EditorWindow
{
    public TextAsset content;
	public LevelManager levelManager;
    //public bool CreateMissingPrefabs = false;

	[MenuItem("Window/StatsManager")]
    public static void ShowWindow()
    {
		EditorWindow.GetWindow(typeof(StatsManager));
    }
    
    void OnGUI()
    {
        GUILayout.Label ("Settings", EditorStyles.boldLabel);
		EditorGUILayout.BeginVertical();
		content = EditorGUILayout.ObjectField("XML File", content, typeof(TextAsset), true) as TextAsset;
		EditorGUILayout.EndVertical();

		if (GUILayout.Button("OK"))
			ReadFile();
		
    }

    public class StatEntry
    {
        public float growfactor0;
        public float growfactor1;
        public float growfactor2;
        public float growfactor3;

    }

    // Namespaces. We need this to initialize XmlNamespaceManager so that we can search XmlDocument.
    private static string[,] namespaces = new string[,] 
    {
        {"table", "urn:oasis:names:tc:opendocument:xmlns:table:1.0"},
        {"office", "urn:oasis:names:tc:opendocument:xmlns:office:1.0"},
        {"style", "urn:oasis:names:tc:opendocument:xmlns:style:1.0"},
        {"text", "urn:oasis:names:tc:opendocument:xmlns:text:1.0"},            
        {"draw", "urn:oasis:names:tc:opendocument:xmlns:drawing:1.0"},
        {"fo", "urn:oasis:names:tc:opendocument:xmlns:xsl-fo-compatible:1.0"},
        {"dc", "http://purl.org/dc/elements/1.1/"},
        {"meta", "urn:oasis:names:tc:opendocument:xmlns:meta:1.0"},
        {"number", "urn:oasis:names:tc:opendocument:xmlns:datastyle:1.0"},
        {"presentation", "urn:oasis:names:tc:opendocument:xmlns:presentation:1.0"},
        {"svg", "urn:oasis:names:tc:opendocument:xmlns:svg-compatible:1.0"},
        {"chart", "urn:oasis:names:tc:opendocument:xmlns:chart:1.0"},
        {"dr3d", "urn:oasis:names:tc:opendocument:xmlns:dr3d:1.0"},
        {"math", "http://www.w3.org/1998/Math/MathML"},
        {"form", "urn:oasis:names:tc:opendocument:xmlns:form:1.0"},
        {"script", "urn:oasis:names:tc:opendocument:xmlns:script:1.0"},
        {"ooo", "http://openoffice.org/2004/office"},
        {"ooow", "http://openoffice.org/2004/writer"},
        {"oooc", "http://openoffice.org/2004/calc"},
        {"dom", "http://www.w3.org/2001/xml-events"},
        {"xforms", "http://www.w3.org/2002/xforms"},
        {"xsd", "http://www.w3.org/2001/XMLSchema"},
        {"xsi", "http://www.w3.org/2001/XMLSchema-instance"},
        {"rpt", "http://openoffice.org/2005/report"},
        {"of", "urn:oasis:names:tc:opendocument:xmlns:of:1.2"},
        {"rdfa", "http://docs.oasis-open.org/opendocument/meta/rdfa#"},
        {"config", "urn:oasis:names:tc:opendocument:xmlns:config:1.0"}
    };

    private XmlNamespaceManager _InitializeXmlNamespaceManager(XmlDocument xmlDocument)
    {
        XmlNamespaceManager nmsManager = new XmlNamespaceManager(xmlDocument.NameTable);

        for (int i = 0; i < namespaces.GetLength(0); i++)
            nmsManager.AddNamespace(namespaces[i, 0], namespaces[i, 1]);

        return nmsManager;
    }

    private string _ReadCell(XmlNode cellNode, XmlNamespaceManager nmsManager, ref int cellIndex, int rowIndex)
    {
        XmlAttribute cellRepeated = cellNode.Attributes["table:number-columns-repeated"];

        string value = null;

        if (cellRepeated == null)
        {
            XmlAttribute cellVal = cellNode.Attributes["office:value"];

            if (cellVal == null)
                value = String.IsNullOrEmpty(cellNode.InnerText) ? null : cellNode.InnerText;
            else
                value = cellVal.Value;

            cellIndex++;
        }
        else
            cellIndex += Convert.ToInt32(cellRepeated.Value, CultureInfo.InvariantCulture);
        return value;
    }

    private void _ReadRow(XmlNode rowNode, XmlNamespaceManager nmsManager, ref int rowIndex, StatEntry entry)
    {
        XmlAttribute rowsRepeated = rowNode.Attributes["table:number-rows-repeated"];

        if (rowsRepeated == null || Convert.ToInt32(rowsRepeated.Value, CultureInfo.InvariantCulture) == 1)
        {
            XmlNodeList cellNodes = rowNode.SelectNodes("table:table-cell", nmsManager);

            int cellIndex = 0;
            foreach (XmlNode cellNode in cellNodes)
            {
				if (cellIndex != 0)
					continue;
                string value = _ReadCell(cellNode, nmsManager, ref cellIndex, rowIndex);
                if (rowIndex == 1)
                    entry.growfactor0 = float.Parse(value);
				else if (rowIndex == 2)
					entry.growfactor1 = float.Parse(value);
				else if (rowIndex == 3)
					entry.growfactor2 = float.Parse(value);
				if (rowIndex == 4)
					entry.growfactor3 = float.Parse(value);
            }

            rowIndex++;
        }
        else
        {
            rowIndex += Convert.ToInt32(rowsRepeated.Value, CultureInfo.InvariantCulture);
        }
    }

    private void _ReadSheet(XmlNode tableNode, XmlNamespaceManager nmsManager)
    {
        XmlNodeList rowNodes = tableNode.SelectNodes("table:table-row", nmsManager);

        int rowIndex = 0;
        StatEntry entry = new StatEntry();
        foreach (XmlNode rowNode in rowNodes)
            _ReadRow(rowNode, nmsManager, ref rowIndex, entry);

		float[] growFactors = { entry.growfactor0, entry.growfactor1, entry.growfactor2, entry.growfactor3 };

		GameObject gameManager = GameObject.Find("GameManager");
		levelManager = gameManager.GetComponent<LevelManager>();
		levelManager.growFactors = growFactors;
		EditorUtility.SetDirty(levelManager);
    }

    public void ReadFile()
    {
        XmlDocument contentXml = new XmlDocument();
        contentXml.LoadXml(content.text);

        // Initialize XmlNamespaceManager
        XmlNamespaceManager nmsManager = _InitializeXmlNamespaceManager(contentXml);;

        foreach (XmlNode tableNode in contentXml.SelectNodes("/office:document-content/office:body/office:spreadsheet/table:table", nmsManager))
            _ReadSheet(tableNode, nmsManager);

        Debug.Log("XML loaded !");
    }

}
