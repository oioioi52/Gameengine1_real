using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5.0f;
    
    [Header("점프 설정")]
    public float jumpForce = 10.0f; // 점프 힘으로 사용할 때는 10.0f 보다는 400.0f~700.0f 정도가 적절할 수 있습니다.
    
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private int score = 0;
    // 리스폰용 시작 위치
    private Vector3 startPosition;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // 게임 시작 시 위치를 저장
        startPosition = transform.position;
        Debug.Log("시작 위치 저장: " + startPosition);
    }
    
    void Update()
    {
        // 좌우 이동
        float moveX = 0f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;
        
        // rb.linearVelocity 대신 rb.velocity 사용
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
        
        // 점프 
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // rb.linearVelocity 대신 rb.velocity 사용
            // 점프를 구현할 때는 Velocity에 순간적으로 값을 넣어주는 것이 일반적입니다.
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); 
            
            // 만약 AddForce를 사용하는 경우: rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    
    // 바닥 충돌 감지 (Collision)
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        // 장애물 충돌 감지
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("⚠️ 장애물 충돌! 시작 지점으로 돌아갑니다.");
            
            // 시작 위치로 순간이동
            transform.position = startPosition;
            
            // 속도 초기화 (안 하면 계속 날아감)
            rb.linearVelocity = Vector2.zero; // new Vector2(0, 0)과 동일
        } // <--- 여기에 OnCollisionEnter2D 닫는 괄호가 있었음 (문제의 근원)
    } // <--- OnCollisionEnter2D 함수를 여기서 닫습니다. (추가된 괄호)
    
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    
    // 아이템 수집 감지 (Trigger)
    void OnTriggerEnter2D(Collider2D other)
{
        // 코인 수집 (기존 코드)
        if (other.CompareTag("Coin"))
        {
            score += 1;
            Debug.Log("💰 코인 획득! 현재 점수: " + score);
            Destroy(other.gameObject);
        }
        
        
        // 골 도달 - 새로 추가!
        if (other.CompareTag("Goal"))
        {
            Debug.Log("🎉🎉🎉 게임 클리어! 🎉🎉🎉");
            Debug.Log("최종 점수: " + score + "점");
            
            // 캐릭터 조작 비활성화
            enabled = false;
        }
}
}