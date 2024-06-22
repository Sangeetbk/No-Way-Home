using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JournalSystemUI : MonoBehaviour
{
    public TextMeshProUGUI journalEntryText;
        
    public TextMeshProUGUI questEntryText;


    private void Update() 
    {
        
    
    }

    public void UpdateJournalEntry(string journalEntry)
    {
        journalEntryText.text += journalEntry + "\n\n";
    }

    public void UpdateQuestEntry(string questEntry)
    {
        questEntryText.text += questEntry + "\n";
    }
}
