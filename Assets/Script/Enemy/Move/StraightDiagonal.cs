using UnityEngine;

public class StraightDiagonal : MonoBehaviour
{
    [SerializeField]
    float speed = 2f;
    [SerializeField]
    private int dir = 1;


    void Update()
    {
        CheckLimit();
        if(transform.position.y < 6)
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
        if (transform.position.y < -7f)
        {
            Destroy(gameObject);
        }
    }
}
