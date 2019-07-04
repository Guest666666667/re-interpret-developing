using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandThrow : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Throw(GameObject projectilePrefab, Vector2 direction)
    {
        GameObject projectileObject = Instantiate(
            projectilePrefab,
            transform.position,
            Quaternion.identity);
        ProjectileControl projectile = projectileObject.GetComponent<ProjectileControl>();
        projectile.Launch(direction, 300*3);
    }
}
