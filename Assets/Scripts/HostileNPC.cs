// Written by Joy de Ruijter
using UnityEngine;

public class HostileNPC : NPC
{
    #region Variables

    [Space(10)]
    [Header("Hostile NPC Properties")]


    [HideInInspector] public WeaponHolster holster;

    #endregion

    HostileNPC(NPCType _npcType, string _name)
        : base(_npcType, _name) { }

}
