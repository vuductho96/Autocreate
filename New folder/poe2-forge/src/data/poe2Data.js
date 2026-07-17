/**
 * PoE2 Forge - Game Data Module
 * 
 * Contains all PoE2-specific game data.
 * ABSOLUTE RULE: This file contains ONLY Path of Exile 2 data.
 * NO Path of Exile 1 data is permitted.
 */

export const POE2_DATA = {
  classes: [
    {
      id: 'warrior',
      name: 'Warrior',
      baseLife: 66,
      baseStr: 26,
      baseDex: 14,
      baseInt: 14,
      ascendancies: [
        { id: 'titan', name: 'Titan', description: 'Master of raw physical power and endurance' },
        { id: 'warbringer', name: 'Warbringer', description: 'Master of warcries and shouts' }
      ]
    },
    {
      id: 'ranger',
      name: 'Ranger',
      baseLife: 53,
      baseStr: 14,
      baseDex: 26,
      baseInt: 14,
      ascendancies: [
        { id: 'deadeye', name: 'Deadeye', description: 'Precision-based ranged specialist' },
        { id: 'pathfinder', name: 'Pathfinder', description: 'Flask and nature-based specialist' }
      ]
    },
    {
      id: 'huntress',
      name: 'Huntress',
      baseLife: 53,
      baseStr: 14,
      baseDex: 26,
      baseInt: 14,
      ascendancies: [
        { id: 'beastlord', name: 'Beastlord', description: 'Commands animal companions' },
        { id: 'amazon', name: 'Amazon', description: 'Javelin and spear specialist' }
      ]
    },
    {
      id: 'sorceress',
      name: 'Sorceress',
      baseLife: 44,
      baseStr: 14,
      baseDex: 14,
      baseInt: 26,
      ascendancies: [
        { id: 'stormweaver', name: 'Stormweaver', description: 'Lightning and elemental specialist' },
        { id: 'chronomancer', name: 'Chronomancer', description: 'Time manipulation specialist' }
      ]
    },
    {
      id: 'mercenary',
      name: 'Mercenary',
      baseLife: 53,
      baseStr: 20,
      baseDex: 20,
      baseInt: 14,
      ascendancies: [
        { id: 'witchhunter', name: 'Witchhunter', description: 'Anti-magic combat specialist' },
        { id: 'gemling_legionnaire', name: 'Gemling Legionnaire', description: 'Crossbow and explosive specialist' }
      ]
    },
    {
      id: 'monk',
      name: 'Monk',
      baseLife: 53,
      baseStr: 14,
      baseDex: 20,
      baseInt: 20,
      ascendancies: [
        { id: 'invoker', name: 'Invoker', description: 'Elemental martial artist' },
        { id: 'acolyte_of_chayula', name: 'Acolyte of Chayula', description: 'Shadow and chaos specialist' }
      ]
    }
  ],

  skills: [
    // Warrior Skills
    { name: 'Leap Slam', classes: ['warrior', 'all'], tags: ['attack', 'melee', 'aoe', 'slam', 'travel'], baseDamage: 180, baseScore: 75, gemId: 'Metadata/Items/Gems/SkillGemLeapSlam' },
    { name: 'Shield Charge', classes: ['warrior'], tags: ['attack', 'melee', 'mobile', 'defensive', 'travel'], baseDamage: 110, baseScore: 68, gemId: 'Metadata/Items/Gems/SkillGemShieldCharge' },
    { name: 'Sunder', classes: ['warrior'], tags: ['attack', 'melee', 'aoe', 'physical'], baseDamage: 165, baseScore: 78, gemId: 'Metadata/Items/Gems/SkillGemSunder' },
    { name: 'Earthquake', classes: ['warrior'], tags: ['attack', 'melee', 'aoe', 'slam'], baseDamage: 220, baseScore: 85, gemId: 'Metadata/Items/Gems/SkillGemEarthquake' },

    // Ranger Skills
    { name: 'Lightning Arrow', classes: ['ranger'], tags: ['attack', 'projectile', 'aoe', 'lightning'], baseDamage: 130, baseScore: 80, gemId: 'Metadata/Items/Gems/SkillGemLightningArrow' },
    { name: 'Rain of Arrows', classes: ['ranger'], tags: ['attack', 'projectile', 'aoe'], baseDamage: 125, baseScore: 75, gemId: 'Metadata/Items/Gems/SkillGemRainOfArrows' },
    { name: 'Tornado Shot', classes: ['ranger'], tags: ['attack', 'projectile', 'aoe'], baseDamage: 135, baseScore: 84, gemId: 'Metadata/Items/Gems/SkillGemTornadoShot' },

    // Huntress Skills
    { name: 'Javelin Throw', classes: ['huntress'], tags: ['attack', 'projectile', 'pierce'], baseDamage: 145, baseScore: 76, gemId: 'Metadata/Items/Gems/SkillGemJavelinThrow' },
    { name: 'Spear Thrust', classes: ['huntress'], tags: ['attack', 'melee', 'pierce'], baseDamage: 160, baseScore: 72, gemId: 'Metadata/Items/Gems/SkillGemSpearThrust' },

    // Sorceress Skills
    { name: 'Ice Nova', classes: ['sorceress'], tags: ['spell', 'aoe', 'cold', 'nova'], baseDamage: 150, baseScore: 78, gemId: 'Metadata/Items/Gems/SkillGemIceNova' },
    { name: 'Fireball', classes: ['sorceress'], tags: ['spell', 'projectile', 'fire'], baseDamage: 170, baseScore: 80, gemId: 'Metadata/Items/Gems/SkillGemFireball' },
    { name: 'Lightning Bolt', classes: ['sorceress'], tags: ['spell', 'lightning', 'projectile'], baseDamage: 195, baseScore: 85, gemId: 'Metadata/Items/Gems/SkillGemLightningBolt' },
    { name: 'Meteor', classes: ['sorceress'], tags: ['spell', 'fire', 'aoe'], baseDamage: 260, baseScore: 90, gemId: 'Metadata/Items/Gems/SkillGemMeteor' },

    // Mercenary Skills
    { name: 'Crossbow Shot', classes: ['mercenary'], tags: ['attack', 'projectile'], baseDamage: 130, baseScore: 72, gemId: 'Metadata/Items/Gems/SkillGemCrossbowShot' },
    { name: 'Explosive Bolt', classes: ['mercenary'], tags: ['attack', 'projectile', 'aoe', 'fire'], baseDamage: 155, baseScore: 82, gemId: 'Metadata/Items/Gems/SkillGemExplosiveBolt' },

    // Monk Skills
    { name: 'Whirlwind Kick', classes: ['monk'], tags: ['attack', 'melee', 'aoe', 'mobile'], baseDamage: 145, baseScore: 78, gemId: 'Metadata/Items/Gems/SkillGemWhirlwindKick' },
    { name: 'Palm Strike', classes: ['monk'], tags: ['attack', 'melee'], baseDamage: 125, baseScore: 74, gemId: 'Metadata/Items/Gems/SkillGemPalmStrike' }
  ],

  supportGems: [
    { name: 'Added Fire Damage Support', tags: ['fire', 'damage'], gemId: 'Metadata/Items/Gems/SupportGemAddedFireDamage', text: 'Adds Extra Fire Damage based on Physical Damage' },
    { name: 'Added Cold Damage Support', tags: ['cold', 'damage'], gemId: 'Metadata/Items/Gems/SupportGemAddedColdDamage', text: 'Adds Flat Cold Damage to Attacks and Spells' },
    { name: 'Added Lightning Damage Support', tags: ['lightning', 'damage'], gemId: 'Metadata/Items/Gems/SupportGemAddedLightningDamage', text: 'Adds Flat Lightning Damage' },
    { name: 'Concentrated Effect Support', tags: ['aoe', 'damage'], gemId: 'Metadata/Items/Gems/SupportGemConcentratedEffect', text: 'More Area Damage, Less Area of Effect' },
    { name: 'Increased Area of Effect Support', tags: ['aoe'], gemId: 'Metadata/Items/Gems/SupportGemIncreasedAreaOfEffect', text: 'Increased Area of Effect' },
    { name: 'Faster Attacks Support', tags: ['attack', 'speed'], gemId: 'Metadata/Items/Gems/SupportGemFasterAttacks', text: 'Supported Attack Skills have increased Attack Speed' },
    { name: 'Spell Echo Support', tags: ['spell', 'speed'], gemId: 'Metadata/Items/Gems/SupportGemSpellEcho', text: 'Spells repeat an additional time with increased Cast Speed' },
    { name: 'Pierce Support', tags: ['projectile'], gemId: 'Metadata/Items/Gems/SupportGemPierce', text: 'Projectiles Pierce additional targets' },
    { name: 'Fork Support', tags: ['projectile'], gemId: 'Metadata/Items/Gems/SupportGemFork', text: 'Projectiles Fork on hit' },
    { name: 'Multiple Projectiles Support', tags: ['projectile'], gemId: 'Metadata/Items/Gems/SupportGemMultipleProjectiles', text: 'Fires additional projectiles' }
  ],

  items: {
    weapons: {
      swords: [
        { name: 'Golden Blade', type: 'One Hand Sword', implicit: '+(16-24) to all Attributes', stats: 'Physical Damage: 3-28, Crit Chance: 5%, APS: 1.1' },
        { name: 'Broadsword', type: 'One Hand Sword', implicit: 'None', stats: 'Physical Damage: 8-13, Crit Chance: 5%, APS: 1.6' },
        { name: 'Sickle Sword', type: 'One Hand Sword', implicit: 'None', stats: 'Physical Damage: 18-38, Crit Chance: 5%, APS: 1.5' }
      ],
      staves: [
        { name: 'Reflecting Staff', type: 'Staff', implicit: 'Grants Skill: Mirror of Refraction', stats: 'Spell Damage, Cast Speed' },
        { name: 'Voltaic Staff', type: 'Staff', implicit: 'Grants Skill: Lightning Bolt & Spark', stats: 'Elemental Damage, Shock Chance' },
        { name: 'Permafrost Staff', type: 'Staff', implicit: 'Grants Skill: Heart of Ice & Icestorm', stats: 'Cold Damage, Freeze Chance' }
      ],
      uniques: [
        {
          name: "Sire of Shards",
          base: "Chiming Staff",
          mods: [
            "Grants Skill: Level 20 Sigil of Power",
            "(80-120)% increased Spell Damage",
            "(10-20)% increased Cast Speed",
            "+(5-10)% to all Elemental Resistances",
            "Spells fire 4 additional Projectiles",
            "Spells fire Projectiles in a circle"
          ]
        },
        {
          name: "The Searing Touch",
          base: "Pyrophyte Staff",
          mods: [
            "Grants Skill: Level 20 Solar Orb",
            "(80-120)% increased Fire Damage",
            "(10-20)% increased Cast Speed",
            "100% increased Flammability Magnitude",
            "100% increased Ignite Magnitude"
          ]
        },
        {
          name: "Kaom's Heart",
          base: "Conqueror Plate",
          mods: [
            "+1500 to maximum Life",
            "You have no Spirit",
            "Has no Sockets"
          ]
        },
        {
          name: "The Brass Dome",
          base: "Champion Cuirass",
          mods: [
            "(500-600)% increased Armour",
            "Take no Extra Damage from Critical Hits",
            "+200 to Stun Threshold",
            "-5% to all Maximum Elemental Resistances"
          ]
        }
      ]
    },
    armour: [
      { name: 'Iron Plate', type: 'Body Armour', defense: 'Armour', baseValue: 200 },
      { name: 'Leather Vest', type: 'Body Armour', defense: 'Evasion', baseValue: 180 },
      { name: 'Silk Robe', type: 'Body Armour', defense: 'Energy Shield', baseValue: 160 },
      { name: 'Iron Helm', type: 'Helmet', defense: 'Armour', baseValue: 100 },
      { name: 'Iron Gauntlets', type: 'Gloves', defense: 'Armour', baseValue: 80 },
      { name: 'Iron Greaves', type: 'Boots', defense: 'Armour', baseValue: 80 },
      { name: 'Leather Belt', type: 'Belt', defense: 'Life', baseValue: 40 }
    ],
    accessories: [
      { name: 'Gold Amulet', type: 'Amulet', implicit: '+16% Item Rarity' },
      { name: 'Iron Ring', type: 'Ring', implicit: '+1-4 Physical Damage' },
      { name: 'Ruby Ring', type: 'Ring', implicit: '+25% Fire Resistance' },
      { name: 'Sapphire Ring', type: 'Ring', implicit: '+25% Cold Resistance' },
      { name: 'Topaz Ring', type: 'Ring', implicit: '+25% Lightning Resistance' }
    ]
  },

  passiveTree: {
    totalNodes: 1500,
    keystones: [
      { name: 'Iron Reflexes', description: 'Converts Evasion to Armour' },
      { name: 'Precise Technique', description: 'No Critical Strikes. Hits can\'t be Evaded' },
      { name: 'Elemental Overload', description: 'Elemental damage increased based on accuracy' },
      { name: 'Pain Attunement', description: '30% more Spell Damage when on Low Life' },
      { name: 'Chaos Inoculation', description: 'Maximum Life becomes 1. Immune to Chaos' }
    ]
  },

  mechanics: {
    resistCap: 75,
    maxResistCap: 90,
    spiritBase: 100,
    manaBase: 50,
    movementSpeedBase: 0,
    critChanceBase: 5,
    critMultiplierBase: 150
  }
};

