using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementSkillManager : MonoBehaviour
{
    public static ElementSkillManager Instance { get; private set; }

    public GameObject ignisSkillEffect_obj;
    public GameObject ordoSkillEffect_obj;

    [System.NonSerialized] public bool[] skillActivated = new bool[5] { false, false, false, false, false };

    public float ignisSkillTime = 30f;
    public float aquaSkillMultiplier = 1.5f;
    public float terraSkillSpawnTimeRefresh = 0.2f;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator IgnisSkill()
    {
        skillActivated[0] = true; //activated skill
        StageManager.accelerateGame = true; //accelerate game
        ElementMeterPanel.Instance.elementSkillRemaining[0] -= 1;//minus skill remaining
        //Activate effect camera and post proccessing
        ignisSkillEffect_obj.SetActive(true);

        ///WAIT TILL IGNIS SKILL TIME
        float remainingTime = ignisSkillTime;
        while(remainingTime > 0)
        {
            if (!StageManager.pauseGame)
                remainingTime -= Time.unscaledDeltaTime;
            Debug.Log(remainingTime);
            yield return null; 
        }

        //Disable effect camera and post proccessing
        ignisSkillEffect_obj.SetActive(false);
        StageManager.accelerateGame = false; //stop accelerate game
        skillActivated[0] = false; //deactivate skill
        ElementMeterPanel.Instance.elementMana[0] = 0; //reset mana
    }

    public IEnumerator AquaSkill()
    {
        skillActivated[1] = true;//skill activate

        ///WAIT TILL AQUA SKILL REMAINING FINISHED
        while(ElementMeterPanel.Instance.elementSkillRemaining[1] != 0)
        {
            yield return null;
        }

        skillActivated[1] = false; //deactivate skill
        ElementMeterPanel.Instance.elementMana[1] = 0; //deset element mana
    }

    public IEnumerator TerraSkill()
    {
        skillActivated[2] = true;//skill activate
        ElementMeterPanel.Instance.elementSkillRemaining[2] -= 1;//minus skill remaining

        int totalMagicChest = 0;
        float remainingTime = terraSkillSpawnTimeRefresh;
        while(totalMagicChest < InstantiateAssetHandler.Instance.magicChestObjList.Count)
        {
            //get magic chest shelf interaction script
            GameObject currentShelf = InstantiateAssetHandler.Instance.magicChestObjList[totalMagicChest];
            ShelfInteraction currentShelfInteraction = currentShelf.GetComponent<ShelfInteraction>();
            //spawn ingredient directly
            currentShelfInteraction.DirectIngredientSpawn();

            //stalling coroutine untill terraSkill SpawnTime refresh
            while(remainingTime > 0)
            {
                //add time (if not pause game)
                if (!StageManager.pauseGame)
                    remainingTime -= Time.unscaledDeltaTime;

                yield return null;
            }

            //reset remaining time & total magic chest int
            remainingTime = terraSkillSpawnTimeRefresh;
            totalMagicChest++;
        }

        skillActivated[2] = false; //deactivate skill
        ElementMeterPanel.Instance.elementMana[2] = 0; //reset mana
    }

    public IEnumerator AerSkill()
    {
        skillActivated[3] = true;//skill activate

        ///WAIT TILL AQUA SKILL REMAINING FINISHED
        while (ElementMeterPanel.Instance.elementSkillRemaining[3] != 0)
        {
            yield return null;
        }

        skillActivated[3] = false; //deactivate skill
        ElementMeterPanel.Instance.elementMana[3] = 0; //deset element mana
    }

    public void OrdoSkill()
    {
        RayCastMovement.Instance.rayCastMode = RayCastMovementMode.Teleport; //TELEPORT MODE
        //activate ordoskilleffect
        ordoSkillEffect_obj.SetActive(true);
    }
}
