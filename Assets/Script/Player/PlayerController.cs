using DG.Tweening;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IHit
{
    public void GetHit();
}
public class PlayerController : MonoBehaviour, IHit
{
    public float moveDistance = 1;
    private Vector3 moveValue;
    private Vector3 curPos;

    void Start()
    {
        moveValue = Vector3.zero;
        curPos = transform.position;
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector3 input = context.ReadValue<Vector3>();
        if (input.magnitude > 1f) return; //입력받은 값이 1보다 높으면 실행안함 (대각선으로 움직이려할때 막는 것)

        if (context.performed)  //수행됐는지 체크
        {
            if (input.magnitude == 0f)
            {
                Moving(transform.position + moveValue);
                Rotate(moveValue);
            }
            else
            {
                moveValue = input * moveDistance;
            }
        }

    }

    public void GetHit()
    {
        throw new System.NotImplementedException();
    }

    void Moving(Vector3 pos)
    {
        transform.DOMove(pos, 0.4f).SetEase(Ease.Linear).OnComplete(() =>
        {
            //콜백함수
            if (pos.z > curPos.z)
            {
                SetMoveForwardState();
            }
        });
    }

    void Rotate(Vector3 pos)
    {
        transform.rotation = Quaternion.Euler(0, pos.x * 90, 0);
    }

    void SetMoveForwardState()
    {
        Manager.Instance.UpdateDistanceCount();
        curPos = transform.position;
    }
}
