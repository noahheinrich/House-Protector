using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Animator animator;

    private Transform target;
    private int pathIndex = 0;

    private float baseSpeed;
    

    // Start is called before the first frame update
    void Start()
    {
        baseSpeed = moveSpeed;
        target = LevelManager.main.path[pathIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f) {
            pathIndex++;
            

            if (pathIndex == LevelManager.main.path.Length) {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            } else {
                target = LevelManager.main.path[pathIndex];
            }
        }

        animator.SetFloat("Horizontal", target.position.x - transform.position.x); 
        animator.SetFloat("Vertical", target.position.y - transform.position.y);
        animator.SetFloat("Speed", rb.velocity.sqrMagnitude);
    }

    private void FixedUpdate() {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    public void UpdateSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }    

    public void ResetSpeed()
    {
        moveSpeed = baseSpeed;
    }
}
