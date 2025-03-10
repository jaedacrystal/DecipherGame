using UnityEngine;
using System.Collections.Generic;

public class Class : MonoBehaviour
{
    public List<Cards> defenseCards;
    public List<Cards> offenseCards;
    public List<Cards> balanceCards;

    public void Defense()
    {
        SaveClass(ClassType.Defense, defenseCards);
    }

    public void Offense()
    {
        SaveClass(ClassType.Offense, offenseCards);
    }

    public void Balance()
    {
        SaveClass(ClassType.Balance, balanceCards);
    }

    void SaveClass(ClassType chosenClass, List<Cards> chosenCards)
    {
        PlayerPrefs.SetString("ChosenClass", chosenClass.ToString());

        string cardsJson = JsonUtility.ToJson(new CardListWrapper { cards = chosenCards });
        PlayerPrefs.SetString("ChosenClassCards", cardsJson);

        PlayerPrefs.Save();
    }

    [System.Serializable]
    private class CardListWrapper
    {
        public List<Cards> cards;
    }
}









