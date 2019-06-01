# Experience-Level-API

A simple API to manage the experience points and the level of an arbitrary number of characters.

### API Features:

* API consumer (game designer) can inject any experience-level conversion formula(s) into the module
* get current level
* set level
* get experience value for an arbitrary level value
* get level value for an arbitrary experience value
* get experience needed until next level-up
* get percentual experience progress between last and next level-up
* get experience delta between current experience and an arbitrary level value
* add / subtract experience
* reset experience to last level-up

## Installation

To install API in unity simply import the [Experience_Level_API_Package](/Experience_Level_API_Package.unitypackage), or import the [Experience_Level_API_Implementation](/Experience_Level_API_Implementation.unitypackage) to look at a possible implementation of the Experience-Level_API.

## Usage

To start using the API you have to create a new object of type `Character` which require an object of type `ExperienceLevelFormula` and a name.
Two example `ExperienceLevelFormula` implementations are included: 
- `LinearExperienceLevelFormula` 
- `SquareExperienceLevelFormula`

``` C#
using ExperienceSystem;
using CharacterSystem; 

ExperienceLevelFormula epxerienceLeveFormula = new SquareExperienceLevelFormula();
Character character = new Character("name", experienceLevelFormula); 
string characterID = character.ID;
```

Once a character is created the `ExperienceAPI` can be used to manage the experience and the level of the newly created character, all that is really needed is the character `ID` to reference the character that was just created

##### Get and set character level

Get and set a character's level value by passing the characterID

``` C# 
ExperienceAPI.GetLevel(characterID);
ExperienceAPI.SetLevel(characterID, level);
```

#### Calculate the experience for an arbitrary level value

Calculate the experience amount for an arbitrary level value, this can be done by passing a character's experience level conversion formula or by passing an experience level conversion formula independent of any character

``` C# 
ExperienceAPI.CalculateExperience(characterID, level);
ExperienceAPI.CalculateExperience(character, level);
ExperienceAPI.CalculateExperience(epxerienceLeveFormula, level);
```

##### Calculate the level for an arbitrary experience amount

Calculate the level value amount for an arbitrary experience amount, this can be done by passing a character's experience level conversion formula or by passing an experience level conversion formula independent of any character

``` C# 
ExperienceAPI.CalculateLevel(characterID, experience);
ExperienceAPI.CalculateLevel(character, experience);
ExperienceAPI.CalculateLevel(epxerienceLeveFormula, experience);
```

##### Calculate the remaining experience until next level-up

Calculate the remaining experience until next level-up, this can be done by passing a character's experience level conversion formula and it's current experience amount or by passing an experience level conversion formula and the current experience amount you want to derive the remaining quantity from

``` C# 
ExperienceAPI.CalculateRemainingExperience(characterID);
ExperienceAPI.CalculateRemainingExperience(character);
ExperienceAPI.CalculateRemainingExperience(experienceLevelFormula, currentExperience);
```

##### Calculate the percentual experience progress until next level-up

Calculate the percentual experience progress until next level-up, this can be done by passing a character's experience level conversion formula and it's current experience amount or by passing an experience level conversion formula and the current experience amount you want to derive the experience progress from

``` C# 
ExperienceAPI.CalculateProgress(characterID);
ExperienceAPI.CalculateProgress(character);
ExperienceAPI.CalculateProgress(experienceLevelFormula, currentExperience);
```

##### Calculate the experience delta between the current experience and an arbitrary level value

Calculate the experience delta between the current experience and an arbitrary level value, this can be done by passing a character's experience level conversion formula or by passing an experience level conversion formula
``` C#
ExperienceAPI.CalculateExperienceDelta(characterID, level);
ExperienceAPI.CalculateExperienceDelta(character, level);
ExperienceAPI.CalculateExperienceDelta(characterID, currentExperience, level);
```

##### Add / Subtract / Change the the experience amount of a character
``` C#
ExperienceAPI.AddExperience(characterID, experienceToAdd);
ExperienceAPI.AddExperience(character, experienceToAdd);

ExperienceAPI.SubreactExperience(characterID, experienceToSubtract);
ExperienceAPI.SubreactExperience(character, experienceToSubtract);

ExperienceAPI.ChangeExperience(characterID, experienceChange);
ExperienceAPI.ChangeExperience(character, experienceChange);
```

##### Reset a character experience to the last level-up or an arbitrary level
``` C#
ExperieceAPI.ResetExperience(characterID);
ExperieceAPI.ResetExperience(character);
ExperieceAPI.ResetExperience(characterID, level);
ExperieceAPI.ResetExperience(character, level);
```
