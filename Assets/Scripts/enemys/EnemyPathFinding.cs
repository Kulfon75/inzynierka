using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    /*  private int currentPathIndex;
      private List<Vector3> pathVectorList;

      void Start()
      {

      }

      void Update()
      {
          if (Input.GetMouseButtonDown(2))
          {
              SetTargetPosition(GetMousePos());
          }
          HandleMovement();
      }

      public void SetTargetPosition(Vector3 position)
      {
          currentPathIndex = 0;
          pathVectorList = pathFinding.Instance.FindPath(transform.position, position);
          if (pathVectorList != null && pathVectorList.Count > 1)
          {
              pathVectorList.RemoveAt(0);
          }
      }

      public Vector3 GetMousePos()
      {
          Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
          vec.z = 0;
          return vec;
      }

      private void HandleMovement()
      {
          if (pathVectorList != null)
          {
              Vector3 targetPosition = pathVectorList[currentPathIndex];
              if (Vector3.Distance(transform.position, targetPosition) > 1f)
              {
                  Vector3 moveDir = (targetPosition - transform.position).normalized;
                  transform.position = transform.position + moveDir * 5 * Time.deltaTime;
              }
              else
              {
                  currentPathIndex++;
                  if (currentPathIndex >= pathVectorList.Count)
                  {
                      StopMoving();
                  }
              }
          }
      }

      private void StopMoving()
      {
          pathVectorList = null;
      }*/

    private const float speed = 5f;

    private int currentPathIndex;
    private List<Vector3> pathVectorList;


    private void Start()
    {
    }

    private void Update()
    {
        HandleMovement();

        if (Input.GetMouseButtonDown(2))
        {
            SetTargetPosition(GetMousePos());
        }
    }

    private void HandleMovement()
    {
        if (pathVectorList != null)
        {

            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 1f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
            }
            else
            {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count)
                {
                    StopMoving();
                }
            }
        }
    }

    private void StopMoving()
    {
        pathVectorList = null;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        pathVectorList = pathFinding.Instance.FindPath(GetPosition(), targetPosition);

        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }

    public Vector3 GetMousePos()
    {
        Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vec.z = 0;
        return vec;
    }
}
