﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using System;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class TerraForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Force");
            string tooltip = "''";

                

            if (thorium == null)
            {
                tooltip +=
@"Attacks have a chance to shock enemies with lightning
Sets your critical strike chance to 10%
Every crit will increase it by 5%
Getting hit drops your crit back down
Allows the player to dash into the enemy
Right Click to guard with your shield
You attract items from a larger range
";
            }
            else
            {
                tooltip +=
@"Sets your critical strike chance to 10%
Every crit will increase it by 5%
Getting hit drops your crit back down
";
            }

            tooltip +=
@"Attacks may inflict enemies with Lead Poisoning or Stunned
Grants immunity to fire and lava
Increases armor penetration by 10
While standing in lava, you gain 10 more armor penetration, 15% attack speed, and your attacks ignite enemies";
                
            Tooltip.SetDefault(tooltip);

            
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 11;
            item.value = 600000;
            item.shieldSlot = 5;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            //crit effect improved
            modPlayer.TerraForce = true;
            //crits
            modPlayer.TinEffect();
            //lead poison
            modPlayer.LeadEnchant = true;
            //tungsten
            modPlayer.TungstenEnchant = true;
            //lava immune (obsidian)
            modPlayer.ObsidianEffect();

            if (Fargowiltas.Instance.ThoriumLoaded)
                Thorium(player, hideVisual);
            else //because iron absorbed somewhere else
            {
                //lightning
                modPlayer.CopperEnchant = true;
                //EoC Shield
                player.dash = 2;
                //shield
                modPlayer.IronEffect();
                //magnet
                if (Soulcheck.GetValue("Iron Magnet"))
                {
                    modPlayer.IronEnchant = true;
                }
            }
        }

        private void Thorium(Player player, bool hideVisual)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);

            
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            if(Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(null, "TinEnchant");
            }
            else
            {
                recipe.AddIngredient(null, "CopperEnchant");
                recipe.AddIngredient(null, "TinEnchant");
                recipe.AddIngredient(null, "IronEnchant");
            }

            recipe.AddIngredient(null, "LeadEnchant");
            recipe.AddIngredient(null, "TungstenEnchant");
            recipe.AddIngredient(null, "ObsidianEnchant");

            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}