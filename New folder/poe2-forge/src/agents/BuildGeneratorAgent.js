/**
 * PoE2 Forge - Build Generator Agent
 * 
 * Takes meta analysis and game data to generate candidate builds.
 * Produces multiple build candidates that are then ranked and
 * passed to the Optimizer Agent.
 */

import { BaseAgent } from './BaseAgent.js';

export class BuildGeneratorAgent extends BaseAgent {
  constructor() {
    super('Build Generator', 'Generates candidate PoE2 builds from meta analysis');
  }

  async run(context) {
    const { gameData, metaAnalysis, userInput } = context;

    this.log('Generating candidate builds...');
    this.setProgress(10);
    await this.delay(700);

    const candidates = [];
    const recommendations = metaAnalysis.recommendations;

    for (let i = 0; i < recommendations.length; i++) {
      const rec = recommendations[i];
      this.log(`Building candidate ${i + 1}: ${rec.skillName} ${rec.archetype}`);
      this.setProgress(20 + (i / recommendations.length) * 60);

      const build = this.generateBuild(rec, gameData, userInput, metaAnalysis);
      candidates.push(build);
      await this.delay(500);
    }

    this.log(`Generated ${candidates.length} candidate builds`);
    this.setProgress(90);
    await this.delay(300);

    // Rank candidates
    candidates.sort((a, b) => b.score - a.score);
    this.log(`Top candidate: ${candidates[0].name} (score: ${candidates[0].score})`);
    this.setProgress(100);

    return candidates;
  }

  generateBuild(recommendation, gameData, userInput, meta) {
    const skill = gameData.skills.find(s => s.name === recommendation.skillName) || gameData.skills[0];
    const classData = gameData.classData;
    
    // Select support gems
    const supportGems = this.selectSupportGems(skill, meta.budgetAnalysis);
    
    // Generate gear recommendations
    const gear = this.generateGear(skill, classData, meta.budgetAnalysis, userInput);
    
    // Generate passive tree allocation
    const passives = this.allocatePassives(skill, classData, userInput);

    const build = {
      name: `${classData.name} ${skill.name} ${recommendation.archetype}`,
      class: classData.name,
      ascendancy: userInput.ascendancy !== 'any' ? userInput.ascendancy : classData.ascendancies[0]?.name,
      mainSkill: skill,
      supportGems,
      gear,
      passives,
      stats: this.calculateStats(skill, gear, passives, classData),
      score: recommendation.confidence,
      archetype: recommendation.archetype,
      reasoning: recommendation.reasoning
    };

    return build;
  }

