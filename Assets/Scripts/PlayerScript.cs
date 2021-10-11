using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	static public PlayerScript me;
	public float spd;
	public float hp;
	public GameObject currentMat;
	public GameObject recipeManager;
	private GameObject selectedMat;
	//Temp inventory
	[Header("Temp Inventory")]
	public List<GameObject> tempInventory;
	public List<GameObject> choosentMats;

	private void Awake()
	{
		me = this;
	}

	private void Update()
	{
		// simple movement for now
		if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - spd * Time.deltaTime);
		}
		else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
		{
			transform.position = new Vector3(transform.position.x - spd * Time.deltaTime, transform.position.y, transform.position.z);
		}
		else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
		{
			transform.position = new Vector3(transform.position.x + spd * Time.deltaTime, transform.position.y, transform.position.z);
		}
		else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + spd * Time.deltaTime);
		}
		else if (Input.GetKey(KeyCode.W))
		{
			transform.position = new Vector3(transform.position.x - Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime, transform.position.y, transform.position.z - Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime);
		}
		else if (Input.GetKey(KeyCode.S))
		{
			transform.position = new Vector3(transform.position.x + Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime, transform.position.y, transform.position.z + Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime);
		}
		else if (Input.GetKey(KeyCode.A))
		{
			transform.position = new Vector3(transform.position.x + Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime, transform.position.y, transform.position.z - Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime);
		}
		else if (Input.GetKey(KeyCode.D))
		{
			transform.position = new Vector3(transform.position.x - Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime, transform.position.y, transform.position.z + Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime);
		}
		

		// look at mouse pos(not changing y-axis)
		transform.LookAt(new Vector3(MouseManager.me.mousePos.x, transform.position.y, MouseManager.me.mousePos.z));

		// select mat
		if (Input.GetKeyDown(KeyCode.Z))
        {
			if (tempInventory[0] != null)
            {
				choosentMats.Add(tempInventory[0]);
				selectedMat = tempInventory[0];
				recipeManager.SendMessage("SearchRecipeForMats", choosentMats);
				recipeManager.SendMessage("SearchForCombinations", choosentMats);
			}

        }
		else if (Input.GetKeyDown(KeyCode.X))
        {
			if (tempInventory[1] != null)
            {
				choosentMats.Add(tempInventory[1]);
				selectedMat = tempInventory[1];
				recipeManager.SendMessage("SearchRecipeForMats", choosentMats);
				recipeManager.SendMessage("SearchForCombinations", choosentMats);
			}
        }
		else if (Input.GetKeyDown(KeyCode.C))
        {
			if (tempInventory[2] != null)
            {
				choosentMats.Add(tempInventory[2]);
				selectedMat = tempInventory[2];
				recipeManager.SendMessage("SearchRecipeForMats", choosentMats);
				recipeManager.SendMessage("SearchForCombinations", choosentMats);
			}
        }
		else if (Input.GetKeyDown(KeyCode.V))
        {
			if(tempInventory[3] != null)
            {
				choosentMats.Add(tempInventory[3]);
				selectedMat = tempInventory[3];
				recipeManager.SendMessage("SearchRecipeForMats", choosentMats);
				recipeManager.SendMessage("SearchForCombinations", choosentMats);
			}
        }
		else if (Input.GetKeyDown(KeyCode.B))
        {
			if(tempInventory[4] != null)
            {
				choosentMats.Add(tempInventory[4]);
				selectedMat = tempInventory[4];
				recipeManager.SendMessage("SearchRecipeForMats", choosentMats);
				recipeManager.SendMessage("SearchForCombinations", choosentMats);
			}
        }
	}

	public void RefreshChoosenMats()
    {
		choosentMats.Clear();
		choosentMats.Add(selectedMat);
    }

	public void ChangeSpell(GameObject outcome)
    {
		currentMat = outcome;
		Debug.Log(currentMat.name);
    }
}