using UnityEngine;
using System.Collections;
using UnityEngineInternal;

public class Gui : MonoBehaviour
{

	private static Gui _instance;
	public static Gui Instance { get { return _instance; } }

	public guiCowPanel CowPanel;

	void Awake()
	{
		_instance = this;
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public static void SetCow(Cow cow)
	{
		if (cow)
		{
			Instance.CowPanel.SelecteCow = cow;
			Instance.PanelOpen(Instance.CowPanel.gameObject);
			return;
		}

		Instance.PanelClose(Instance.CowPanel.gameObject);
	}

	public void CowPanelClose()
	{
		GameLogic.SelectedCow = null;
	}

	public void PanelOpen(GameObject panel)
	{
		PanelOperate(panel, true);
	}
	public void PanelClose(GameObject panel)
	{
		PanelOperate(panel, false);
	}
	public void PanelSwitc(GameObject panel)
	{
		if (!panel)
		{
			return;
		}
		panel.SetActive(!panel.activeSelf);
	}

	private void PanelOperate(GameObject panel, bool open)
	{
		if (!panel)
		{
			return;
		}
		panel.SetActive(open);
	}

}
