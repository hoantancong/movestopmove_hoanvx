using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    // Start is called before the first frame update
    private int numOfBot = 15;
    private List<Bot> bots = new List<Bot>();
    void Start()
    {
        StartCoroutine(CreateBots());
    }
    IEnumerator CreateBots()
    {
        yield return new WaitForSeconds(2);
        //create bot
        for (int i = 0; i < numOfBot; i++)
        {
            Debug.Log(ObjectPool.Instance);
            GameObject bot =  ObjectPool.Instance.SpawnFromPool("Bot", new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)), Quaternion.identity);
            //bots.Add(bot.GetComponent<Bot>());
            //botIndicator.AddBot(bot.transform);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
