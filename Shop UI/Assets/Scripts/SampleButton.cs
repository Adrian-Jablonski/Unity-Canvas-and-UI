using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SampleButton : MonoBehaviour
{

    // Add references to these variables in the samplebutton created. Then scroll to the top the the SampleButton inspector and click Overrides in Prefab section and apply all changes. This will apply the changes to the prefab
    public Button button;
    public Text nameLabel;
    public Text priceLabel;
    public RawImage iconImage;

    private Item item;
    private ShopScrollList scrollList;
    public ImageLoader imageLoader;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(HandleClick);
    }

    public void Setup(Item currentItem, ShopScrollList currentScrollList)
    {
        item = currentItem;
        nameLabel.text = item.itemName;
        priceLabel.text = item.price.ToString();
        StartCoroutine(GetTexture(item.url, iconImage));


        scrollList = currentScrollList;
    }

    public void HandleClick()
    {
        scrollList.ClickedItem(item);
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
