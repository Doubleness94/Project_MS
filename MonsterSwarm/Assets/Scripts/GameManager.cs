using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get
        {
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (m_instance == null)
            {
                // ������ GameManager ������Ʈ�� ã�� �Ҵ�
                m_instance = FindObjectOfType<GameManager>();
            }

            // �̱��� ������Ʈ�� ��ȯ
            return m_instance;
        }
    }

    private static GameManager m_instance; // �̱����� �Ҵ�� static ����
    public bool isGameover { get; private set; } // ���� ���� ����
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
