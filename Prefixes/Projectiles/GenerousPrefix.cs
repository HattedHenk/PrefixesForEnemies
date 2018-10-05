﻿using EnemyMods.Prefixes.Groups;
using System;
using Terraria;

namespace EnemyMods.Prefixes.Projectiles
{
    public class GenerousPrefix : NPCPrefix
    {
        public override string Type => ProjectilesGroup.NAME;

        public override string Name => "Generous";

        public override float Rarity => 0.5f;

        public int TriggerTimer { get; private set; }

        public override bool IsAllowed(NPC npc) => npc.value > 0;

        public override void OnCreate(NPC npc)
        {
            npc.value *= 3;
        }

        public override void AI(NPC npc)
        {
            if (npc.target == 255)
            {
                return;
            }
            Player target = Main.player[npc.target];

            int distance = (int)Math.Sqrt((npc.Center.X - target.Center.X) * (npc.Center.X - target.Center.X) + (npc.Center.Y - target.Center.Y) * (npc.Center.Y - target.Center.Y));

            if (distance < 450)
            {
                TriggerTimer++;
                if (TriggerTimer >= 240 && Collision.CanHitLine(target.position, target.width, target.height, npc.position, npc.width, npc.height) && npc.value > 0)
                {
                    int rand = Main.rand.Next(0, 100);
                    int type;
                    float mult = .5f;
                    if (rand >= 90 && npc.value >= 1000000)
                    {
                        mult = 3;
                        type = 161;
                        npc.value -= 1000000;
                    }
                    else if (rand >= 75 && npc.value >= 10000)
                    {
                        mult = 2f;
                        type = 160;
                        npc.value -= 10000;
                    }
                    else if (rand >= 40 && npc.value >= 100)
                    {
                        mult = 1f;
                        type = 159;
                        npc.value -= 100;
                    }
                    else
                    {
                        type = 158;
                        npc.value -= 1;
                    }
                    int p = Projectile.NewProjectile(npc.position.X, npc.position.Y, -(npc.position.X - target.position.X) / distance * 9, -(npc.position.Y - target.position.Y) / distance * 9, type, (int)((npc.damage * mult) * ProjectilesGroup.DamageMultiplier), 0);
                    Main.projectile[p].hostile = true;
                    Main.projectile[p].friendly = false;
                    if (Main.netMode == 2)
                    {
                        Main.projectile[p].alpha = 255;
                    }
                    TriggerTimer = 0;
                }
            }
        }
    }
}