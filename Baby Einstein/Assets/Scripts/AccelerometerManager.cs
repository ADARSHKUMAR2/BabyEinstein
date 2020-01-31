using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerometerManager : MonoBehaviour
{
    float direction;
    private float pos;
    Rigidbody2D rb;
    [SerializeField]
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position.x;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float accelerometer_value = Input.acceleration.x;
        direction = (float)(System.Math.Round(accelerometer_value , 3)) * speed;
        Debug.Log(direction);
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, pos - (speed/6) , pos + (speed/6)), transform.position.y);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(-direction, 0f,0f) * 15f;
    }

}
