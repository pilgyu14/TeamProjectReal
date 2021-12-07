using UnityEngine;

public class StraightDiagonal : MonoBehaviour
{
    [SerializeField]
    float speed = 2f;
    [SerializeField]
    private int dir = 1;

    private EnemyShooting enemyShooting = null;

    private void Start()
    {
        enemyShooting = FindObjectOfType<EnemyShooting>();
    }


    void Update()
    {
        CheckLimit();
        if (transform.position.y < 6)
        {
            transform.Translate(new Vector2(1f * dir, -1f).normalized * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(new Vector2(0f, -1f).normalized * speed * Time.deltaTime);
        }
    }

    private void CheckLimit()
    {
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
        if (transform.position.x < -8f)
        {
            Destroy(gameObject);
        }
        if (transform.position.x > 3f)
        {
            Destroy(gameObject);
        }
    }
}
