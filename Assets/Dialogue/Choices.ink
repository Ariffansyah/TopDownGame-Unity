VAR name = ""

{ name == "": -> main | -> already_chose }

=== main ===
Choose your fighter?
    + [Sumanto]
        -> chosen("Sumanto")
    + [Ambatukam]
        -> chosen("Ambatukam")
    + [Rusdi]
        -> chosen("Rusdi")
        
=== chosen(fighter) ===
~ name = fighter
You chose {fighter}!
-> END

=== already_chose ===
You already chose {name}!
-> END