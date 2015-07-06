using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeBarManager : MonoBehaviour 
{
    public Scrollbar LifePlayerValue;
	public Scrollbar LifeNexusValue;
	public Image lifeBarNexus;

	//color/value of life
    private Color _lifeBarColor;
    private float _life;
    private float _lifeBarValue;
 
	public NexusLife LifeNexus;
	public Image lifeBar;

	private Stats _stats;
	public Stats stats
	{
		set
		{
			_stats = value;
		}
		get
		{
			return _stats;
		}
	}
	private GameObject _player;
	public GameObject player
	{
		set
		{
			_player = value;
		}
		get
		{
			return _player;
		}
	}

	void Update () 
    {
		if (_stats == null)
			return;
		if (_stats.life >= 0)
        {
			_lifeBarValue = _stats.life / _stats.lifeMax;
			LifePlayerValue.size = _lifeBarValue;
			Debug.Log(_lifeBarValue);
            SetColorLife();
			lifeBar.color = _lifeBarColor;
        }

		if (LifeNexus.GetLife() >= 0)
		{
			_lifeBarValue = LifeNexus.GetLife() / 100;
			LifeNexusValue.size = _lifeBarValue;
			SetColorLife();
			lifeBarNexus.color = _lifeBarColor;
		}
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
