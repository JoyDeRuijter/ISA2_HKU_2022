// Written by Joy de Ruijter
using UnityEngine;
using System.Collections.Generic;

public enum NPCType {Friendly, Protective, Hostile}

public class NPC : MonoBehaviour
{
    #region Variables

    [Header("NPC Properties")]
    [SerializeField] protected NPCType npcType;
    [SerializeField] protected new string name;

    protected Rigidbody rb;
    protected Animator anim;
    protected CapsuleCollider col;

    #endregion

    public NPC(NPCType _npcType, string _name)
    { 
        npcType = _npcType;
        name = _name;
    }

    public void FormNPC()
    {
        switch (npcType)
        { 
            case NPCType.Friendly:
                GameObject friendlyNpcObject = new GameObject("NPC_" + npcType + "_" + name, typeof(Rigidbody), typeof(Animator), typeof(CapsuleCollider), typeof(FriendlyNPC));
                FriendlyNPC friendlyNPC = friendlyNpcObject.GetComponent<FriendlyNPC>();
                friendlyNPC.name = name;
                friendlyNPC.npcType = npcType;
                friendlyNpcObject.transform.position = transform.position;
                friendlyNpcObject.transform.rotation = transform.rotation;
                friendlyNpcObject.transform.localScale = transform.localScale;
                friendlyNPC.rb = friendlyNpcObject.GetComponent<Rigidbody>();
                friendlyNPC.rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                friendlyNPC.anim = friendlyNpcObject.GetComponent<Animator>();
                friendlyNPC.col = friendlyNpcObject.GetComponent<CapsuleCollider>();
                friendlyNPC.col.center = new Vector3(0, 1, 0);
                friendlyNPC.col.radius = 0.25f;
                friendlyNPC.col.height = 2f;
                gameObject.transform.parent = friendlyNpcObject.transform;
                DestroyImmediate(this);
                break;

            case NPCType.Protective:
                GameObject protectiveNpcObject = new GameObject("NPC_" + npcType + "_" + name, typeof(Rigidbody), typeof(Animator), typeof(CapsuleCollider), typeof(ProtectiveNPC));
                ProtectiveNPC protectiveNPC = protectiveNpcObject.GetComponent<ProtectiveNPC>();
                protectiveNPC.name = name;
                protectiveNPC.npcType = npcType;
                protectiveNpcObject.transform.position = transform.position;
                protectiveNpcObject.transform.rotation = transform.rotation;
                protectiveNpcObject.transform.localScale = transform.localScale;
                protectiveNPC.rb = protectiveNpcObject.GetComponent<Rigidbody>();
                protectiveNPC.rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                protectiveNPC.anim = protectiveNpcObject.GetComponent<Animator>();
                protectiveNPC.col = protectiveNpcObject.GetComponent<CapsuleCollider>();
                protectiveNPC.col.center = new Vector3(0, 1, 0);
                protectiveNPC.col.radius = 0.25f;
                protectiveNPC.col.height = 2f;

                GameObject protectiveHolsterObject = new GameObject("WeaponHolster", typeof(WeaponHolster));
                protectiveHolsterObject.transform.parent = gameObject.transform.Find("Root/Hips/Spine_01/Spine_02/Spine_03/Clavicle_R/Shoulder_R/Elbow_R/Hand_R");
                protectiveHolsterObject.transform.localScale = new Vector3(100, 100, 100);
                protectiveHolsterObject.transform.localPosition = Vector3.zero;
                protectiveHolsterObject.transform.localRotation = Quaternion.identity;
                protectiveNPC.holster = protectiveHolsterObject.GetComponent<WeaponHolster>();

                gameObject.transform.parent = protectiveNpcObject.transform;

                DestroyImmediate(this);
                break;

            case NPCType.Hostile:
                GameObject hostileNpcObject = new GameObject("NPC_" + npcType + "_" + name, typeof(Rigidbody), typeof(Animator), typeof(CapsuleCollider), typeof(HostileNPC));
                HostileNPC hostileNPC = hostileNpcObject.GetComponent<HostileNPC>();
                hostileNPC.name = name;
                hostileNPC.npcType = npcType;
                hostileNpcObject.transform.position = transform.position;
                hostileNpcObject.transform.rotation = transform.rotation;
                hostileNpcObject.transform.localScale = transform.localScale;
                hostileNPC.rb = hostileNpcObject.GetComponent<Rigidbody>();
                hostileNPC.rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                hostileNPC.anim = hostileNpcObject.GetComponent<Animator>();
                hostileNPC.col = hostileNpcObject.GetComponent<CapsuleCollider>();
                hostileNPC.col.center = new Vector3(0, 1, 0);
                hostileNPC.col.radius = 0.25f;
                hostileNPC.col.height = 2f;

                GameObject hostileHolsterObject = new GameObject("WeaponHolster", typeof(WeaponHolster));
                hostileHolsterObject.transform.parent = gameObject.transform.Find("Root/Hips/Spine_01/Spine_02/Spine_03/Clavicle_R/Shoulder_R/Elbow_R/Hand_R");
                hostileHolsterObject.transform.localScale = new Vector3(100, 100, 100);
                hostileHolsterObject.transform.localPosition = Vector3.zero;
                hostileHolsterObject.transform.localRotation = Quaternion.identity;
                hostileNPC.holster = hostileHolsterObject.GetComponent<WeaponHolster>();

                gameObject.transform.parent = hostileNpcObject.transform;
                DestroyImmediate(this);
                break;

            default:
                break;
        }
    }
}

public enum Direction { forward, backward }
