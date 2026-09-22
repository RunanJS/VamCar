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

        inventory[index] = ex;
    }

    public bool SkillEffect(GameObject t)
    {
        throw new System.NotImplementedException();
    }
}
