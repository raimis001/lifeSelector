using UnityEngine;
using UnityEngine.UI;

public class guiCowPanel : MonoBehaviour
{

	public Slider HpSlider;
	public guiAttack AttackPanel;
	public Text MaxMonsters;
	public Text CurrentMonsters;

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
		if (_selectedCow)
		{
			AttackPanel.Params = _selectedCow.Attack;
			HpSlider.value = _selectedCow.Hp;
			MaxMonsters.text = _selectedCow.MaxMonsterCount.ToString("0");
			CurrentMonsters.text = _selectedCow.MonsterCount.ToString("0");
		}
	}
}
