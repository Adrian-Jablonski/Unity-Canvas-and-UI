using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        // iconImage.texture = item.icon;   //TODO: Set up for image to load from URL

        scrollList = currentScrollList;
    }

    public void HandleClick()
    {
        scrollList.ClickedItem(item);
    }
    
}
