Welcome! #speaker:MamangKasbor #image:Cat_Neutral
-> main
=== main ===
What do you want?
+ [I want fritters] 
    -> choices_1
+ [Just want to chat]
    -> choices_2
=== choices_1 ===
Fresh from the fryer! Bakwan or tempeh? #speaker:MamangKasbor #image:Cat_Smile
+ [Bakwan]
    -> choices_1_bakwan
+ [Tempeh]
    -> choices_1_tempeh
=== choices_1_bakwan ===
One bakwan! Want chili sauce? #speaker:MamangKasbor #image:Cat_Smile
+ [Yes]
    Here you go, careful, it's spicy! #speaker:MamangKasbor #image:Cat_Happy
    -> END
+ [No]
    Alright, enjoy! #speaker:MamangKasbor #image:Cat_Neutral
    -> END
=== choices_1_tempeh ===
Tempeh! One or two? #speaker:MamangKasbor #image:Cat_Smile
+ [One]
    Here's your tempeh! #speaker:MamangKasbor #image:Cat_Neutral
    -> END
+ [Two]
    Two tempeh, enjoy! #speaker:MamangKasbor #image:Cat_Smile
    -> END
=== choices_2 ===
Oh, it’s quiet today. Want to talk about school or work? #speaker:MamangKasbor #image:Cat_Neutral
+ [School]
    School is tough, but you can do it! #speaker:MamangKasbor #image:Cat_Smile
    -> END
+ [Work]
    Work can be tiring, but keep going! #speaker:MamangKasbor #image:Cat_Smile
    -> END
+ [Never mind]
    Alright, come by again anytime! #speaker:MamangKasbor #image:Cat_Neutral
    -> END
