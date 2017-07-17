﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Prospector : MonoBehaviour {

	static public Prospector 	S;
	public Deck					deck;
    public Layout               layout;
    public TextAsset            deckXML;
    public TextAsset			layoutXML;
<<<<<<< HEAD
    public List<CardProspector> drawPile;
    public Vector3 layoutCenter;
    public float xOffset = 3;
    public float yOffset = -2.5f;
    public Transform layoutAnchor;

    public CardProspector target;
    public List<CardProspector> tableau;
    public List<CardProspector> discardPile;
=======
>>>>>>> 42a120ba49f020ef7c4e88d150510512532f3f24

	void Awake(){
		S = this;
	}

	void Start() {
		deck = GetComponent<Deck> ();
		deck.InitDeck (deckXML.text);
		Deck.Shuffle (ref deck.cards);

        layout = GetComponent<Layout>();
        layout.ReadLayout(layoutXML.text);
<<<<<<< HEAD
        drawPile = ConvertListCardsToListCardProspectors(deck.cards);
        LayoutGame();
=======
>>>>>>> 42a120ba49f020ef7c4e88d150510512532f3f24
	}

    CardProspector Draw()
    {
        CardProspector cd = drawPile[0];
        drawPile.RemoveAt(0);
        return (cd);
    }

    List<CardProspector> ConvertListCardsToListCardProspectors(List<Card> lCD)
    {
        List<CardProspector> lCP = new List<CardProspector>();
        CardProspector tCP;
        foreach (Card tCD in lCD)
        {
            tCP = tCD as CardProspector;
            lCP.Add(tCP);
        }
        return (lCP);
    }

    void LayoutGame()
    {
        if (layoutAnchor == null)
        {
            GameObject tGO = new GameObject("_LayoutAnchor");
            layoutAnchor = tGO.transform;
            layoutAnchor.transform.position = layoutCenter;
        }

        CardProspector cp;
        foreach (SlotDef tSD in layout.slotDefs)
        {
            cp = Draw();
            cp.faceUP = tSD.faceUp;
            cp.transform.parent = layoutAnchor;
            cp.transform.localPosition = new Vector3(
                layout.multiplier.x * tSD.x,
                layout.multiplier.y * tSD.y,
                -tSD.layerID);
            cp.layoutID = tSD.id;
            cp.slotDef = tSD;
            cp.state = CardState.tableau;

            cp.SetSortingLayerName(tSD.layerName);

            tableau.Add(cp);
        }

        MoveToTarget(Draw());
        UpdateDrawPile();
    }

    void MoveToTarget(CardProspector cd)
    {
        if (target != null) MoveToDiscard(target);
        target = cd;
        cd.state = CardState.target;
        cd.transform.parent = layoutAnchor;
        cd.transform.localPosition = new Vector3(
            layout.multiplier.x * layout.discardPile.x,
            layout.multiplier.y * layout.discardPile.y,
            -layout.discardPile.layerID);
        cd.faceUP = true;
        cd.SetSortingLayerName(layout.discardPile.layerName);
        cd.SetSortOrder(0);
    }
    void UpdateDrawPile()
    {
        CardProspector cd;
        for (int i = 0; i < drawPile.Count; i++)
        {
            cd = drawPile[i];
            cd.transform.parent = layoutAnchor;
            Vector2 dpStagger = layout.drawPile.stagger;
            cd.transform.localPosition = new Vector3(
                layout.multiplier.x * (layout.drawPile.x + i * dpStagger.x),
                layout.multiplier.y * (layout.drawPile.y + i * dpStagger.y),
                -layout.drawPile.layerID + 0.1f * i);
            cd.faceUP = false; 
            cd.state = CardState.drawpile;
            cd.SetSortingLayerName(layout.drawPile.layerName);
            cd.SetSortOrder(-10 * i);
        }
    }
    void MoveToDiscard(CardProspector cd)
    {
      
        cd.state = CardState.discard;
        discardPile.Add(cd);  
        cd.transform.parent = layoutAnchor;
        cd.transform.localPosition = new Vector3(
            layout.multiplier.x * layout.discardPile.x,
            layout.multiplier.y * layout.discardPile.y,
            -layout.discardPile.layerID + 0.5f);
        cd.faceUP = true;
        cd.SetSortingLayerName(layout.discardPile.layerName);
        cd.SetSortOrder(-100 + discardPile.Count);
    }
    public void CardClicked(CardProspector cd)
    {
        switch(cd.state)
        {
            case CardState.target:
                break;
            case CardState.drawpile:
                MoveToDiscard(target);
                MoveToTarget(Draw());
                UpdateDrawPile();
                break;
            case CardState.tableau:
                bool validMatch = true;
                if (!cd.faceUP)
                {
                    validMatch = false;
                }
                if (!AdjacentRank(cd, target))
                {
                    validMatch = false;
                }
                if (!validMatch) return;
                tableau.Remove(cd);
                MoveToTarget(cd);
                break;

        }
    }
    public bool AdjacentRank(CardProspector c0, CardProspector c1)
    {
        if (!c0.faceUP || !c1.faceUP) return (false);

        if (Mathf.Abs(c0.rank - c1.rank) == 1)
        {
            return (true);
        }
        if (c0.rank == 1 && c1.rank == 13) return (true);
        if (c0.rank == 13 && c1.rank == 1) return (true);

        return (false);
    }

}
