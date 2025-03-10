//using System;
//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;

//public class DialogueSystem : MonoBehaviour
//{
//    public int counter;
//    public int listCounter;
//    [HideInInspector] public bool canTalk;
//    private bool playerCheck;

//    public GameObject dialogueBG;
//    public TextMeshProUGUI player;
//    public TextMeshProUGUI dialogue;
//    public TextReveal textReveal;


//    [Serializable]
//    public class DialogueList
//    {
//        public string title;
//        public List<Dialogue> dialogues;
//    }

//    public List<DialogueList> dialogueList;

//    [Serializable]
//    public class Dialogue
//    {
//        public bool isPlayer = false;
//        public string playerName;
//        [TextArea(3, 3)]
//        public string dialogue;
//    }

//    private void Start()
//    {
//        listCounter = PlayerPrefs.GetInt("DialogueIndex", 0);

//        string playerName = PlayerPrefs.GetString("PlayerName", "Player");
//        playerCheck = dialogueList[listCounter].dialogues[counter].isPlayer;

//        if (playerCheck)
//        {
//            player.text = playerName;
//        }
//        else
//        {
//            player.text = dialogueList[listCounter].dialogues[counter].playerName;
//        }

//        Invoke("showDialogue", 1);
//        dialogue.text = "";
//    }


//    private void Update()
//    {
//        if (canTalk)
//        {
//            if (Input.GetKeyDown(KeyCode.F))
//            {
//                dialogueBG.SetActive(true);
//            }
//        }

//        if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
//        {
//            nextDialogue();
//        }
//    }

//    public void hideDialogue()
//    {
//        dialogueBG.SetActive(false);
//    }

//    public void showDialogue()
//    {
//        dialogueBG.SetActive(true);
//        counter = 0;
//        textReveal.StartReveal(dialogueList[listCounter].dialogues[counter].dialogue);
//    }

//    public void nextDialogue()
//    {
//        if (dialogueList[listCounter].dialogues.Count - 1 > counter)
//        {
//            counter++;

//            if (listCounter == 0 && counter == 1)
//            {
//                Invoke("hideDialogue", 5);
//            }

//            playerCheck = dialogueList[listCounter].dialogues[counter].isPlayer;

//            if (playerCheck)
//            {
//                player.text = PlayerPrefs.GetString("PlayerName", "Player");
//            }
//            else
//            {
//                player.text = dialogueList[listCounter].dialogues[counter].playerName;
//            }

//            textReveal.StartReveal(dialogueList[listCounter].dialogues[counter].dialogue);
//        }
//        else
//        {
//            counter = 0;
//            dialogueBG.SetActive(false);
//            dialogue.text = "";

//            if (listCounter == 1)
//            {
//                FindObjectOfType<NPCDialogue>().CheckDialogueEnd();
//            }
//        }
//    }
//}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public int counter;
    public int listCounter;
    [HideInInspector] public bool canTalk;
    private bool playerCheck;

    public GameObject dialogueBG;
    public TextMeshProUGUI player;
    public TextMeshProUGUI dialogue;
    public TextReveal textReveal;

    [Serializable]
    public class DialogueList
    {
        public string title;
        public List<Dialogue> dialogues;
    }

    public List<DialogueList> dialogueList;

    [Serializable]
    public class Dialogue
    {
        public bool isPlayer = false;
        public string playerName;
        [TextArea(3, 3)]
        public string dialogue;
    }

    private void Start()
    {
        // Check if opponent is defeated
        bool opponentDefeated = PlayerPrefs.GetInt("Opponent_Defeated", 0) == 1;

        // If opponent is defeated, skip index 0
        if (opponentDefeated && listCounter == 0)
        {
            listCounter = 1; // Change to the next available dialogue
        }
        else
        {
            listCounter = PlayerPrefs.GetInt("DialogueIndex", 0);
        }

        string playerName = PlayerPrefs.GetString("PlayerName", "Player");
        playerCheck = dialogueList[listCounter].dialogues[counter].isPlayer;

        if (playerCheck)
        {
            player.text = playerName;
        }
        else
        {
            player.text = dialogueList[listCounter].dialogues[counter].playerName;
        }

        Invoke("showDialogue", 1);
        dialogue.text = "";
    }

    private void Update()
    {
        if (canTalk && Input.GetKeyDown(KeyCode.F))
        {
            dialogueBG.SetActive(true);
        }

        if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
        {
            nextDialogue();
        }
    }

    public void hideDialogue()
    {
        dialogueBG.SetActive(false);
    }

    public void showDialogue()
    {
        dialogueBG.SetActive(true);
        counter = 0;
        textReveal.StartReveal(dialogueList[listCounter].dialogues[counter].dialogue);
    }

    public void nextDialogue()
    {
        if (dialogueList[listCounter].dialogues.Count - 1 > counter)
        {
            counter++;

            if (listCounter == 0 && counter == 1)
            {
                Invoke("hideDialogue", 5);
            }

            playerCheck = dialogueList[listCounter].dialogues[counter].isPlayer;

            if (playerCheck)
            {
                player.text = PlayerPrefs.GetString("PlayerName", "Player");
            }
            else
            {
                player.text = dialogueList[listCounter].dialogues[counter].playerName;
            }

            textReveal.StartReveal(dialogueList[listCounter].dialogues[counter].dialogue);
        }
        else
        {
            counter = 0;
            dialogueBG.SetActive(false);
            dialogue.text = "";

            if (listCounter == 1)
            {
                FindObjectOfType<NPCDialogue>().CheckDialogueEnd();
            }
        }
    }

    public void OnOpponentDefeated()
    {
        PlayerPrefs.SetInt("Opponent_Defeated", 1);
        PlayerPrefs.Save();
    }
}

