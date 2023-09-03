using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameInput gameInput;

    private Vector2 moveVector2 = Vector2.zero;

    private float moveSpeed = 12f;
    private float rotateSpeed = 15f;

    private void Update()
    {
        Movement();
        Rotate();

    }

    private void Movement()
    {
        moveVector2 = gameInput.GetMovementVector2Normalized();

        Vector3 moveVector3 = new Vector3(moveVector2.x, moveVector2.y, 0);

        float moveDistance = moveSpeed * Time.deltaTime;
        bool canMove = !Physics2D.CircleCast(this.transform.position, 0.5f, moveVector2, moveDistance);


        if (!canMove)
        {
            Vector2 moveDirX = new Vector2(moveVector3.x, 0).normalized;
            canMove = !Physics2D.CircleCast(this.transform.position, 0.5f, moveDirX, moveDistance);

            if (canMove)
            {
                moveVector3 = new Vector3(moveDirX.x, 0, 0);
            }
            else
            {
                Vector2 moveDirY = new Vector2(0, moveVector3.y).normalized;
                canMove = !Physics2D.CircleCast(this.transform.position, 0.5f, moveDirY, moveDistance);

                if (canMove)
                {
                    moveVector3 = new Vector3(0, moveDirY.y, 0);
                }
                else
                {
                    //Cannot Move
                }
            }
        }

        if (canMove)
        {
            Move(moveVector3);
        }

        moveVector2 = Vector2.zero;
    }

    private void Move(Vector3 moveVector3)
    {
        this.transform.position += moveSpeed * Time.deltaTime * moveVector3;
    }

    private void Rotate()
    {
        //this.transform.up = Vector2.Lerp(this.transform.up, rotateVector2, Time.deltaTime * rotateSpeed);

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        var targetRotation = mouseWorldPosition - this.transform.position;

        this.transform.up = Vector2.Lerp(this.transform.up, targetRotation, Time.deltaTime * rotateSpeed);
    }

}
