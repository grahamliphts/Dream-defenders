using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Chat : MonoBehaviour 
{
	public GameObject chat;
	public InputField input;
	public GameObject txtModel;
	public ScrollRect scrollRect;

	private bool _focus;
	public bool focus
	{
		set
		{
			_focus = value;
		}
		get
		{
			return _focus;
		}
	}
	private ArrayList _log;
	private int _maxMessages;
	private int _lastLogLen;
	private string _lastMsg;

	void Start () 
	{
		input.gameObject.SetActive(false);
		_log = new ArrayList();
		_maxMessages = 200;
		_lastLogLen = 0;
	}

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			if (_focus == true)
			{
				Debug.Log("Unfocus");
				SetInput(false);
			}
			else
			{
				Debug.Log("Focus");
				SetInput(true);
			}		 
		}
		if (Input.GetMouseButtonDown(0) && _focus == true)
			SetInput(false);

		if(_focus == true)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				_lastMsg = input.text;
				PrintText(input.text);
				input.text = "";
				input.ActivateInputField();
			}

			if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				input.text = _lastMsg;
				Debug.Log(input.text);
			}
			scrollRect.verticalNormalizedPosition = 0;
			
		}
		Debug.Log(input.text);
	}

	void SetInput(bool value)
	{
		input.gameObject.SetActive(value);
		if(value == false)
		{
			input.DeactivateInputField();
			input.text = "";
		}
		else
		{
			input.Select();
			input.ActivateInputField();
		}	
		_focus = value;
	}

	void PrintText(string txt)
	{
		GameObject texte = Instantiate(txtModel) as GameObject;
		_log.Add(texte);
		texte.transform.SetParent(chat.transform);
		texte.GetComponent<Text>().text = txt;
		if (_log.Count > 200)
		{
			Destroy((Object)_log[0]);
			_log.RemoveAt(0);
			
		}
	}
}
