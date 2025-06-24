using UnityEngine;

[CreateAssetMenu(fileName = "InstructionSciprtableObject", menuName = "ScriptableObjects/InstructionSciprtableObject", order = 2)]

public partial class InstructionSciprtableObject : ScriptableObject
{
    public InstructionType InstructionType;
    public string Instruction;
    public int DisplayDuration;
    public int WaitToTriggerDuration = 0;
}


