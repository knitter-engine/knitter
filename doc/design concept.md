# design concept
- everything is code: Game objects, engines, and everything else are code oriented, not config file.(This may let Roslyn super useful)
- no external string: External strings may cause unknown errors. Unified management of strings can avoid many potential problems
- Interface oriented: The interface makes the responsibilities of each module clear, easy to replace
