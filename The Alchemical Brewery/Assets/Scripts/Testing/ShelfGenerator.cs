using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfGenerator : MonoBehaviour
{
    public static ShelfGenerator Instance { get; private set; }

    public GameObject shelf_obj;

    public int unlockedShelf = 10;

    public void ShelfGeneration()
    {
        for (int i = 0; i < unlockedShelf; i++)
        {
            //Instantiate gameobj
            GameObject newShelf = Instantiate(shelf_obj, transform.position, Quaternion.identity) as GameObject;
            //set transform
            newShelf.transform.SetParent(this.transform, false);

            ShelfDataAssign(newShelf, i, 1);
        }
    }

    void ShelfDataAssign(GameObject shelf, int shelfIndex, int ingredientIndex)
    {
        //get shelf's ingredient item handler
        IngredientItemHandler ingredientItemHandler = shelf.GetComponent<IngredientItemHandler>();

        //set shelf index and igredient index;
        ingredientItemHandler.shelfIndex = shelfIndex;
        ingredientItemHandler.ingredientIndex = ingredientIndex;
    }
}
