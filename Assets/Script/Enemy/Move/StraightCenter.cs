using UnityEngine;

public class StraightCenter : MonoBehaviour
{
    [SerializeField]
    float speed = 2f;

    void Update()
    {
        CheckLimit();
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void CheckLimit()
    {
        if (transform.position.y < -7f)
        {
            Destroy(gameObject);
        }
    }
}
