using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Ball"))
        {
            GameObject particles = Instantiate(
                LevelManager.Instance.brickParticles,
                transform.position,
                LevelManager.Instance.brickParticles.transform.rotation
                );
            Destroy(particles, 0.6f);
            LevelManager.Instance.DestroyBrick();
            Destroy(gameObject);
        }
    }
}
