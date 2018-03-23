# PolyRents
Resource checkout system for the OCOB helpdesk.

## Class Diagram
![Alt text](docs/ClassDiagram.png?raw=true "Class Diagram")

## Component Diagram
![Alt text](docs/ComponentDiagram.png?raw=true "Component Diagram")

## ER Diagram
![Alt text](docs/PolyRentsERDiagram.png?raw=true "ER Diagram")

## Check Digit Algorithm explained

<img src="docs/Mod10Opt1.jpg" width="595" height="770"/>

```java
public int calculateCheckDigit()
{
    int sum = 0;

    int stepNum = 0;

    for (int i = 0; i < libNumber.Length; i++)
    {
        stepNum = int.Parse(libNumber.Substring(i, 1));
        
        //Odd position work
        if (i % 2 == 0)
        {
            stepNum = stepNum * 2;

            // add the digits together if it is greater than 9
            stepNum = stepNum > 9 ? stepNum / 10 + stepNum % 10 : stepNum;
        }

        sum += stepNum;
    }

    return 10 - sum % 10;
}
```
