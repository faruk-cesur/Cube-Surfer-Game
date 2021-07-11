using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // I'm using Singleton Design Pattern and DontDestroyOnLoad for background music in here
    
    private static BackgroundMusic obje = null;

    void Awake()
    {
        if( obje == null )
        {
            obje = this;
            DontDestroyOnLoad( this );
        }
        else if( this != obje )
        {
            Destroy( gameObject );
        }
    }
}
