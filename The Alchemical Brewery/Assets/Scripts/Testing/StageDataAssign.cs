using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Stage Data", menuName = "StageDataAssign")]
public class StageDataAssign : ScriptableObject
{
    public int stageIndex;
    public List<int> CustomerAppear;
}
