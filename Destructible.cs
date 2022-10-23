using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject destroyedVesrion;
    public int cur_Health;
    public int destructibleHealth;
    public int Hp
    {
        get
        {
            return cur_Health;
        }
        set
        {
            if (value >= 0)
            {
                cur_Health = value;
            }
            else
            {
                cur_Health = 0;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        cur_Health = Mathf.Clamp(cur_Health - 1, 0, 100);
        cur_Health = destructibleHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (cur_Health <= 0)
        {
            Instantiate(destroyedVesrion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int amount)
    {
        cur_Health -= amount;
    }
}