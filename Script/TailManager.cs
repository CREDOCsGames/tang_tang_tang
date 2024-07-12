using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TailManager : ScriptableObject
{
    public Stage1RemainTails RemainTails1Box;
    public Stage2RemainTails RemainTails2Box;

    public int[] tails = new int[5];

    public int PresentStageNum;

    public bool[] existingTail = new bool[10];

}
