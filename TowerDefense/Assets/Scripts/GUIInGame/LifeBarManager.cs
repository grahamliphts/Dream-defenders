using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeBarManager : MonoBehaviour 
{
    public Scrollbar LifePlayerValue;
	public Scrollbar LifeNexusValue;
	public Image lifeBarNexus;

    private Color _lifeBarColor;
    private float _life;
    private float _lifeBarValue;
    private PlayerLifeManager _lifeManager;
	public NexusLife lifeNexus;

    private Image _lifeBar;

	//set the lifeBar to the player
	private GameObject _player;
	void Update () 
    {
		if (_lifeManager == null)
			return;
        if (_lifeManager.GetLife() >= 0)
        {
			_lifeBarValue = _lifeManager.GetLife() / 100;
			LifePlayerValue.size = _lifeBarValue;
            SetColorLife();
            _lifeBar.color = _lifeBarColor;
        }

		if(lifeNexus.GetLife() >= 0)
		{
			_lifeBarValue = lifeNexus.GetLife() / 100;
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

	public void SetLifeManager(PlayerLifeManager lifeManager)
	{
		_lifeManager = lifeManager;
	}

	public void SetLifeBar(Image lifeBar)
	{
		_lifeBar = lifeBar;
	}

	public void SetPlayer(GameObject player)
	{
		_player = player;
	}
}
