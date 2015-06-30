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
	private NetworkView _networkView;

	void Start () 
	{
		input.gameObject.SetActive(false);
		_networkView = GetComponent<NetworkView>();
		_log = new ArrayList();
		_maxMessages = 200;
		_lastLogLen = 0;
	}

	void Update () 
	{
		if (LevelStart.instance.modeMulti)
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

			if (_focus == true)
			{
				if (Input.GetKeyDown(KeyCode.Return))
				{
					_lastMsg = input.text;
					//Debug Chat Solo
					//if (LevelStart.instance.modeMulti)
					NetworkPlayer player = Network.player;
					_networkView.RPC("PrintText", RPCMode.All, input.text, player.ToString());
					//else
						//PrintText(input.text);
					input.text = "";
					input.ActivateInputField();
				}

				if (Input.GetKeyDown(KeyCode.DownArrow))
					input.text = _lastMsg;
				scrollRect.verticalNormalizedPosition = 0;

			}
		}
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

	[RPC]
	private void PrintText(string txt, string id)
	{
		GameObject texte = Instantiate(txtModel) as GameObject;
		_log.Add(texte);
		texte.SetActive(true);
		texte.transform.SetParent(chat.transform, false);
		texte.GetComponent<Text>().text = "Player " + id + " : " + txt;
		if (_log.Count > 200)
		{
			Destroy((Object)_log[0]);
			_log.RemoveAt(0);

		}
	}
}
