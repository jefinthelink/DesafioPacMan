using UnityEngine;

public class Enemy : People
{
    public int number;
    [HideInInspector] public People p;
    public float xpValue = 10.0f;
    public int pointsValue = 2;
    private void Start()
    {
        p = new People();
    }


}
