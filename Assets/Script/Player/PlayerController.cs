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
        if (input.magnitude > 1f) return; //�Է¹��� ���� 1���� ������ ������� (�밢������ �����̷��Ҷ� ���� ��)

        if (context.performed)  //����ƴ��� üũ
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
            //�ݹ��Լ�
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
