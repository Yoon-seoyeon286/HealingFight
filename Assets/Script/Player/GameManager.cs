using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindAnyObjectByType<GameManager>();
            }

            return m_instance;
        }
    }

    static GameManager m_instance;
    int score = 0;
    public bool isGameOver{ get; private set; }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
