using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementMeterPanel : MonoBehaviour
{
    public static ElementMeterPanel Instance { get; private set; }

    [Header("Element Mana Value")]
    [Range(0, 4)] public int[] elementMana = new int[5] { 0, 0, 0, 0, 0 };

    [System.NonSerialized] public bool[] elementReady = new bool[5]; //element ready

    [System.NonSerialized] public int[] elementSkillRemaining = new int[5] { 0, 0, 0, 0, 0 };

    float testTime = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        UpdateElementReady();
    }

    void UpdateElementReady()
    {
        //check every element skill remaining
        for (int i = 0; i < elementSkillRemaining.Length; i++)
        {
            if(elementSkillRemaining[i] == 0) //if no more skill left (Allow element mana consuming)
            {
                if(elementMana[i] >= 4) //if element mana full
                {
                    elementMana[i] = 4;
                    //element ready to reload
                    elementReady[i] = true;
                }
                else //if element mana not full
                {
                    elementReady[i] = false;
                }
            }
        }
    }

    void ReloadSkillRemaining(int index)
    {
        switch(index)
        {
            case 0: //ignis
                {
                    elementSkillRemaining[0] += 1;
                    break;
                }
            case 1: //aqua
                {
                    elementSkillRemaining[1] += 3;
                    break;
                }
            case 2: //terra
                {
                    elementSkillRemaining[2] += 1;
                    break;
                }
            case 3: //aer
                {
                    elementSkillRemaining[3] += 3;
                    break;
                }
            case 4: //ordo
                {
                    elementSkillRemaining[4] += 3;
                    break;
                }
        }
    }

    public void ActivateElementSkill(int index)
    {
        //check if element is ready and skill is NOT activated
        if(elementReady[index] && !ElementSkillManager.Instance.skillActivated[index])
        {
            switch (index)
            {
                case 0: //activate ignis skill
                    {
                        //Adding element skill remaining if just start up skill
                        if (elementSkillRemaining[index] == 0)
                        {
                            ReloadSkillRemaining(index);
                        }
                        //dierectly activate ignis skill
                        StartCoroutine(ElementSkillManager.Instance.IgnisSkill());
                        break;
                    }
                case 1: //activate aqua skill
                    {
                        //Adding element skill remaining if just start up skill
                        if (elementSkillRemaining[index] == 0)
                        {
                            ReloadSkillRemaining(index);
                        }
                        //dierectly activate aqua skill
                        StartCoroutine(ElementSkillManager.Instance.AquaSkill());
                        break;
                    }
                case 2: //activate terra skill
                    {
                        //Adding element skill remaining if just start up skill
                        if (elementSkillRemaining[index] == 0)
                        {
                            ReloadSkillRemaining(index);
                        }
                        //dierectly activate terra skill
                        StartCoroutine(ElementSkillManager.Instance.TerraSkill());
                        break;
                    }
                case 3: //activate aer skill
                    {
                        //Adding element skill remaining if just start up skill
                        if (elementSkillRemaining[index] == 0)
                        {
                            ReloadSkillRemaining(index);
                        }
                        //dierectly activate aer skill
                        StartCoroutine(ElementSkillManager.Instance.AerSkill());
                        break;
                    }
                case 4: //activate ordo skill
                    {
                        //Adding element skill remaining if just start up skill
                        if (elementSkillRemaining[index] == 0)
                        {
                            ReloadSkillRemaining(index);
                        }
                        ElementSkillManager.Instance.skillActivated[4] = true;
                        break;
                    }
            }
        }
        else if (elementReady[index] && ElementSkillManager.Instance.skillActivated[index])
        {
            switch (index)
            {
                case 0:
                    {
                        break;
                    }

                case 1:
                    {
                        break;
                    }
                case 2:
                    {
                        break;
                    }
                case 3:
                    {
                        break;
                    }
                case 4: //using ordo skill
                    {
                        ElementSkillManager.Instance.OrdoSkill();
                        break;
                    }
            }
        }
    }
}
