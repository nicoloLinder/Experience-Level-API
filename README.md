# Experience-Level-API

A simple API to manage the experience points and the level of any number of characters.

###API Features:
* API consumer (game designer) can inject any experience-level conversion formula(s) into the module
* get current level
* get experience value for an arbitrary level value
* get level value for an arbitrary experience value
* get experience needed until next level-up
* get percentual experience progress between last and next level-up
* get experience delta between current experience and an arbitrary level value
* add / subtract experience
* set level
* reset experience to last level-up

##Usage

To start using the API you have to create a new object of type `Character` which require an object of type `ExperienceLevelFormula` and a name (Two examples are included `LinearExperienceLevelFormula` and `SquareExperienceLevelFormula`)
``` C#
var epxerienceLeveFormula = new SquareExperienceLevelFormula()
var characte = new ("name", experienceLevelFormula)
```
