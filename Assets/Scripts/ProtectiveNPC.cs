// Written by Joy de Ruijter
using UnityEngine;

public class ProtectiveNPC : NPC
{
    #region Variables

    [HideInInspector] public WeaponHolster holster;

    #endregion

    ProtectiveNPC(NPCType _npcType, string _name)
    : base(_npcType, _name) { }

}