  selectSupportGems(mainSkill, budgetAnalysis) {
    // Select support gems based on skill tags
    const supports = [];
    const gemPool = [
      { name: 'Added Fire Damage Support', tags: ['fire', 'damage', 'physical', 'melee'], gemId: 'Metadata/Items/Gems/SupportGemAddedFireDamage', text: 'Adds Extra Fire Damage based on Physical Damage' },
      { name: 'Added Cold Damage Support', tags: ['cold', 'damage', 'projectile', 'nova'], gemId: 'Metadata/Items/Gems/SupportGemAddedColdDamage', text: 'Adds Flat Cold Damage to Attacks and Spells' },
      { name: 'Added Lightning Damage Support', tags: ['lightning', 'damage', 'projectile'], gemId: 'Metadata/Items/Gems/SupportGemAddedLightningDamage', text: 'Adds Flat Lightning Damage' },
      { name: 'Concentrated Effect Support', tags: ['aoe', 'damage', 'slam', 'nova'], gemId: 'Metadata/Items/Gems/SupportGemConcentratedEffect', text: 'More Area Damage, Less Area of Effect' },
      { name: 'Increased Area of Effect Support', tags: ['aoe', 'nova', 'slam'], gemId: 'Metadata/Items/Gems/SupportGemIncreasedAreaOfEffect', text: 'Increased Area of Effect' },
      { name: 'Faster Attacks Support', tags: ['attack', 'speed', 'melee', 'travel'], gemId: 'Metadata/Items/Gems/SupportGemFasterAttacks', text: 'Supported Attack Skills have increased Attack Speed' },
      { name: 'Spell Echo Support', tags: ['spell', 'speed', 'projectile', 'nova'], gemId: 'Metadata/Items/Gems/SupportGemSpellEcho', text: 'Spells repeat an additional time with increased Cast Speed' },
      { name: 'Pierce Support', tags: ['projectile', 'pierce'], gemId: 'Metadata/Items/Gems/SupportGemPierce', text: 'Projectiles Pierce additional targets' },
      { name: 'Fork Support', tags: ['projectile'], gemId: 'Metadata/Items/Gems/SupportGemFork', text: 'Projectiles Fork on hit' },
      { name: 'Multiple Projectiles Support', tags: ['projectile'], gemId: 'Metadata/Items/Gems/SupportGemMultipleProjectiles', text: 'Fires additional projectiles' }
    ];

    // Pick top 5 support gems by relevance to main skill
    const scored = gemPool.map(gem => {
      let relevance = 0;
      gem.tags.forEach(tag => {
        if (mainSkill.tags.includes(tag)) relevance += 10;
      });
      return { ...gem, relevance };
    }).sort((a, b) => b.relevance - a.relevance);

    return scored.slice(0, 5);
  }

  generateGear(skill, classData, budgetAnalysis, userInput) {
    const gearSlots = ['Weapon', 'Off-Hand', 'Helmet', 'Body Armour', 'Gloves', 'Boots', 'Belt', 'Amulet', 'Ring 1', 'Ring 2'];
    const gear = {};

    gearSlots.forEach(slot => {
      const gearItem = this.getGearForSlot(slot, skill, budgetAnalysis, userInput);
      gear[slot] = {
        slot,
        name: gearItem.name,
        base: gearItem.base || '',
        mods: gearItem.mods || this.getModsForSlot(slot, skill, userInput),
        priority: this.getSlotPriority(slot, skill)
      };
    });

    return gear;
  }

  getGearForSlot(slot, skill, budget, userInput) {
    // If budget allows, choose unique weapons/armours
    const useUniques = ['High', 'Very High', 'Mirror'].includes(budget.tier);
    
    if (slot === 'Weapon') {
      if (useUniques && skill.tags.includes('spell')) {
        return {
          name: "Sire of Shards",
          base: "Chiming Staff",
          mods: [
            "Grants Skill: Level 20 Sigil of Power",
            "120% increased Spell Damage",
            "20% increased Cast Speed",
            "+10% to all Elemental Resistances",
            "Spells fire 4 additional Projectiles",
            "Spells fire Projectiles in a circle"
          ]
        };
      } else if (useUniques && skill.tags.includes('fire')) {
        return {
          name: "The Searing Touch",
          base: "Pyrophyte Staff",
          mods: [
            "Grants Skill: Level 20 Solar Orb",
            "120% increased Fire Damage",
            "20% increased Cast Speed",
            "100% increased Flammability Magnitude",
            "100% increased Ignite Magnitude"
          ]
        };
      }
      // Rare weapons
      const weaponBase = skill.tags.includes('spell') ? 'Voltaic Staff' : 'Broadsword';
      return {
        name: `Crafted Rare ${weaponBase}`,
        base: weaponBase,
        mods: skill.tags.includes('spell') ? 
          ["+1 to Level of all Lightning Spell Gems", "89% increased Spell Damage", "18% increased Cast Speed", "+26% Critical Damage Bonus"] :
          ["145% increased Physical Damage", "Adds 18 to 32 Physical Damage", "14% increased Attack Speed", "+220 to Accuracy Rating"]
      };
    }

    if (slot === 'Body Armour') {
      if (useUniques && userInput.tankiness >= 7) {
        return {
          name: "The Brass Dome",
          base: "Champion Cuirass",
          mods: [
            "600% increased Armour",
            "Take no Extra Damage from Critical Hits",
            "+300 to Stun Threshold",
            "-5% to all Maximum Elemental Resistances"
          ]
        };
      } else if (useUniques && userInput.features.includes('bossing')) {
        return {
          name: "Kaom's Heart",
          base: "Conqueror Plate",
          mods: [
            "+1500 to maximum Life",
            "You have no Spirit",
            "Has no Sockets"
          ]
        };
      }
      return {
        name: "Solid Rare Iron Plate",
        base: "Iron Plate",
        mods: ["+118 to maximum Life", "+45% to Fire Resistance", "+42% to Lightning Resistance", "92% increased Armour"]
      };
    }

    const tierPrefix = budget.tier === 'Mirror' ? 'Mirror-tier' :
                       budget.tier === 'Very High' ? 'High-end Crafted' :
                       budget.tier === 'High' ? 'Well-crafted' :
                       budget.tier === 'Medium' ? 'Solid Rare' :
                       'Budget Rare';
    return {
      name: `${tierPrefix} ${slot}`,
      base: slot.includes('Ring') ? 'Ruby Ring' : 'Leather Vest',
      mods: this.getModsForSlot(slot, skill, userInput)
    };
  }

