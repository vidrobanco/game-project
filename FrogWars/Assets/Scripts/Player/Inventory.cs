using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    /* REFERENCE FOR INVENTORY
     * 
     * Indexes:
     * 0 - Ammo Flies
     * 1 - Fire Flies
     */
    public List<int> fliesInv = new List<int>()
    {
        0, 
        0 
    };

    public int selectFlyNo = 0;

    public bool FliesAvailable()
    {
        if (fliesInv[selectFlyNo] > 0)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Adds the value to the selected
    /// fly counter's value e.g. if the value
    /// is -1 it will decrease it by 1
    /// </summary>
    /// <param name="value"></param>
    void ChangeVal(int value)
    {
        fliesInv[selectFlyNo] += value;
    }

    /// <summary>
    /// Changes the selected fly number from the
    /// fliesInv list.
    /// </summary>
    /// <param name="add"></param>
    void ChangeSelFly(bool add)
    {
        if (add)
        {
            if (selectFlyNo == fliesInv.Count - 1)
            {
                selectFlyNo = 0;
                return;
            }

            selectFlyNo++;
        }
        else
        {
            if (selectFlyNo == 0)
            {
                selectFlyNo = fliesInv.Count - 1;
                return;
            }

            selectFlyNo--;
        }

    }

    void Start()
    {
        print(FliesAvailable());
    }
}
