﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardState
{
    drawpile,
    tableau,
    target,
    discard
}

public class CardProspector : Card {
    public CardState state = CardState.drawpile;
    public List<CardProspector> hiddenBy = new List<CardProspector>();
    public int layoutID;
    public SlotDef slotDef;

    override public void OnMouseUpAsButton()
    {
        Prospector.S.CardClicked(this);
        base.OnMouseUpAsButton();
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
