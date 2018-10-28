using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    #region Singleton
    public static Inventory Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("More than one instance of Inventory ... ?");
            return;
        }

        Instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;

    public List<Item> items = new List<Item>();

    public bool Add(Item item)
    {
        if(!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                Debug.Log("Not enough space in inventory");
                return false;
            }
            items.Add(item);

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
            return true;
        }
        return false;
    }

    public void Remove(Item item)
    {
        if (!item.isDefaultItem)
        {
            items.Remove(item);

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
    }
}
