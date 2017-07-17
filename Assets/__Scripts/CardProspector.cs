using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
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
=======
public class CardProspector : MonoBehaviour {

>>>>>>> 42a120ba49f020ef7c4e88d150510512532f3f24
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
