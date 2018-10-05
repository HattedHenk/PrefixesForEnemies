﻿using EnemyMods.Prefixes.Groups;
using Terraria;

namespace EnemyMods.Prefixes.Stats
{
    public class DangerousPrefix : NPCPrefix
    {
        public override string Type => StatsGroup.NAME;

        public override string Name => "Dangerous";

        public override float Rarity => 1.0f;

        public override void OnCreate(NPC npc)
        {
            npc.value *= 1.5f;
            npc.damage = (int)(1.4 * npc.damage);
        }
    }
}