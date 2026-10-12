using UnityEngine;

public abstract class StorageBase : PartBase
{
    public int size = 16;
    /*
    public virtual void HandedItem(int index)
    {
        if (inventory[index] == null
            && PlayerHand.player.Hand == null) return;

        ItemSlot ex = PlayerHand.player.Hand;

        PlayerHand.player.Hand = inventory[index];

        inventory[index] = ex;

        Debug.Log("Ä­¿¡ " + inventory[index].id);
        Debug.Log(", ¼Õ¿¡ " + PlayerHand.player.Hand.id);
    }*/
}
