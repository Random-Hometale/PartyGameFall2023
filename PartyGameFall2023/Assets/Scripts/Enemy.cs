using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnCollisonEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Floor")
            {
                GameObject magicEffect = ObjectPooling.instance.GetPooledObject("MagicEffect");
                magicEffect.transform.position = transform.position;
                magicEffect.SetActive(true);
                magicEffect.GetComponent<Animator>().Play("MagicEffect", -1, 0);

                gameObject.SetActive(false);
            }
        }

        


}
