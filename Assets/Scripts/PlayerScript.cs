﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	static public PlayerScript me;
	private Animator anim;
	public float spd;
	public float rot_spd;
	public float hp;
	public GameObject currentMat;
	public List<InventoryDict> requiredMats;
	public GameObject recipeManager;
	private GameObject selectedMat;
	public GameObject tear;
	public GameObject cotton;
	
	//Temp inventory
	[Header("Temp Inventory")]
	public List<InventoryDict> tempInventory;
	public List<GameObject> choosentMats;

	private void Awake()
	{
		me = this;
		anim = GetComponent<Animator>();
	}

	private void Update()
	{
		// simple movement for now
		//if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
		//{
		//	transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - spd * Time.deltaTime);
		//}
		//else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
		//{
		//	transform.position = new Vector3(transform.position.x - spd * Time.deltaTime, transform.position.y, transform.position.z);
		//}
		//else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
		//{
		//	transform.position = new Vector3(transform.position.x + spd * Time.deltaTime, transform.position.y, transform.position.z);
		//}
		//else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
		//{
		//	transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + spd * Time.deltaTime);
		//}
		//else if (Input.GetKey(KeyCode.W))
		//{
		//	transform.position = new Vector3(transform.position.x - Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime, transform.position.y, transform.position.z - Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime);
		//}
		//else if (Input.GetKey(KeyCode.S))
		//{
		//	transform.position = new Vector3(transform.position.x + Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime, transform.position.y, transform.position.z + Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime);
		//}
		//else if (Input.GetKey(KeyCode.A))
		//{
		//	transform.position = new Vector3(transform.position.x + Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime, transform.position.y, transform.position.z - Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime);
		//}
		//else if (Input.GetKey(KeyCode.D))
		//{
		//	transform.position = new Vector3(transform.position.x - Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime, transform.position.y, transform.position.z + Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime);
		//}
		//if (anim.GetCurrentAnimatorStateInfo(0).IsName("testIdle"))
		{
			if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) &&
				anim.GetCurrentAnimatorStateInfo(0).IsName("testIdle"))
			{
				anim.Play("testWalk");
			}
			if (anim.GetCurrentAnimatorStateInfo(0).IsName("testWalk"))
			{
				if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
				{
					transform.position = new Vector3(transform.position.x - Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime, transform.position.y, transform.position.z + Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime);
				}
				else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
				{
					transform.position = new Vector3(transform.position.x + Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime, transform.position.y, transform.position.z + Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime);
				}
				else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
				{
					transform.position = new Vector3(transform.position.x - Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime, transform.position.y, transform.position.z - Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime);
				}
				else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
				{
					transform.position = new Vector3(transform.position.x + Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime, transform.position.y, transform.position.z - Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime);
				}
				else if (Input.GetKey(KeyCode.W))
				{
					transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + spd * Time.deltaTime);
				}
				else if (Input.GetKey(KeyCode.S))
				{
					transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - spd * Time.deltaTime);
				}
				else if (Input.GetKey(KeyCode.A))
				{
					transform.position = new Vector3(transform.position.x - spd * Time.deltaTime, transform.position.y, transform.position.z);
				}
				else if (Input.GetKey(KeyCode.D))
				{
					transform.position = new Vector3(transform.position.x + spd * Time.deltaTime, transform.position.y, transform.position.z);
				}
				else
				{
					anim.Play("testIdle");
				}
			}
			
		}

		// look at mouse pos(not changing y-axis)
		//transform.LookAt(new Vector3(MouseManager.me.mousePos.x, transform.position.y, MouseManager.me.mousePos.z));
		var target = new Vector3(MouseManager.me.mousePos.x, transform.position.y, MouseManager.me.mousePos.z);
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target - transform.position), rot_spd * Time.deltaTime);

		// select mat
		if (Input.GetKeyDown(KeyCode.Alpha1))
        {
			if (tempInventory[0].matAmounts > 0)
            {
				choosentMats.Add(tempInventory[0].Mats);
				selectedMat = tempInventory[0].Mats;
				recipeManager.SendMessage("SearchRecipeForMats", choosentMats);
				recipeManager.SendMessage("SearchForCombinations", choosentMats);
			}

        }
		else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
			if (tempInventory[1].matAmounts > 0)
            {
				choosentMats.Add(tempInventory[1].Mats);
				selectedMat = tempInventory[1].Mats;
				recipeManager.SendMessage("SearchRecipeForMats", choosentMats);
				recipeManager.SendMessage("SearchForCombinations", choosentMats);
			}
        }
		else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
			if (tempInventory[2].matAmounts > 0)
            {
				choosentMats.Add(tempInventory[2].Mats);
				selectedMat = tempInventory[2].Mats;
				recipeManager.SendMessage("SearchRecipeForMats", choosentMats);
				recipeManager.SendMessage("SearchForCombinations", choosentMats);
			}
        }
		else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
			if(tempInventory[3].matAmounts > 0)
            {
				choosentMats.Add(tempInventory[3].Mats);
				selectedMat = tempInventory[3].Mats;
				recipeManager.SendMessage("SearchRecipeForMats", choosentMats);
				recipeManager.SendMessage("SearchForCombinations", choosentMats);
			}
        }
		else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
			if(tempInventory[4].matAmounts > 0)
            {
				choosentMats.Add(tempInventory[4].Mats);
				selectedMat = tempInventory[4].Mats;
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

	public void ChangeSpell(Recipe recipe)
    {
		currentMat = recipe.Outcome;
		Debug.Log(currentMat.name);
		requiredMats.Clear();
		foreach(var mat in recipe.materials)
        {
			requiredMats.Add(mat);
        }
    }

	public void RefreshSpell(GameObject outcome)
    {
		currentMat = outcome;
		Debug.Log(currentMat.name);
		requiredMats.Clear();
		InventoryDict inDic = new InventoryDict();
		inDic.Mats = outcome;
		inDic.matAmounts = 1;
		requiredMats.Add(inDic);
    }

    //拾取素材
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "DroppedMat" && tempInventory[3].matAmounts == 0)
        {
			InventoryDict droppedMat = new InventoryDict();

			//这是用魔法写的，必须要改，但想不到咋办
			if (other.gameObject.name.ToString() == "Tear(Clone)")
				droppedMat.Mats = tear;
			else if (other.gameObject.name.ToString() == "Cotton(Clone)")
				droppedMat.Mats = cotton;

			recipeManager.GetComponent<RecipeManagerScript>().bossMat.text = "4:" + droppedMat.Mats.name.ToString();
			droppedMat.matAmounts = 1;

			tempInventory.RemoveAt(3);
			tempInventory.Add(droppedMat);

			Destroy(other.gameObject);
        }
    }

	public bool ConsumeMats()
    {
		List<GameObject> inventoryList = new List<GameObject>();
		foreach(var mat in tempInventory)
        {
			inventoryList.Add(mat.Mats);
        }
		foreach(var mat in requiredMats)
        {
			for(int i = 0; i < tempInventory.Count; i++)
            {
				if(mat.Mats == tempInventory[i].Mats)
                {
					if(tempInventory[i].matAmounts < mat.matAmounts)
                    {
						return false;
					}
                }
            }
            if (!inventoryList.Contains(mat.Mats))
            {
				return false;
            }
        }
		foreach (var mat in requiredMats)
		{
			for (int i = 0; i < tempInventory.Count; i++)
			{
				if (mat.Mats == tempInventory[i].Mats)
				{
					if (tempInventory[i].matAmounts >= mat.matAmounts)
					{
						tempInventory[i].matAmounts -= mat.matAmounts;

						if(i == 3)
                        {
							choosentMats.Remove(tempInventory[i].Mats);
							tempInventory[i].Mats = null;
							recipeManager.GetComponent<RecipeManagerScript>().bossMat.text = "4:None";
							//recipeManager.SendMessage("SearchRecipeForMats", choosentMats);
							recipeManager.SendMessage("SearchForCombinations", choosentMats);
                        }
					}
				}
			}
		}
		return true;
    }
}