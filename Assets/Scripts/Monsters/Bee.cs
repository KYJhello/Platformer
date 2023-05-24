using BeeState;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bee : MonoBehaviour
{
    public enum State { Idle, Trace, Return, Attack, Patrol, Size }

    [SerializeField] TMP_Text text;
    [SerializeField] public float detcetRange;
    [SerializeField] public float attackRange;
    [SerializeField] public float moveSpeed;
    [SerializeField] public Transform[] patrolPoints;

    public StateBase[] states;
    public State curState;

    // model
    public Transform player;
    public int patrolIndex = 0;
    public Vector2 returnPositon;


    private void Awake()
    {
        states = new StateBase[(int)State.Size];
        states[(int)State.Idle] = new IdleState(this);          // 0
        states[(int)State.Trace] = new TraceState(this);        // 1
        states[(int)State.Return] = new ReturnState(this);      // 2
        states[(int)State.Attack] = new AttackState(this);      // 3
        states[(int)State.Patrol] = new PatrolState(this);      // 4
    }

    private void Start()
    {
        curState = State.Idle;
        states[(int)curState].Enter();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        returnPositon = transform.position;
    }
    private void Update()
    {
        states[(int)curState].Update();
    }
    public void ChangeState(State state)
    {
        switch (state)
        {
            case State.Idle:
                text.text = "Idle";
                break;
            case State.Trace:
                text.text = "Trace";
                break;
            case State.Return:
                text.text = "Return";
                break;
            case State.Attack:
                text.text = "Attack";
                break;
            case State.Patrol:
                text.text = "Patrol";
                break;
        }
        states[(int)curState].Exit();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detcetRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
namespace BeeState
{
    public class IdleState : StateBase{
        private Bee bee;
        private float idleTime;

        public IdleState(Bee bee) 
        {
            this.bee = bee;
        }
        public override void Update()
        {
            idleTime += Time.deltaTime;
            if(idleTime > 2)
            {
                idleTime = 0;
                
                bee.ChangeState(Bee.State.Patrol);
            }else 
            {
                bee.ChangeState(Bee.State.Trace);
            }
        }
        public override void Enter()
        {
            Debug.Log("Idle Enter");
        }

        public override void Exit()
        {
            Debug.Log("Idle Exit");
        }


    }
    public class TraceState : StateBase
    {
        private Bee bee;

        public TraceState(Bee bee)
        {
            this.bee = bee;
        }
        public override void Enter()
        {
            Debug.Log("Trace Enter");
        }

        public override void Exit()
        {
            Debug.Log("Trace Exit");
        }

        public override void Update()
        {

        }
    }

    public class ReturnState : StateBase
    {
        private Bee bee;
        public ReturnState(Bee bee)
        {
            this.bee = bee;
        }

        public override void Enter()
        {
            Debug.Log("Return Enter");
        }

        public override void Exit()
        {
            Debug.Log("Return Exit");
        }


        public override void Update()
        {
            
        }

    }
    public class AttackState : StateBase
    {
        private Bee bee;
        public AttackState(Bee bee)
        {
            this.bee = bee;
        }

        public override void Enter()
        {
            Debug.Log("Attack Enter");
        }

        public override void Exit()
        {
            Debug.Log("Attack Exit");
        }


        public override void Update()
        {

        }
    }
    public class PatrolState : StateBase
    {
        private Bee bee;
        public PatrolState(Bee bee)
        {
            this.bee = bee;
        }

        public override void Enter()
        {
            Debug.Log("Patrol Enter");
            bee.patrolIndex = (bee.patrolIndex + 1) % bee.patrolPoints.Length;
        }

        public override void Exit()
        {
            Debug.Log("Patrol Exit");
        }


        public override void Update()
        {

        }
    }
}





//namespace DesignPattern
//{
//    public class Bee : MonoBehaviour
//    {
//        [SerializeField] TMP_Text text;
//        public enum State { Idle, Trace, Return, Attack, Patrol }

//        private State curState;
//        private Transform player;
//        [SerializeField] private float detcetRange;
//        [SerializeField] private float attackRange;
//        [SerializeField] private float moveSpeed;
//        [SerializeField] private Transform[] patrolPoints;

//        private int patrolIndex = 0;
//        private Vector2 returnPositon;

//        private void Start()
//        {
//            curState = State.Idle;
//            player = GameObject.FindGameObjectsWithTag("Player").transform;
//            returnPosition = transform.position;
//        }

//        private void Update()
//        {
//            switch (curState)
//            {
//                case State.Idle:
//                    text.text = "Idle";
//                    IdleUpdate();
//                    break;
//                case State.Trace:
//                    text.text = "Trace";
//                    TraceUpdate();
//                    break;
//                case State.Return:
//                    text.text = "Return";
//                    ReturnUpdate();
//                    break;
//                case State.Attack:
//                    text.text = "Attack";
//                    AttackUpdate();
//                    break;
//                case State.Patrol:
//                    text.text = "Patrol";
//                    PatrolUpdate();
//                    break;
//                default: break;
//            }
//        }

//        float idleTime = 0;
//        private void IdleUpdate()
//        {
//            if (idleTime > 2)
//            {
//                idleTime = 0;
//                patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
//                curState = State.Patrol;
//            }
//            idleTime += Time.deltaTime;
//            if (Vector2.Distance(player.position, transform.position) < detcetRange)
//            {
//                curState = State.Trace;
//            }
//        }
//        private void TraceUpdate()
//        {
//            Vector2 dir = (player)
//        }
//        private void ReturnUpdate()
//        {
//            Vector2 dir = (returnPosition - transform.position).normalized;
//            transform.Translate(dir * moveSpeed * Time.deltaTime);

//            if (Vector2.Distance(transform.position,))
//    }
//        float lastAttackTime = 0;
//        private void AttackUpdate()
//        {
//            if (lastAttackTime > 3)
//            {
//                Debug.Log("АјАн");
//                lastAttackTime = 0;
//            }
//            lastAttackTime += Time.deltaTime;
//            if (Vector2.Distance(player.position, transform.position) > attackRange)
//            {
//                curState = State.Trace;
//            }
//        }
//        private void PatrolUpdate()
//        {
//            Vector2 dir = (patrolPoints[patrolIndex].position - transform.position).normalized;
//            transform.Translate(dir * moveSpeed * Time.deltaTime);

//            if (Vector2.Distance(transform.position, patrolPoints[patrolIndex].position) < 0.02f)
//            {
//                curState = State.Idle;
//            }
//            else if (Vector2.Distance(player.position, transform.position) < detcetRange)
//            {
//                curState = State.Trace;
//            }
//        }
//        private void OnDrawGizmos()
//        {
//            Gizmos.color = Color.yellow;
//        }
//    }
//}