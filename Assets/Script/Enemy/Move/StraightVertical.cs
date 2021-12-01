using UnityEngine;

public class StraightVertical : MonoBehaviour
{
    [SerializeField]
    float speed = 3f;
    [SerializeField]
    private int dir = 1;


    void Update()
    {
        CheckLimit();
        if (transform.position.y < 3.5)
        {
            transform.Translate(new Vector2(1f * dir, 0f).normalized * speed * Time.deltaTime);
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
        if (transform.position.y < 1f)
        {
            Destroy(gameObject);
        }
    }
}
