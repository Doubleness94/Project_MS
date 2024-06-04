using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<GameManager>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }

    private static GameManager m_instance; // 싱글톤이 할당될 static 변수
    public bool isGameover { get; private set; } // 게임 오버 상태
    public PlayerMovement playerMov;
    public PlayerStatus status;
    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
        playerMov = FindObjectOfType<PlayerMovement>();
        status = FindObjectOfType<PlayerStatus>();

    }
}
