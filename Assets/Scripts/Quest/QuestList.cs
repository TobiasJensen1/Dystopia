using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestList : MonoBehaviour
{

    public List<Quest> quests = new List<Quest>();

    // Start is called before the first frame update
    void Start()
    {
        //First Quest
        List<QuestEvent> firstQuestEvents = new List<QuestEvent>();
        firstQuestEvents.Add(new QuestEvent(1, "Talk to Asshina", 
            "Hello Traveler. I see you already had a taste of the filth this place has become. I Would suggest you go sleep in the tavern nearby before you head north into the Blossom Valley, its a very nasty place, especially at night. Also, let me teach you this trick i learned from the Eastern lands. If you wish to use it, you can press Q at any time while you're under attack. But be wary, it requires Fury to spend!"
            , false, false));
        firstQuestEvents.Add(new QuestEvent(2, "Sleep at the Tavern", "After a short night in the tavern, you find yourself well rested, ready to take on your mission. You will need to head North into the Blossom Valley."
            , false, false));
        firstQuestEvents.Add(new QuestEvent(3, "Help Thalien", "Help! My wares got attacked by forest spiders! There might be multiple of them, i can teach you this swipe attack i learned back home in the Elvern lands. It will come in handy when dealing with multiple enemies. I also have these healthpotions for you. Unfortunately i took an arrow to the knee, so i cant fight them."
            , false, false));
        firstQuestEvents.Add(new QuestEvent(4, "Troll Invasion", "We should never have build this farm on Troll territory. The Trolls have taken their lands back. We need your help to reclaim the area, there are 7 trolls on these lands, we need your help to get rid of them."
            , false, false));
        firstQuestEvents.Add(new QuestEvent(5, "Kill Troll General Hal'choka", "Good Job! You've killed all the trolls, the lands are finally ours again... Wait, do you hear that?"
            , false, false));
        firstQuestEvents.Add(new QuestEvent(6, "Wrath of the Dragon", "Hal'choka! I thought he was dead! Theres no way you can beat him. Take these scrolls, they will teach you the secret abilities War Cry and Wrath of the Dragon. Use them wisely, if you combine them they are very powerful"
            , false, false));
        firstQuestEvents.Add(new QuestEvent(7, "Under Attack", "The city is under attack! Help the Elf Mage guards defend the city against the troll invasion"
            , false, false));


        quests.Add(new Quest(1, "Welcome to Dystopia",
            "Welcome to the land of Dystopia. " +
            "You have been ordered to these treacherous lands by the ruler of Dystopia, Chichwa. " +
            "You are highly skilled mercenary from the Golden Company, " +
            "and Chichwa has made sure the Golden Company is compensated  " +
            "plentiful for your life. The nation of Dystopia is in a dire situation, " +
            "and you are Chichwa's last hope to save this once great Kingdom." +
            "Your first task will be to locate the Kingdom and the King, " +
            "which is found beyond the Blossom Valley.",
            true, false, firstQuestEvents
            ));

        GetComponent<QuestHandler>().enabled = true;
    }

    
}
