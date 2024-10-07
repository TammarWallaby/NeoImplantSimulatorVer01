/* Player가 존재하는 모든 씬 안의 Player에 들어갈 스크립트
 * 플레이어 이동 기능
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public MainCamController cameraControl;

    public float moveSpeed;

    Rigidbody rb;
    Vector3 cameraForward;
    Vector3 cameraRight;
    Vector3 moveDir;
    Vector3 velocity;

    float horizontal;
    float vertical;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // 카메라 방향 벡터를 가져옵니다.
        cameraForward = cameraControl.GetForwardDirection();
        cameraRight = cameraControl.GetRightDirection();

        // 입력 값 받아오기
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // 플레이어 이동 방향 계산
        moveDir = (cameraForward * vertical + cameraRight * horizontal).normalized;
    }

    private void FixedUpdate()
    {
        velocity = moveDir * moveSpeed;
        // Rigidbody의 y축 속도는 현재 속도를 유지
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
    }
}
