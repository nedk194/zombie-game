using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class highScores : MonoBehaviour
{

    public void addScore(int score, string name){ // adds score and username to scores file
        if (score > 500){ // prevents low scores being added, as they wont be seen 
            string path = Application.dataPath + "/scores.txt"; // path is the location of the file, application.datapath puts file into the game assets folder
            string text = "\n"+name +"\n"+score.ToString(); // adds score and username on different lines
            if (!File.Exists(path)){ // if the filename given by path exists then 
                File.WriteAllText(path, text);  // write text in path
            }
            File.AppendAllText(path, text); } // if it doenst exist, create it and add all text to it 
    }


    public int[] savedScores;  // this is the array of integers
    public Dictionary<string, string> lBoardTable; // this dictionary creates a link between score and username, so after scores are sorted, the corresponding username is remebered
    public void retrieveData(){ // called to sort the file's data into the two arrays and dictionary
        var t = new StreamReader(Application.dataPath + "/scores.txt"); // opens up scores.txt in read mode
        string textFile = t.ReadToEnd(); // textFile = all text on the scores file
        t.Close(); // close the file

        string[] lines = textFile.Split(    "\n"[0]    ); // splits the large string into an array of small strings by seperating by every new line
        lBoardTable = new Dictionary<string, string>(); // create the dictionary where the key and value are both strings
        lBoardTable.Clear(); // make sure it is clear
        
        savedScores = new int[lines.Length / 2]; // sets the length of saved scores to half the length of lines
        int x = 0; // counter variable 
        for (int i = 0; i < lines.Length ; i +=2) // for half the length of lines, jumping i in 2s
        {
            string scoreToBeAdded = lines[i+1]; // score to add is lines[i+1]

            while (lBoardTable.ContainsKey(scoreToBeAdded)   ){ // checks if the score to add is unique
                scoreToBeAdded = "0" + scoreToBeAdded;  } // adds 0 to the string untill it is unique
            
            lBoardTable.Add(scoreToBeAdded, lines[i]); // add scor to be added as key and the line before hand as its value
            
            
            if (lines[i+1] != ""){ // checks if score isnt 0 
            savedScores[x] = int.Parse(lines[i+1]); // adds lines[i+1] as an integer value to savedScores
            x ++;} // iterate counter variable
        }
    }
    private int[] mergeSort(int[] array){
        int[] left; // left array
        int[] right; // right array
        int[] output = new int[array.Length]; // list that will be outputted
        if (array.Length <= 1){
            return array; // base case for when left/right have been splitted down to 1  
        }
        int mid = array.Length / 2; // find midpoint of array
        left = new int[mid]; // left array holds from 0 to midpoint of input array

        if (array.Length % 2 == 0){ // if input array lenght is an even number then 
            right = new int[mid];  //right and left array's are equal in size
        }
        else{
            right = new int[mid+1]; // if odd then right is 1 bigger
        }
        for (int i = 0; i < mid; i++){ // fill left array with first half of input array
            left[i] = array[i];
        }
        int j =0; // j is counter variable as i starts halfway through input array
        for (int i = mid; i < array.Length; i++){ // fill right wih second half of array
            right[j] = array[i];
            j +=1;
        }
        left = mergeSort(left); // recursivly mergesort the left half
        right = mergeSort(right); // recursuvly mergesort the right half

        output = merge(left, right); // code only reaches here after the array inputted is <=0
        return output; // returns two arrays merged into one
    }
    private int[] merge(int[] left, int[] right){ // takes in two arrays 
        int[] result = new int[left.Length + right.Length]; // creates the list to outputted, of size equal to both lists combined 

        int leftIndex = 0, rightIndex = 0, resultIndex = 0; // sets all index variables to 0
        // these index variables act as a counter through the arrays to keep track of which item of the array is being looked at
        while (leftIndex < left.Length || rightIndex < right.Length){ // while there are items left in either array
            if (leftIndex < left.Length && rightIndex < right.Length){ // if there are items in both arrays, meaning they must be compared with eachother
                if (left[leftIndex] >= right[rightIndex]){ // if next item in left is bigger than the next in right, add next in left to result array
                    result[resultIndex] = left[leftIndex]; // in the next position 
                    resultIndex +=1; // +1 to result index so next index position will be changed
                    leftIndex +=1; // increment the left index as the previous index is already in result
                }
                else{ // if not then right must be bigger so
                    result[resultIndex] = right[rightIndex]; // add right to result
                    resultIndex +=1; // increment both right and result index
                    rightIndex +=1;
                }
            }
            else{ // if only one list still holds values
                if(leftIndex < left.Length){ // if it is left that still holds values
                    result[resultIndex] = left[leftIndex]; // add all lefts values into result
                    resultIndex +=1; // increment index variables 
                    leftIndex +=1;
                }
                if(rightIndex < right.Length){ // if it is right that holds values
                    result[resultIndex] = right[rightIndex]; // add all of rights values to result
                    resultIndex +=1; // increment index variables
                    rightIndex +=1;
                }
            }
        }
        return result; // return result, outside of while loop so only reaches here when neither left/right have values to consider
    }

    public lBoardTextController userNamesTable; // used to edit the text of lBoardText
    public lBoardTextController scoresTable; // same for scores

    public void getScores(){ 
        retrieveData();  // calls retreive data to update the arrays 
        string scoresString = ""; // the string to display in scoresTable
        string usernameString = ""; // the string to bdisplay in lBoardText
        foreach (int score in mergeSort(savedScores) )  // calls mergesort passing savedScores, iterates loop for every item in it
        {
            scoresString += score + "\n"; // add score and start new line
            usernameString += lBoardTable[score.ToString()] + "\n" ; //  add corresponding username and new line
        }
        userNamesTable.setText(usernameString); // set text of both text objects
        scoresTable.setText(scoresString);        
        
    }
}
