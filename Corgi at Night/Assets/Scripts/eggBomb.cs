using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eggBomb : MonoBehaviour {

    private CircleCollider2D shellsplosion;
    private Animator eggAnim;

	// Update is called once per frame
	void Start () {
        shellsplosion = gameObject.GetComponent<CircleCollider2D>();
        eggAnim = gameObject.GetComponent<Animator>();
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Floor"))
        {
            shellsplosion.radius = shellsplosion.radius * 1.5f;
            StartCoroutine("deathLinger");
            eggAnim.Play("boom");
        }
    }

    public IEnumerator deathLinger()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}
