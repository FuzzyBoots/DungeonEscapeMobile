using UnityEngine;
using UnityEngine.Assertions;

public class SpiderSprite : MonoBehaviour
{    
    public void FireOrb()
    {
        Spider spider = transform.parent.GetComponent<Spider>();
        Assert.IsNotNull(spider, "Spider sprite failed to find a Spider as a parent");
        spider.FireOrb();
    }
}
