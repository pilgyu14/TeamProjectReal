using UnityEngine;

public class StraightCenter : MonoBehaviour
{
    [SerializeField]
    float speed = 2f;

    public bool IsMove = true;
    void Update()
    {
        CheckLimit();
        if (IsMove)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
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
