using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z_utilites.Enumerates
{
    public enum FoodCookedState
	{
		raw, medium, cooked, mediumBurned, burned
	}

	public enum FoodType
	{
		lowerBread, upperBread, burguerPatty, bacon, cheese, // no vegan
		tomatoSlice, onionSlice, lettuce, fries,// vegan
		friesPacket, plate // No eatible
	}

	public enum FoodTypeOnlyNormalFood
	{
		lowerBread, upperBread, burguerPatty, bacon, cheese, // no vegan
		tomatoSlice, onionSlice, lettuce, fries,// vegan		
	}
}