using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    public string itemName;
    public string url;
    public float price = 1f;

    public Item()
    {
    }

    public Item(string itemName, string url, float price)
    {
        this.itemName = itemName;
        this.url = url;
        this.price = price;
    }
}

public class ShopScrollList : MonoBehaviour
{

    public List<Item> itemList;
    public Transform contentPanel;
    // public ShopScrollList otherShop;
    public Text myGoldDisplay;
    public SimpleObjectPool buttonObjectPool;
    public float gold = 20f;

    // Start is called before the first frame update
    void Start()
    {
        SetItemList();
        RefreshDisplay();
    }

    public void RefreshDisplay()
    {
        AddButtons();
    }

    private void AddButtons()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            Item item = itemList[i];
            GameObject newButton = buttonObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel);    // Adds new button to verticle layout

            SampleButton sampleButton = newButton.GetComponent<SampleButton>();
            sampleButton.Setup(item, this);
        }
    }

    private void SetItemList()
    {
        itemList.Add(new Item("Item 1", "https://picsum.photos/200/300",  20f));
        itemList.Add(new Item("Item 2", "https://picsum.photos/200/300", 100f));
        itemList.Add(new Item("Item 3", "https://picsum.photos/200/300", 40f));
        itemList.Add(new Item("Item 4", "https://picsum.photos/200/300", 13f));
        itemList.Add(new Item("Item 5", "https://picsum.photos/200/300", 50f));
    }

    public void ClickedItem(Item item)
    {
        Debug.Log("CLICKED ITEM: " + item.itemName);
    }

    IEnumerator GetTexture(string url, RawImage image)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            image.texture = myTexture;
        }
    }
}