  getModsForSlot(slot, skill, userInput) {
    const baseMods = [];
    
    if (['Helmet', 'Body Armour', 'Gloves', 'Boots'].includes(slot)) {
      baseMods.push('+85 to Maximum Life');
      if (userInput.tankiness >= 5) {
        baseMods.push('+35% to Fire Resistance');
        baseMods.push('+32% to Cold Resistance');
      }
    }
    
    if (slot === 'Weapon') {
      baseMods.push('Adds 10 to 20 Physical Damage');
      if (skill.tags.includes('spell')) baseMods.push('75% increased Spell Damage');
    }
    
    if (slot === 'Boots') baseMods.push('30% increased Movement Speed');
    if (slot === 'Amulet') baseMods.push('+25 to all Attributes');
    if (slot.includes('Ring')) baseMods.push('+30 to Maximum Life', '+25% Elemental Resistance');

    return baseMods;
  }

  getSlotPriority(slot, skill) {
    if (slot === 'Weapon') return 10;
    if (slot === 'Body Armour') return 9;
    if (slot === 'Helmet') return 7;
    return 5;
  }

  allocatePassives(skill, classData, userInput) {
    const totalPoints = 100;
    const allocation = {
      totalPoints,
      offense: Math.round(totalPoints * (userInput.damage / 10) * 0.6),
      defense: Math.round(totalPoints * (userInput.tankiness / 10) * 0.6),
      utility: 0,
      keystones: [],
      notableCount: 0
    };
    allocation.utility = totalPoints - allocation.offense - allocation.defense;
    allocation.notableCount = Math.round(totalPoints * 0.15);

    // Add keystones based on build archetype
    if (skill.tags.includes('crit')) allocation.keystones.push('Precise Technique');
    if (userInput.tankiness >= 8) allocation.keystones.push('Iron Reflexes');
    if (skill.tags.includes('spell')) allocation.keystones.push('Elemental Overload');

    return allocation;
  }

  calculateStats(skill, gear, passives, classData) {
    // Simplified stat calculation
    const baseLife = classData.baseLife || 50;
    const lifePer = 12;
    const estimatedLife = baseLife + (passives.defense * lifePer);

    return {
      life: estimatedLife,
      energyShield: skill.tags.includes('es') ? Math.round(estimatedLife * 0.8) : 0,
      estimatedDPS: Math.round(skill.baseDamage * 1.5 * (1 + passives.offense * 0.02)),
      fireRes: 75,
      coldRes: 75,
      lightningRes: 75,
      moveSpeed: 30,
      spirit: 100
    };
  }

  delay(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }
}
