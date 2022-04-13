using TMPro;
using UnityEngine;
using Bitszer;

public class GameManager : MonoBehaviour
{
    public TMP_Text itemCountValueText;

      private string potionItemId = "01G05C01HQ051F1PBY8N1X7Z9H";

  private void OnEnable()
  {
    Events.OnAuctionHouseInitialized.AddListener(OnAuctionHouseInitialized);
  }

  private void OnDisable()
  {
    Events.OnAuctionHouseInitialized.RemoveListener(OnAuctionHouseInitialized);
  }

  private void OnAuctionHouseInitialized()
  {
    Debug.Log("AuctionHouse Initialized...");

    StartCoroutine(AuctionHouse.Instance.GetMyInventoryByGame(20, null, result => 
    {
      // Do whatever you need to do with the result.

      foreach (var item in result.data.getMyInventorybyGame.inventory)
      {
        if (item.gameItem.itemId.Equals("01G05C01HQ051F1PBY8N1X7Z9H"))
        {
          itemCountValueText.text = item.ItemCount.ToString();
        }
      }
    }));
  }

    public void AddValue()
    {
        var currentValue = int.Parse(itemCountValueText.text);
        var valueToAdd = Random.Range(1, 10);
        var finalValue = currentValue + valueToAdd;

        itemCountValueText.text = finalValue.ToString();

        InventoryDelta[] inventoryDeltas = 
        {
            new InventoryDelta
            {
                itemId = potionItemId,
                itemCount = finalValue
            }
        };

        StartCoroutine(AuctionHouse.Instance.PushInventory(inventoryDeltas, result => {}));
    }

    public void OpenAuctionHouse()
    {
        AuctionHouse.Instance.Open();
    }
}
