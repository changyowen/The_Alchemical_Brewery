using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DistillRefineStationInteraction : MonoBehaviour
{
    public DistillInformationHandler informationHandler;

    void OnMouseDown()
    {
        //check if not mouse over UI
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            //if player still far from pot
            float dist = Vector3.Distance(PlayerInfoHandler.Instance.playerPosition, transform.position);
            if (dist > 5f)
            {
                //go to the nearest point toward collider
                Collider col = GetComponent<Collider>();
                Vector3 closestPoint = col.ClosestPoint(PlayerInfoHandler.Instance.playerPosition);
                Vector3 _adjustedNewDest = new Vector3(closestPoint.x, transform.position.y, closestPoint.z);
                RayCastMovement.Instance.NewDestination(_adjustedNewDest);
            }
            else //if player already near the pot
            {
                //stop moving
                RayCastMovement.Instance.NewDestination(PlayerInfoHandler.Instance.playerPosition);
                //Start interaction
                DistillStandInteraction();
            }
        }
    }

    void DistillStandInteraction()
    {
        //if it is not boiling
        if (!informationHandler.isBoiling)
        {
            //if pot is holding ingredient
            if (informationHandler.holderIngredient != -1)
            {
                //if player ingredient pocket not full
                if (PlayerInfoHandler.Instance.playerIngredientHolder.Count < 4)
                {
                    //take ingredient
                    TakeRefinedIngredient();
                }
            }
            else //if it not holding any ingredient
            {
                //if player holding ingredient
                if (PlayerInfoHandler.Instance.playerIngredientHolder.Count != 0)
                {
                    //put ingredient into refine
                    PutIngredient();
                }
            }
        }
        else //if pot is boiling
        {

        }
    }

    void TakeRefinedIngredient()
    {
        int _ingredientIndex = informationHandler.holderIngredient;
        PlayerInfoHandler.Instance.playerIngredientHolder.Add(_ingredientIndex);
        informationHandler.holderIngredient = -1;
    }

    void PutIngredient()
    {
        int _putIngredientIndex = PlayerInfoHandler.Instance.playerIngredientHolder[0];
        PlayerInfoHandler.Instance.playerIngredientHolder.RemoveAt(0);

        float _boilingTime = 3f;
        StartCoroutine(StartRefine_Extract(_boilingTime, _putIngredientIndex));
    }

    IEnumerator StartRefine_Extract(float _boilingTime, int _putIngredientIndex)
    {
        informationHandler.isBoiling = true;
        //wait for boiling time
        yield return new WaitForSeconds(_boilingTime);
        //complete refine
        informationHandler.holderIngredient = _putIngredientIndex + 40;
        //end boiling
        informationHandler.isBoiling = false;
    }
}
