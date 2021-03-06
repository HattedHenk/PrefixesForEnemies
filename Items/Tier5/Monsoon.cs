﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier5
{
    public class Monsoon : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 32;
            item.height = 52;
            item.channel = true;
            item.noUseGraphic = true;
            item.useStyle = 5;
            item.useAnimation = 25;
            item.useTime = 25;
            item.damage = 60;
            item.value = 80000;
            item.rare = 8;
            item.knockBack = 2;
            item.shoot = 1;
            item.useAmmo = AmmoID.Arrow;
            item.noMelee = true;
            item.shootSpeed = 7.4f;
            item.ranged = true;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Monsoon");
      Tooltip.SetDefault("");
    }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int projIndex = Projectile.NewProjectile(position.X - speedX, position.Y - speedY, speedX, speedY, type, -player.whoAmI - 256, knockBack, player.whoAmI);

            float arrowSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
            //damage is arrow index
            //knockback is arrow speed
            Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("Monsoon"), damage, arrowSpeed, player.whoAmI);
            return false;
        }
    public override void AddRecipes()
    {
        ModRecipe recipe = new ModRecipe(mod);
        recipe.AddIngredient(mod.ItemType("ChoiceToken"), 1);
        recipe.AddIngredient(mod.ItemType("RubyTicket"), 3);
        recipe.SetResult(this);
        recipe.AddRecipe();
    }
}
}
