//namespace BeeState
//{
//    public class TraceState : StateBase
//    {

//    }
//}





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
//                Debug.Log("공격");
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