using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{

	public int Cash { get; set; }
	public int Day { get; set; }
	public float PopulationCurrent { get; set; }
	public float PopulationCeiling { get; set; }
	public int JobsCurrent { get; set; }
	public int JobsCeiling { get; set; }
	public float Food { get; set; }
	public int[] buildingCount = new int[4]; //0 Road; 1 House; 2 Farm; 3 Factory

	public enum buildings
	{
		Road,
		House,
		Farm,
		Factory
	};
	private UIController uiController;
	// Use this for initialization
	void Start ()
	{
		uiController = GetComponent<UIController>();
		Cash = 50;
		uiController.UpdateDayCount();
		uiController.UpdateCityData();
	}

	public void EndTurn()
	{
		Day++;
		CalculateCash();
		CalculateJobs();
		CalculatePopulation();
		CalculateFood();
		uiController.UpdateDayCount();
		uiController.UpdateCityData();
		Debug.Log("Day ended.");
		Debug.LogFormat("Jobs: {0}/{1}, Cash: {2}, Pop: {3}/{4}, Food: {5}", JobsCurrent, JobsCeiling, Cash, PopulationCurrent, PopulationCeiling, Food);
	}

	void CalculateJobs()
	{
		JobsCeiling = buildingCount[(int)buildings.Factory] * 10;
		JobsCurrent = Mathf.Min((int) PopulationCurrent, JobsCeiling);
	}

	void CalculateCash()
	{
		Cash += JobsCurrent * 2;
	}

	public void DepositCash(int deposit)
	{
		Cash += deposit;
	}
	
	void CalculateFood()
	{
		Food += buildingCount[(int) buildings.Farm] * 2.5f;
		Food -= PopulationCurrent * 0.5f;
	}

	void CalculatePopulation()
	{
		PopulationCeiling = buildingCount[(int)buildings.House] * 5;
		if (Food >= PopulationCurrent * .5f && PopulationCurrent < PopulationCeiling)
		{
			//Food -= PopulationCurrent * .25f;
			PopulationCurrent = Mathf.Min(PopulationCurrent += Food * .25f, PopulationCeiling);
		}
		else if (Food < PopulationCurrent * 0.25f)
		{
			PopulationCurrent -= (PopulationCurrent - Food) * .5f;
		}
	}
}
