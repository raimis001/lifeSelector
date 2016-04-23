using UnityEngine;
using UnityEngine.UI;

public class guiCowPanel : MonoBehaviour
{

	public Slider HpSlider;
	public guiAttack AttackPanel;

	private Cow _selectedCow;
	public Cow SelecteCow
	{
		set
		{
			_selectedCow = value;
		}
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		AttackPanel.Params = _selectedCow.Attack;
		HpSlider.value = _selectedCow.Hp;
	}
}
