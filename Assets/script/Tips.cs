using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Tips : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ClearSelf", 2);
    }
    void ClearSelf()
    {
        GameObject.Destroy(gameObject);
    }

    public void Init(string showContent)
    {
        Text tempText = this.GetComponent<Text>();
        tempText.text = showContent;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
