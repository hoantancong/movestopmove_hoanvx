using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
public enum BotState
{
    idle,
    patrol,
    attack,
    die
}
public class Bot : Character
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshPro stateText;
    private Dictionary<BotState, IState<Bot>> states;
    private IState<Bot> currentState;

    public float patrolRadius = 30f;
    public int numberOfPoints = 5;
    public NavMeshAgent agent;
    private List<Vector3> patrolPoints;
    public Transform aimedTarget;

    void Start()
    {
        //
        agent = GetComponent<NavMeshAgent>();
        patrolPoints = GeneratePatrolPoints();
        //MoveToNextPatrolPoint();
        //
        states = new Dictionary<BotState, IState<Bot>>
        {
            {BotState.patrol,new PatrolState() },
            {BotState.attack,new AttackState() },
            {BotState.idle,new IdleState() }
        };
        //currentState = states[BotState.patrol];
        ChangeState(BotState.patrol);
    }
    public void ChangeState(BotState newState)
    {
        currentState?.OnExit(this);
        currentState = states[newState];
        currentState.OnEnter(this);
        stateText.text = $"{newState}";
    }
    // Update is called once per frame
    void Update()
    {
        if (!isAlive) return;
        currentState.OnUpdate(this);
    }
    //
    List<Vector3> GeneratePatrolPoints()
    {
        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i < numberOfPoints; i++)
        {
            Vector3 randomPoint = Random.insideUnitSphere * patrolRadius + transform.position;
            NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, patrolRadius, 1);
            points.Add(hit.position);
        }
        return points;
    }
    //
    public void MoveToNextPatrolPoint()
    {
        if (!isAlive) 
            return;
        if (patrolPoints.Count == 0)
            return;
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            agent.destination = patrolPoints[Random.Range(0, patrolPoints.Count)];
            TF.forward = Vector3.forward;
            ChangeState(BotState.idle);
        }
 
    }
    public override Transform FireTarget()
    {
        Transform target = base.FireTarget();
        if (target != null && Ultility.GetRandom5050())
        {
            aimedTarget = target;
            ChangeState(BotState.attack);
            return target;

        }
        return null;
    }
    //
    public void OnWaitWeaponReady()
    {
        //after fire bot is wait for weapon ready
        if (!isFired)
        {
            ChangeState(BotState.patrol);
        }
    }
    public override void GetHit()
    {
        base.GetHit();
        agent.isStopped = true;
    }
    //
}
