/**
 * PoE2 Forge - Meta Agent
 * 
 * Analyzes the current meta to determine which builds, skills,
 * and strategies are performing well. Uses the research data
 * and user preferences to create a strategic analysis.
 */

import { BaseAgent } from './BaseAgent.js';

export class MetaAgent extends BaseAgent {
  constructor() {
    super('Meta Agent', 'Analyzes current PoE2 meta for build viability');
  }

  async run(context) {
    const { gameData, userInput } = context;
    
    this.log('Analyzing current PoE2 meta...');
    this.setProgress(10);
    await this.delay(600);

    // Score each skill based on user preferences
    const scoredSkills = this.scoreSkills(gameData.skills, userInput);
    this.log(`Scored ${scoredSkills.length} skills against user preferences`);
    this.setProgress(40);
    await this.delay(500);

    // Determine archetype recommendations
    const archetypes = this.determineArchetypes(userInput);
    this.log(`Recommended archetypes: ${archetypes.map(a => a.name).join(', ')}`);
    this.setProgress(60);
    await this.delay(400);

    // Analyze budget constraints
    const budgetAnalysis = this.analyzeBudget(userInput.budget);
    this.log(`Budget tier: ${budgetAnalysis.tier} - ${budgetAnalysis.description}`);
    this.setProgress(80);
    await this.delay(300);

    // Build meta report
    const metaReport = {
      topSkills: scoredSkills.slice(0, 5),
      archetypes,
      budgetAnalysis,
      recommendations: this.generateRecommendations(scoredSkills, archetypes, userInput)
    };

    this.log(`Generated ${metaReport.recommendations.length} build recommendations`);
    this.setProgress(100);

    return metaReport;
  }

  scoreSkills(skills, userInput) {
    const features = userInput.features || [];
    
    return skills.map(skill => {
      let score = skill.baseScore || 50;

      // Adjust based on tankiness vs damage preference
      if (userInput.tankiness > 7) {
        score += skill.tags.includes('defensive') ? 15 : -5;
      }
      if (userInput.damage > 7) {
        score += skill.tags.includes('damage') ? 15 : -5;
      }

      // Feature matching
      if (features.includes('bossing') && skill.tags.includes('single-target')) score += 10;
      if (features.includes('mapping') && skill.tags.includes('aoe')) score += 10;
      if (features.includes('comfort') && skill.tags.includes('simple')) score += 10;
      if (features.includes('movespeed') && skill.tags.includes('mobile')) score += 8;

      return { ...skill, metaScore: score };
    }).sort((a, b) => b.metaScore - a.metaScore);
  }

  determineArchetypes(userInput) {
    const archetypes = [];
    const tank = userInput.tankiness;
    const dmg = userInput.damage;

    if (tank >= 7 && dmg <= 4) {
      archetypes.push({ name: 'Tank', description: 'Maximum survivability, moderate damage' });
    }
    if (dmg >= 7 && tank <= 4) {
      archetypes.push({ name: 'Glass Cannon', description: 'Maximum damage, rely on positioning' });
    }
    if (tank >= 5 && dmg >= 5) {
      archetypes.push({ name: 'Balanced', description: 'Good mix of offense and defense' });
    }
    if (tank >= 7 && dmg >= 7) {
      archetypes.push({ name: 'Juggernaut', description: 'High investment build with both offense and defense' });
    }
    
    if (archetypes.length === 0) {
      archetypes.push({ name: 'All-Rounder', description: 'Flexible build adaptable to content' });
    }

    return archetypes;
  }

  analyzeBudget(budget) {
    const budgetTiers = {
      starter: { tier: 'Budget', value: 0, description: 'League start viable, no required uniques' },
      '10div': { tier: 'Low', value: 10, description: 'Basic gearing with some key uniques' },
      '20div': { tier: 'Medium', value: 20, description: 'Well-rounded gear setup' },
      '50div': { tier: 'High', value: 50, description: 'Optimized gear with crafted rares' },
      '100div': { tier: 'Very High', value: 100, description: 'Min-maxed gear setup' },
      unlimited: { tier: 'Mirror', value: 999, description: 'Best in slot everything' }
    };
    return budgetTiers[budget] || budgetTiers.starter;
  }

  generateRecommendations(scoredSkills, archetypes, userInput) {
    const topSkills = scoredSkills.slice(0, 3);
    return topSkills.map(skill => ({
      skillName: skill.name,
      archetype: archetypes[0]?.name || 'Balanced',
      confidence: Math.min(95, skill.metaScore),
      reasoning: `${skill.name} scores high for ${archetypes[0]?.name || 'balanced'} playstyle with ${userInput.league} league constraints.`
    }));
  }

  delay(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }
}
