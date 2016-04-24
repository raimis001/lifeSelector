using UnityEngine;
using UnityEngine.UI;

public class guiAttack : MonoBehaviour
{
	public Text RadarText;
	public Text AttackRangeText;
	public Text AttackDamageText;
	public Text AttackSpeedText;
	public Text DefenceText;

	private AttackParams _params;
	public AttackParams Params
	{
		get { return _params; }
		set
		{
			_params = value;

			if (RadarText) RadarText.text = _params.RadarRange.ToString("0");
			if (AttackRangeText) AttackRangeText.text = _params.AttackRange.ToString("0");
			if (AttackDamageText) AttackDamageText.text = _params.AttackDamage.ToString("0");
			if (AttackSpeedText) AttackSpeedText.text = _params.AttackSpeed.ToString("0.00");
			if (DefenceText) DefenceText.text = _params.DefenceDamage.ToString("0");

		}
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}
