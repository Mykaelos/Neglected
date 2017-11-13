using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : StatManager {
    public List<Card> CurrentCards = new List<Card>();


    public PlayerStatManager() {
        DealCards();
        Recalculate();
    }

    private void DealCards() {
        CurrentCards.Clear();

        int numberOfCards = Random.Range(1, 4);

        for (int x = 0; x < numberOfCards; x++) {
            for (int y = 0; y < 7; y++) { // It will try to add a unique keyed card 7 times, until it fails.
                var nextCard = AvailableCards.RandomElement();

                if (!ContainsKey(nextCard.Key)) {
                    CurrentCards.Add(nextCard);
                    break;
                }
            }
        }
    }

    protected override void SpecificRecalulate() {
        foreach (var card in CurrentCards) {
            Add(card.Key, card.Bonus);
        }
    }

    private bool ContainsKey(string key) {
        foreach (var card in CurrentCards) {
            if (card.Key.Equals(key)) {
                return true;
            }
        }
        return false;
    }

    private List<Card> AvailableCards = new List<Card> {
        new Card {
            Key = "WEALTH_GENERATION_PERCENT",
            Name = "Privileged",
            Bonus = 50,
            Description = "Wealth is generated much faster."
        },
        new Card {
            Key = "WEALTH_GENERATION_PERCENT",
            Name = "Disadvantaged",
            Bonus = -50,
            Description = "Wealth is generated much faster."
        },
        new Card {
            Key = "RUN_SPEED_PERCENT",
            Name = "Athletic",
            Bonus = 25,
            Description = ""
        },
        new Card {
            Key = "RUN_SPEED_PERCENT",
            Name = "Chronic Fatigue",
            Bonus = -25,
            Description = ""
        },
        new Card {
            Key = "HEALTH_MAX_PERCENT",
            Name = "Resilient",
            Bonus = 50,
            Description = ""
        },
        new Card {
            Key = "HEALTH_MAX_PERCENT",
            Name = "Chronic Pain",
            Bonus = -50,
            Description = ""
        }
    };
}

public class Card { // I need help with names...
    public string Key;
    public string Name;
    public int Bonus;
    public string Description;
}

/**
 * chronic pain
 * chronic Fatigue
 * 
 * 
 * Run speed
 * Max Health
 * Energy regen
 * Max Energy
 * 
 * Wealth Gain
 */ 