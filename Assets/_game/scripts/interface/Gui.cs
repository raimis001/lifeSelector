﻿using UnityEngine;
using System.Collections;
using UnityEngineInternal;

public class Gui : MonoBehaviour
{

	public guiCowPanel CowPanel;

	void Awake()
	{
		The.Gui = this;
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
			The.Gui.CowPanel.SelecteCow = cow;
			The.Gui.PanelOpen(The.Gui.CowPanel.gameObject);
			return;
		}

		The.Gui.PanelClose(The.Gui.CowPanel.gameObject);
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
