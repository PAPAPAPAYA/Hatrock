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
    public TextMeshProUGUI match;
    public TextMeshProUGUI argyi;
    public TextMeshProUGUI nail;
    public TextMeshProUGUI tear;
    public TextMeshProUGUI cotton;
    
    public List<GameObject> possibleCombinations;

    public void SearchRecipeForMats(List<GameObject> choosenMats)
    {
        for(int i = 0; i < recipeList.Count; i++)
        {
            if(CompareList(i, choosenMats))
            {
                player.SendMessage("ChangeSpell", recipeList[i].Outcome);
                instruction.text = "";
                /*instruction.text = "Selected Materials: ";
                foreach (var mat in recipeList[i].Materials)
                {
                    instruction.text += mat.name + "\n";
                }
                */
                instruction.text += "Outcome:\n" + recipeList[i].Outcome.name;
                return;
            }
            if(i == recipeList.Count - 1)
            {
                player.SendMessage("RefreshChoosenMats");
                if(choosenMats[choosenMats.Count - 1].GetComponent<MatScript>().matCastType == SpellCtrlScript.CastType.none)
                    instruction.text = /*"Selected Materials: " + choosenMats[choosenMats.Count - 1].name + */"Outcome:\nNone";
                else
                {
                    instruction.text = /*"Selected Materials: " + choosenMats[choosenMats.Count - 1].name + */"Outcome:\n" + choosenMats[choosenMats.Count - 1].name;
                }
                player.SendMessage("ChangeSpell", choosenMats[choosenMats.Count - 1]);
            }
        }
    }

    public void SearchForCombinations(List<GameObject> matList)
    {
        possibleCombinations.Clear();
        match.color = new Color32(255, 255, 255, 255);
        argyi.color = new Color32(255, 255, 255, 255);
        nail.color = new Color32(255, 255, 255, 255);
        tear.color = new Color32(255, 255, 255, 255);
        cotton.color = new Color32(255, 255, 255, 255);
        //combination.text = "Possible Combination:\n";
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
                        if (matList[d].name == "Match - Low Damage Bullet")
                            match.color = new Color32(87, 212, 197, 255);
                        if (matList[d].name == "Argyi - Self Healing")
                            argyi.color = new Color32(87, 212, 197, 255);
                        if (matList[d].name == "Copper Nail - Drop Material Bullet")
                            nail.color = new Color32(87, 212, 197, 255);
                        if (matList[d].name == "Demon May Cry")
                            tear.color = new Color32(87, 212, 197, 255);
                        if (matList[d].name == "Demon Mat B")
                            cotton.color = new Color32(87, 212, 197, 255);
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
            //combination.text += mat.name + "\n";
            if (mat.name == "Match - Low Damage Bullet")
                match.color = new Color32(215, 140, 90, 255);
            if (mat.name == "Argyi - Self Healing")
                argyi.color = new Color32(215, 140, 90, 255);
            if (mat.name == "Copper Nail - Drop Material Bullet")
                nail.color = new Color32(215, 140, 90, 255);
            if (mat.name == "Demon May Cry")
                tear.color = new Color32(215, 140, 90, 255);
            if (mat.name == "Demon Mat B")
                cotton.color = new Color32(215, 140, 90, 255);
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
