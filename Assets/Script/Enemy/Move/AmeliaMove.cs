using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AmeliaMove : MonoBehaviour
{
    [SerializeField]
    float speed = 2f;

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (transform.position.y < 3)
        {
            speed = 1f;
            if (transform.position.y < 2.3f)
            {
                speed = 0f;
                Invoke("LoadMain", 1f);
            }
        }
    }

    private void LoadMain()
    {
        GameManager.Instance.isStageClear = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Boss1");
    }
}
