using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z_utilites.ProbabilitySystem
{
    public static class Probability
    {
        #region Basic Probability check

        //this is the basic Probability function that will take the item chance is 30% and then check if you'll get or not

        public static bool ProbabilityCheck(int itemProbability)
        {
            float rnd = Random.Range(1, 101);
            if (rnd <= itemProbability)
                return true;
            else
                return false;
        }
        #endregion

        #region Normal Probability Num In Num

        //This Function will return true if the item is lucky enough to be picked by a small chance of one in something
        //and 100% chance to get the item if its 1 in 1 ... XD
        public static bool OneInProbability(int In)
        {
            float rnd = Random.Range(1, In + 1);
            if (rnd <= 1)
                return true;
            else
                return false;
        }

        //This Function will return true if the item is lucky enough to be picked by a small chance of num in something
        //This can be used instead the first one without passing the second variable
        public static bool ChanceInProbability(int In, int chance = 1)
        {
            float rnd = Random.Range(1, In + 1);
            if (rnd <= chance)
                return true;
            else
                return false;
        }
        #endregion
    }
}