// Helper maps for PoB2 Class and Ascendancy IDs
const CLASS_ID_MAP = {
  'Witch': 1,
  'Ranger': 2,
  'Warrior': 6,
  'Sorceress': 7,
  'Huntress': 8,
  'Mercenary': 9,
  'Monk': 10,
  'Druid': 11
};

function getAscendClassId(ascendName) {
  if (!ascendName || ascendName === 'None' || ascendName === 'any') return 0;
  const map = {
    'titan': 1, 'warbringer': 2, 'smith of kitava': 3,
    'deadeye': 1, 'pathfinder': 2,
    'beastlord': 1, 'amazon': 2, 'ritualist': 3, 'spirit walker': 2,
    'stormweaver': 1, 'chronomancer': 2, 'disciple of varashta': 3,
    'witchhunter': 1, 'gemling legionnaire': 2, 'tactician': 3,
    'invoker': 1, 'acolyte of chayula': 2, 'martial artist': 3
  };
  return map[ascendName.toLowerCase()] || 0;
}

// Generates XML string for Path of Building 2
export function generatePoB2XML(build) {
  const classId = CLASS_ID_MAP[build.class] || 2;
  const ascendId = getAscendClassId(build.ascendancy);

  // Construct PoB2 XML structure
  let xml = `<?xml version="1.0" encoding="UTF-8"?>
<PathOfBuilding2>
  <Build ascendClassName="${build.ascendancy || 'None'}" viewMode="ITEMS" characterLevelAutoMode="true" targetVersion="0_1" mainSocketGroup="1" level="90" className="${build.class}">
    <PlayerStat stat="AverageDamage" value="1000"/>
    <PlayerStat stat="Speed" value="1.5"/>
    <PlayerStat stat="PreEffectiveCritChance" value="5"/>
    <PlayerStat stat="CritMultiplier" value="150"/>
    <PlayerStat stat="Life" value="${build.stats.life}"/>
    <PlayerStat stat="FireResist" value="${build.stats.fireRes}"/>
    <PlayerStat stat="ColdResist" value="${build.stats.coldRes}"/>
    <PlayerStat stat="LightningResist" value="${build.stats.lightningRes}"/>
  </Build>
  <TreeView showStatDifferences="true" zoomLevel="3" zoomY="0" zoomX="0" searchStr=""/>
  <Skills showSupportGemTypes="ALL" showLegacyGems="false" defaultGemLevel="normalMaximum" sortGemsByDPSField="CombinedDPS" defaultGemQuality="0" sortGemsByDPS="true" activeSkillSet="1">
    <SkillSet id="1">`;

  // Append Skill & Gem setup wrapped inside SkillSet
  if (build.mainSkill) {
    xml += `
      <Skill mainActiveSkillCalcs="1" enabled="true" slot="Weapon 1" mainActiveSkill="1">
        <Gem enableGlobal2="false" level="20" enableGlobal1="true" skillId="${build.mainSkill.gemId || 'Metadata/Items/Gems/SkillGemSunder'}" quality="20" enabled="true" nameSpec="${build.mainSkill.name}"/>`;
    if (build.supportGems) {
      build.supportGems.forEach(gem => {
        xml += `
        <Gem enableGlobal2="false" level="20" enableGlobal1="true" skillId="${gem.gemId || 'Metadata/Items/Gems/SupportGemFasterAttacks'}" quality="20" enabled="true" nameSpec="${gem.name}"/>`;
      });
    }
    xml += `
      </Skill>`;
  }

  xml += `
    </SkillSet>
  </Skills>
  <Items showStatDifferences="true" activeItemSet="1" useSecondWeaponSet="nil">`;

  // Append Items
  let itemIdCounter = 1;
  let slotsXml = '';
  if (build.gear) {
    Object.keys(build.gear).forEach(slotName => {
      const g = build.gear[slotName];
      
      // Map slot names to PoB2 format spec
      let mappedSlotName = slotName;
      if (slotName === 'Weapon') mappedSlotName = 'Weapon 1';
      else if (slotName === 'Off-Hand') mappedSlotName = 'Weapon 2';

      xml += `
    <Item id="${itemIdCounter}">
      Rarity: UNIQUE
      ${g.name}
      ${g.base || 'Broadsword'}
      Unique ID: ${10000 + itemIdCounter}
      Item Level: 85
      --------
      ${g.mods ? g.mods.join('\n      ') : ''}
    </Item>`;
      
      slotsXml += `
      <Slot name="${mappedSlotName}" itemId="${itemIdCounter}" active="true"/>`;
      itemIdCounter++;
    });
  }

  xml += `
    <ItemSet id="1" title="Default" useSecondWeaponSet="nil">${slotsXml}
    </ItemSet>
  </Items>
  <Tree activeSpec="1">
    <Spec nodes="" treeVersion="0_5" masteryEffects="" classId="${classId}" ascendClassId="${ascId}" secondaryAscendClassId="nil" ascendancyInternalId="" classInternalId="${classId}">
      <URL>
        https://www.pathofexile.com/passive-skill-tree/AAAABGIAAAAA
      </URL>
      <Sockets/>
      <Overrides>
        <AttributeOverride dexNodes="" intNodes="" strNodes=""/>
      </Overrides>
    </Spec>
  </Tree>
</PathOfBuilding2>`;

  return xml;
}

