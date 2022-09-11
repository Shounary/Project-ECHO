using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsExplosion : PlayerTrigger
{
    [SerializeField] private Destructable[] wallsArray;
    [Range(0, 1)]
    [SerializeField] private float timeBeforeExplosions = 0.1f;

    private List<Destructable> walls;
    private float counter;

    void Start()
    {
        counter = Random.Range(0, timeBeforeExplosions);
        walls = new List<Destructable>(wallsArray);
    }

    void Update()
    {
        if (isActive && walls.Count > 0) {
            counter -= Time.deltaTime;
            if (counter <= 0) {
                Explode();
                counter = Random.Range(0, timeBeforeExplosions);
            }
        }
    }

    private void Explode()
    {
        walls[0].TakeDamage(1000f);
        walls.RemoveAt(0);
    }

    public override void Execute()
    {
        isActive = true;
    }
}
