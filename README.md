# PolyRents
Resource checkout system for the OCOB helpdesk.
## Diagrams
### Class Diagram
![Alt text](docs/ClassDiagram.png?raw=true "Class Diagram")

### Component Diagram
![Alt text](docs/ComponentDiagram.png?raw=true "Component Diagram")

### ER Diagram
![Alt text](docs/PolyRentsERDiagram.png?raw=true "ER Diagram")

## Further Info
### Library Numbers Explained

Beginning in the mid to late 1970s, barcodes began to replace punched cards and manual
checkout slips at many major university and public libraries. The CLSI company of Boston,
Massachusetts, which was later acquired by Geac Computers, of Markham, Ontario, developed
the standard 14-digit Codabar labels currently used in many of these libraries around the world.
The CLSI Standard replaced the Plessey standard, which had been used widely, as well as many
locally-developed schemes.

The label begins with a single digit representing the type of entity being described:
1. special command to the library system software
2. a person (i.e. a library patron, staff member...)
3. an item (i.e. a particular copy of a book, tape, disk...)

Next comes a 4-digit "agency prefix", which represents the library which owns the item, or to which
the patron belongs. For instance, Wayne State's agency code is 9343. The University of Windsor's
is 1862. Different vendors were given different groups of agency numbers to assign to their
customers. Generally speaking, Geac's original customers were given numbers in the nine thousand
range. These unique agency numbers allow the interlending software and the library staff to return
items to their proper locations.

The next eight digits are unique to the item upon which the label has been placed. That is, each
copy of a book, tape, disk, etc. would have its own item number. Sometimes these are assigned
based upon a check of the library's holdings (smart barcoding), and sometimes they are simply
affixed sequentially as items are processed (dumb barcoding of batches of items in the stacks,
conversion on-the-fly when unlabeled items are checked out for the first time).

Finally, the last digit is a "modulus 10 checksum". It is derived from the previous 13 digits by
means of a mathematical formula. The barcode input software re-calculates this checksum each
time the label is wanded, and compares it to the checksum printed on the label. If a barcode wand
or laser scanner errs in reading the label, or if someone accidentally jumbles up the numbers when
typing them, then the two numbers won't match, and the system will beep and ask the user to
re-input the label.

### Check Digit Algorithm explained
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