import { zlibSync } from 'fflate';

// Base64 encode for XML import string using true Zlib Deflate
export function exportToPoBCode(build) {
  const xml = generatePoB2XML(build);
  try {
    const enc = new TextEncoder();
    const data = enc.encode(xml);
    // Use zlibSync instead of deflateSync to include zlib header (0x78 0x9C) and checksum
    const compressed = zlibSync(data, { level: 9 });
    
    let binary = '';
    const len = compressed.byteLength;
    for (let i = 0; i < len; i++) {
      binary += String.fromCharCode(compressed[i]);
    }
    const base64 = btoa(binary);
    // Replace + with - and / with _ to make it URL-safe as PoB expects
    return base64.replace(/\+/g, '-').replace(/\//g, '_');
  } catch(e) {
    console.error("Failed to generate zlib compressed PoB code:", e);
    // Fallback to simple uncompressed url-safe base64
    try {
      return btoa(unescape(encodeURIComponent(xml))).replace(/\+/g, '-').replace(/\//g, '_');
    } catch(err) {
      return xml;
    }
  }
}

// Ascendancy map for easy lookup
export const ASCENDANCY_MAP = {};
POE2_DATA.classes.forEach(cls => {
  ASCENDANCY_MAP[cls.id] = cls.ascendancies;
});
