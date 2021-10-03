using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecipeManagerScript : MonoBehaviour
{
    public List<Recipe> recipeList;
    public GameObject player;
    public TextMeshProUGUI instruction;
    public TextMeshProUGUI combination;

    public List<GameObject> possibleCombinations;

    public void SearchRecipeForMats(List<GameObject> choosenMats)
    {
        for(int i = 0; i < recipeList.Count; i++)
        {
            if(CompareList(i, choosenMats))
            {
                player.SendMessage("ChangeSpell", recipeList[i].Outcome);
                instruction.text = "Selected Materials: ";
                foreach (var mat in recipeList[i].Materials)
                {
                    instruction.text += mat.name + "\n";
                }
                instruction.text += "\nOutcome:\n" + recipeList[i].Outcome.name;
                return;
            }
            if(i == recipeList.Count - 1)
            {
                player.SendMessage("RefreshChoosenMats");
                if(choosenMats[choosenMats.Count - 1].GetComponent<MatScript>().matCastType == SpellCtrlScript.CastType.none)
                    instruction.text = "Selected Materials: " + choosenMats[choosenMats.Count - 1].name + "\nOutcome:\nNone";
                else
                    instruction.text = "Selected Materials: " + choosenMats[choosenMats.Count - 1].name + "\nOutcome:\n" + choosenMats[choosenMats.Count - 1].name;
            }
        }
    }

    public void SearchForCombinations(List<GameObject> matList)
    {
        possibleCombinations.Clear();
        combination.text = "Possible Combination:\n";
        for (int i = 0; i < recipeList.Count; i++) 
        {
            if (ContainsList(i, matList))
            {
                for (int c = 0; c < recipeList[i].Materials.Count; c++)
                {
                    possibleCombinations.Add(recipeList[i].Materials[c]);
                }
                for (int c = 0; c < possibleCombinations.Count; c++)
                {
                    for (int d = 0; d < matList.Count; d++)
                    {
                        if (possibleCombinations[c] == matList[d])
                        {
                            possibleCombinations.RemoveAt(c);
                        }
                    }
                }
            }
        }
        foreach (var mat in possibleCombinations)
        {
            combination.text += mat.name + "\n";
        }
    }
    

    private bool ContainsList(int i, List<GameObject> matList)
    {
        for (int a = 0; a < matList.Count; a++)
        {
            if (!recipeList[i].Materials.Contains(matList[a]))
            {
                return false;
            }
        }
        return true;
    }

    private bool CompareList(int i, List<GameObject> matList)
    {
        if (recipeList[i].Materials.Count != matList.Count)
        {
            return false;
        }
        recipeList[i].Materials.Sort(CompareSort);
        matList.Sort(CompareSort);
        for(int b = 0; b < recipeList[i].Materials.Count; b++)
        {
            if (recipeList[i].Materials[b] != matList[b])
                return false;
        }
        return true;
    }

    private int CompareSort(GameObject x, GameObject y)
    {
        return x.name.CompareTo(y.name);
    }


}
