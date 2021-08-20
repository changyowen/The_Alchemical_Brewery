using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingMono : MonoBehaviour
{
    public List<int> list1;
    public List<int> list2;

    int a = 0;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            PotionData newPotionData = new PotionData();
            newPotionData.potionName = "potion" + a;
            newPotionData.potionFormular = new int[] { a, a, a, a };
            PlayerProfile.acquiredPotion.Add(newPotionData);
            Debug.Log(PlayerProfile.acquiredPotion.Count);
            a++;
            SaveManager.Save();
            Debug.Log("Save");
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SaveManager.Load();
            Debug.Log("Load");
            Debug.Log(PlayerProfile.acquiredPotion.Count);
            Debug.Log(PlayerProfile.acquiredPotion[PlayerProfile.acquiredPotion.Count - 1].potionFormular[3]);
        }
    }

    [ContextMenu("testing")]
    void test()
    {
        bool y = CompareLists(list1, list2);
        Debug.Log(y);
        foreach (int item in list2)
        {
            Debug.Log(item);
        }
    }

    public bool CompareLists(List<int> aListA, List<int> aListB)
    {
        if (aListA == null || aListB == null || aListA.Count != aListB.Count)
            return false;
        if (aListA.Count == 0)
            return true;
        Dictionary<int, int> lookUp = new Dictionary<int, int>();
        // create index for the first list
        for (int i = 0; i < aListA.Count; i++)
        {
            int count = 0;
            if (!lookUp.TryGetValue(aListA[i], out count))
            {
                lookUp.Add(aListA[i], 1);
                continue;
            }
            lookUp[aListA[i]] = count + 1;
        }
        for (int i = 0; i < aListB.Count; i++)
        {
            int count = 0;
            if (!lookUp.TryGetValue(aListB[i], out count))
            {
                // early exit as the current value in B doesn't exist in the lookUp (and not in ListA)
                return false;
            }
            count--;
            if (count <= 0)
                lookUp.Remove(aListB[i]);
            else
                lookUp[aListB[i]] = count;
        }
        // if there are remaining elements in the lookUp, that means ListA contains elements that do not exist in ListB
        return lookUp.Count == 0;
    }
}
