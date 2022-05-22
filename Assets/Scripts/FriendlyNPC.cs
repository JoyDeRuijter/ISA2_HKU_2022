// Written by Joy de Ruijter
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FriendlyNPC : NPC
{
    #region Variables

    public bool canReactToPlayer;
    [HideInInspector] public float sightRange;

    public bool canInteractWithPlayer;
    [HideInInspector] public float interactRange;

    public bool moves;
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public Transform pathTransform;
    [HideInInspector] public bool pathIsACircuit;
    [HideInInspector] public int pathStartNode;
    [HideInInspector] public Direction pathStartDirection;

    private NodePath path;
    private List<Transform> nodes = new List<Transform>();
    [HideInInspector] public int currentNode;
    private Direction currentDirection;

    #endregion

    FriendlyNPC(NPCType _npcType, string _name)
    : base(_npcType, _name) { }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();

        if (moves)
        {
            path = pathTransform.GetComponent<NodePath>();
            path.isACircuit = pathIsACircuit;
            currentNode = pathStartNode;
            currentDirection = pathStartDirection;
            InitializePath();
            transform.position = nodes[pathStartNode].position;
        }
    }

    private void Update()
    {
        if (moves)
            Move();
    }

    private void OnDrawGizmosSelected()
    {
        if (canReactToPlayer)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);
        }
        if (canInteractWithPlayer)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, interactRange);
        }
    }

    #region Movement & Pathfinding

    private void InitializePath()
    {
        Transform[] pathTransforms = pathTransform.GetComponentsInChildren<Transform>();
        if (nodes == null)
            nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != pathTransform.transform)
                nodes.Add(pathTransforms[i]);
        }
    }

    public virtual void Move()
    {
        CheckWaypointDistance();
        MoveToNode();
    }

    protected void MoveToNode()
    {
        //Debug.Log("The MoveToNode method is being executed");
        //Debug.Log("NPC position: " + transform.position);
        //Debug.Log("Current node position: " + nodes[currentNode].position);
        transform.position = Vector3.MoveTowards(transform.position, nodes[currentNode].position, Time.deltaTime * moveSpeed);
        transform.LookAt(nodes[currentNode].position);
        transform.rotation = Quaternion.Euler(0, transform.localEulerAngles.y, 0);
    }

    protected void FlipDirection()
    {
        if (currentDirection == Direction.forward)
            currentDirection = Direction.backward;
        else if (currentDirection == Direction.backward)
            currentDirection = Direction.forward;
    }

    protected void CheckWaypointDistance()
    {
        //Debug.Log("CheckWayPointDistance method is being executed");
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 0.5f)
        {
            if (currentNode == nodes.Count - 1 && currentDirection == Direction.forward && !path.isACircuit)
            {
                FlipDirection();
                currentNode--;
            }
            else if (currentNode == nodes.Count - 1 && currentDirection == Direction.forward && path.isACircuit)
                currentNode = 0;
            else if (currentNode == 0 && currentDirection == Direction.backward && !path.isACircuit)
            {
                FlipDirection();
                currentNode++;
            }
            else if (currentNode == 0 && currentDirection == Direction.backward && path.isACircuit)
                currentNode = nodes.Count - 1;
            else if (currentDirection == Direction.forward)
                currentNode++;
            else if (currentDirection == Direction.backward)
                currentNode--;
        }
    }

    #endregion
}

