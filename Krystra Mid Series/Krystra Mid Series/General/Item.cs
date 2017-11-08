using Aimtec;
using Aimtec.SDK.Extensions;
using System;
using System.Linq;


namespace Krystra_Mid_Series
{
    internal static class ItemManager
    {
        internal static SpellSlot GetItemSlot(this Obj_AI_Hero source, string itemName)
        {
            if (source == null || string.IsNullOrEmpty(itemName))
            {
                return SpellSlot.Unknown;
            }

            var slot =
                source.Inventory.Slots.FirstOrDefault(
                    x => string.Equals(itemName, x.SpellName, StringComparison.CurrentCultureIgnoreCase));
            if (slot != null && slot.SpellSlot != SpellSlot.Unknown)
            {
                return slot.SpellSlot;
            }

            return SpellSlot.Unknown;
        }

        internal static bool CanUseItem(this Obj_AI_Hero source, string itemName)
        {
            if (source == null || string.IsNullOrEmpty(itemName))
            {
                return false;
            }

            var slot = source.GetItemSlot(itemName);
            if (slot != SpellSlot.Unknown)
            {
                return source.SpellBook.GetSpellState(slot) == SpellState.Ready;
            }

            return false;
        }

        internal static void UseItem(this Obj_AI_Hero source, Obj_AI_Hero target, string itemName)
        {
            if (source == null || target == null || !target.IsValidTarget() || string.IsNullOrEmpty(itemName))
            {
                return;
            }

            var slot = source.GetItemSlot(itemName);
            if (slot != SpellSlot.Unknown && source.CanUseItem(itemName))
            {
                source.SpellBook.CastSpell(slot, target);
            }
        }

        internal static void UseItem(this Obj_AI_Hero source, Vector3 position, string itemName)
        {
            if (source == null || position == Vector3.Zero || string.IsNullOrEmpty(itemName))
            {
                return;
            }

            var slot = source.GetItemSlot(itemName);
            if (slot != SpellSlot.Unknown && source.CanUseItem(itemName))
            {
                source.SpellBook.CastSpell(slot, position);
            }
        }

        internal static void UseItem(this Obj_AI_Hero source, string itemName)
        {
            if (source == null || string.IsNullOrEmpty(itemName))
            {
                return;
            }

            var slot = source.GetItemSlot(itemName);
            if (slot != SpellSlot.Unknown && source.CanUseItem(itemName))
            {
                source.SpellBook.CastSpell(slot);
            }
        }



        internal static void GetNames(this Obj_AI_Hero source)
        {
            foreach (var slot in source.Inventory.Slots)
            {
                if (!slot.SpellName.ToLower().Contains("noscript"))
                {
                    Console.WriteLine(slot.SpellName);
                }
            }
        }


    }
}