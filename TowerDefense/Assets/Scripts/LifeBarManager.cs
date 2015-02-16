using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeBarManager : MonoBehaviour 
{
    public GameObject Player;
    public Scrollbar LifeValue;
    private Color _lifeBarColor;
    private float _life;
    private float _lifeBarValue;
    private PlayerLifeManager _lifeManager;
    private Image _lifeBar;

	void Start () 
    {
        _lifeManager = Player.GetComponent<PlayerLifeManager>();
        _lifeBar = transform.GetChild(0).GetComponent<Image>();
	}

	void Update () 
    {
        _life = _lifeManager.GetLife();
        _lifeBarValue = _life / 100;
        LifeValue.size = _life / 100;
        SetColorLife();
        _lifeBar.color = _lifeBarColor;
	}

    void SetColorLife()
    {
        if (_lifeBarValue >= 0.5f)
            _lifeBarColor = Color.green;
        else if (_lifeBarValue >= 0.25f && _lifeBarValue < 0.5f)
            _lifeBarColor = Color.yellow;
        else if (_lifeBarValue < 0.25)
            _lifeBarColor = Color.red;
    }
}
