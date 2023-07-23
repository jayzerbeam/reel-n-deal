using UnityEngine;

public class player_controller2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 180f;

    private void Update()
    {
        // Move the character forward
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        // Rotate the character left
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        }

        // Rotate the character right
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        }
    }
}