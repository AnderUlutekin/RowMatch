using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTile : MonoBehaviour
{

    [SerializeField]
    private GameObject innerTile;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Initialize()
    {
        GameObject inner = Instantiate(innerTile, transform.position, Quaternion.identity);
        inner.transform.parent = this.transform;
        inner.name = this.gameObject.name;
    }

}
