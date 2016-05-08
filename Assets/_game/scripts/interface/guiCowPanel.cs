using UnityEngine;
using UnityEngine.UI;

public class guiCowPanel : MonoBehaviour
{

	public Slider HpSlider;
	public guiAttack AttackPanel;
	public Text MaxMonsters;
	public Text CurrentMonsters;

	public GameObject MotherPanel;

	private Cow _selectedCow;
	public Cow SelecteCow
	{
		set
		{
			_selectedCow = value;
			if (!_selectedCow) return;

			CowActivity activity = _selectedCow.GetComponent<CowActivity>();
			if (!activity)
			{
				Debug.Log("Not cow activitie");

				//AttackPanel.gameObject.SetActive(false);
				//MotherPanel.SetActive(false);
				//return;
			}

			AttackPanel.gameObject.SetActive(activity is AttackActivity);
			MotherPanel.SetActive(activity is MotherActivity);

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
			if (_selectedCow.Activitie<AttackActivity>())
			{
				AttackPanel.Params = _selectedCow.Activitie<AttackActivity>().Attack;
			}
			HpSlider.value = _selectedCow.Hp;
			MaxMonsters.text = _selectedCow.MaxMonsterCount.ToString("0");
			CurrentMonsters.text = _selectedCow.MonsterCount.ToString("0");
		}
	}

	public void CreateCow()
	{
		if (_selectedCow == null)
		{
			return;
		}
		MotherActivity activity = _selectedCow.GetComponent<MotherActivity>();
		if (!activity)
		{
			Debug.LogError("Not cow activitie");
			return;
		}

		activity.SpawnCow();
	}
}
