using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
                if (_instance == null)
                {
                    Debug.LogWarning(string.Format("{0} 인스턴스를 찾을 수 없습니다.", typeof(UIManager).ToString()));
                }
            }
            return _instance;
        }
    }

    public GameObject messageWindow;
    
}
