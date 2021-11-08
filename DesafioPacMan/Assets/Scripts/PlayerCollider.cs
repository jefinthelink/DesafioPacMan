using System.Collections;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
     private Player player;
    

    private void Start()
    {
        GetValues();
    }
    private void GetValues()
    {
        player = transform.GetComponent<Player>();
        
    }
    
     void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy.number >= player.level)
            {
                player.GameOverPanel.SetActive(true);
                player.GameOverPanel.GetComponent<GameOver>().ShowPanel();
                Destroy(transform.gameObject);
            }
            else
            {
                player.points += enemy.pointsValue;
                player.GetXp(enemy.xpValue);
                Destroy(collision.gameObject);
               
            }
        }
        if (collision.gameObject.CompareTag("Xp"))
        {
            player.GetXp(collision.gameObject.GetComponent<Xp>().xpValue);
            //tocar som
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("StartEvent"))
        {
            player.events.StartEvent();
            Destroy(collision.gameObject);

        }

        
       
    }

}
