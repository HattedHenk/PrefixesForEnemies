﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Items.Tier2
{
    public class AzureNeedle : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 25;
            item.thrown = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.width = 28;
            item.height = 28;

            item.useTime = 13;
            item.useAnimation = 13;
            item.useStyle = 3;
            item.knockBack = .5f;
            item.value = 70;
            item.rare = 3;
            item.UseSound = SoundID.Item7;
            item.consumable = true;
            item.shoot = mod.ProjectileType("AzureNeedle");
            item.shootSpeed = 12f;
            item.maxStack = 999;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Azure Needle");
      Tooltip.SetDefault("Right-click to prepare multiple needles");
    }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            GItem info = item.GetGlobalItem<GItem>();
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            item.useTime = 13;
            item.useAnimation = 13;
            if (info.numNeedles <= 1 && player.altFunctionUse != 2)
            {
                return true;
            }
            else
            {
                play.typeNeedles = type;
                play.needleTime = info.numNeedles * 3;
                play.needleDamage = damage;
                player.itemAnimation = info.numNeedles * 3;
                for (int i = 0; i < info.numNeedles - 1; i++)
                {
                    if ((player.thrownCost33 && Main.rand.Next(100) < 33) || (player.thrownCost50 && Main.rand.Next(2) == 0))
                    {

                    }
                    else
                    {
                        item.stack--;
                    }
                }
                info.numNeedles = 0;
                info.timeToNeedle = 0;
                return false;
            }
            //if channeled, shoot X needles
        }
        public override bool ConsumeItem(Player player)
        {
            GItem info = item.GetGlobalItem<GItem>();
            if (player.altFunctionUse == 2 && info.numNeedles == 0)
            {
                return false;
            }
            return base.ConsumeItem(player);
        }
        public override bool CanUseItem(Player player)
        {
            GItem info = item.GetGlobalItem<GItem>();
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            if (player.altFunctionUse == 2)
            {
                item.UseSound = SoundID.Item32;
                if (info.timeToNeedle == 0)
                {
                    info.timeToNeedle = item.useTime;
                }
                if (info.numNeedles < Math.Min(6, item.stack))
                {
                    info.timeToNeedle--;
                }
                if (info.timeToNeedle == 1 && info.numNeedles < Math.Min(6, item.stack))
                {
                    info.numNeedles++;
                    if (info.numNeedles != Math.Min(6, item.stack))
                    {
                        Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 17, .3f);
                    }
                }
                if (info.numNeedles == Math.Min(6, item.stack) && info.timeToNeedle == 1)
                {
                    //play sound
                    Main.PlaySound(22, (int)player.position.X, (int)player.position.Y, 1, .3f);
                    info.timeToNeedle = item.useTime;
                    item.useTime = 18;
                    item.useAnimation = 18;
                }
                return false;
            }
            item.consumable = true;
            item.UseSound = SoundID.Item7;
            return base.CanUseItem(player);
        }
        public override void UpdateInventory(Player player)
        {
            if (player.inventory[player.selectedItem] != item)
            {
                GItem info = item.GetGlobalItem<GItem>();
                info.numNeedles = 0;
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("TopazTicket"), 1);
            recipe.SetResult(this, 200);
            recipe.AddRecipe();
        }
    }
}
