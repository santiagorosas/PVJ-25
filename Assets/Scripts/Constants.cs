using UnityEngine;

internal class Constants : MonoBehaviour
{
    static private Constants _instance;

    static public Constants Instance 
    {
        get 
        {
            if (_instance == null) 
            {
                _instance = Resources.Load<Constants>("Constants");
            }
            return _instance;
        }        
    }


    [SerializeField] public float PaddleSpeed;
    [SerializeField] public float BallSpeed;

    //public const float PADDLE_SPEED = 8;
}