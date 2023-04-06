using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemBase
{
    public void SetItemData(HomeData itemData, int itemIndex);
    public void Init();
}
