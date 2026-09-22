using UnityEngine;

public abstract class StorageBase : MonoBehaviour, PartBase<GameObject>
{
    protected Inventory inventory = new Inventory(1);

    public virtual void HandedItem(int index)
    {
        ItemSlot hand = PlayerHand.player.Hand;
        if (inventory[index] == null
            && hand == null) return;

        ItemSlot ex = hand;

        hand = inventory[index];

        // 버그: 인벤토리에서 핸드로 반환이 안된다?

        inventory[index] = ex;
    }

    public bool SkillEffect(GameObject t)
    {
        throw new System.NotImplementedException();
    }
}
