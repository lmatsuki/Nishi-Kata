using NishiKata.Utilities;
using UnityEngine;

namespace NishiKata.Inputs
{
    public class DesktopPlayerMovement : BaseMovement, IPlayerMovement
    {
        public float movementSpeed;
        public float rotationSpeed;

        private Transform characterTransform;
        private new Rigidbody rigidbody;

        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            characterTransform = transform.Find(Names.PlayerPrism);
        }

        public void UpdateMovement()
        {
            if (!canMove)
            {
                return;
            }

            float horizontalMovement = HandleHorizontalInput();
            float verticalMovement = HandleVerticalInput();
            Vector3 movement = new Vector3(horizontalMovement, 0.0f, verticalMovement);
            rigidbody.velocity = movement * movementSpeed;
            HandleRotationInput();
        }

        public Rigidbody GetRigidbody()
        {
            return rigidbody;
        }

        public void SetCanMove(bool status)
        {
            canMove = status;
        }

        float HandleHorizontalInput()
        {
            float horizontalMovement = 0.0f;

            if (Input.GetKey(KeyCode.A))
            {
                horizontalMovement = -movementSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                horizontalMovement = movementSpeed * Time.deltaTime;
            }

            return horizontalMovement;
        }

        float HandleVerticalInput()
        {
            float verticalMovement = 0.0f;

            if (Input.GetKey(KeyCode.W))
            {
                verticalMovement = movementSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                verticalMovement = -movementSpeed * Time.deltaTime;
            }

            return verticalMovement;
        }

        void HandleRotationInput()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
            }
        }
    }
}
