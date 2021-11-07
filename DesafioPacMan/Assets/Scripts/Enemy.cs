using UnityEngine;

public class Enemy : People
{
    public int number;
    [HideInInspector] public People p;
    public float xpValue = 10.0f;
    public int pointsValue = 2;
    [SerializeField] private float speed = 4.0f;

   
    public Action currentAction;

    public Transform target;
    private Transform wayPoint0;
    public Transform lasttarget;
    public Transform player;


    private void Start()
    {
        p = new People();
       // currentAction = action.walk;
        wayPoint0 = GameObject.FindGameObjectWithTag("WayPoint0").transform;
        target = wayPoint0;
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void Update()
    {
        FreeMoving();
        FollowPlayer();
        GoHome();
    }

    private void FreeMoving()
    {
        if (currentAction == Action.walk)
        {
            if (transform.position == target.position)
            {
                Transform[] points = target.GetComponent<WayPoints>().nearByPoints;
                int rand = Random.Range(0, points.Length);

                if (points[rand] != lasttarget)
                {
                    lasttarget = target;
                    target = points[rand];
                }
            }
            else
            {
                Move(transform.position,target.position);
            }

        }
    }
    private void FollowPlayer()
    {
        if (currentAction == Action.FollowPlayer)
        {
            if (player == null)
            {
                currentAction = Action.home;
            }
            else 
            {
            Move(transform.position,player.position);
            }
        }
    }
    private void GoHome()
    {
        if (currentAction == Action.home)
        {
            if (transform.position == wayPoint0.position)
            {
                currentAction = Action.walk;
            }
            else 
            {
                Move(transform.position,wayPoint0.position);
            }
        }
    }

    private void Move(Vector3 inicialPosition, Vector3 target)
    {
        transform.position = Vector3.MoveTowards(inicialPosition, target, speed * Time.deltaTime);    
    }

}

public enum Action
{
    home,
    walk,
    FollowPlayer
}
