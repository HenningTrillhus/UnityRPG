how to creat dialog system best way postible


ideas:
JSON, for each NPC:
cons:
1. 

Pros:
1. Seaperate messages for the different npc


who would it look:
<name:first dialog...>
[
    {
        "id": 11,
        "speaker": "NPC",
        "text": "Do you want to help me?",
        "choices": [
            { "text": "Yes, I will help", "next_id": "accept_quest" },
            { "text": "No, sorry", "next_id": "decline_quest" }
        ]
    },
    {
        "id": 12,
        "speaker": "NPC",
        "text": "Thank you! Here is your quest."
    },
    {
        "id": 13,
        "speaker": "NPC",
        "text": "Maybe another time."
    }
]
<name: second dialog...>

toughts on id system: first number (1) == NPC, first npc and so on, 2. number == number of the message in seqence. 


JSON, for all NPC in one:
Cons:
Pros:

{
  "npc1": [
    {
      "id": "intro",
      "speaker": "NPC",
      "text": "Hello traveler!",
      "choices": []
    }
  ],
  "npc2": [
    {
      "id": "intro",
      "speaker": "NPC",
      "text": "Welcome to my shop!",
      "choices": [
        {"text": "Buy items", "next_id": "buy"},
        {"text": "Leave", "next_id": "leave"}
      ]
    }
  ]
}
