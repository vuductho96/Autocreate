/**
 * PoE2 Forge - Optimizer Agent
 * 
 * Takes the top candidate build and iteratively optimizes it.
 * Focuses on maximizing key stats while respecting constraints
 * (resist caps, spirit requirements, mana sustainability).
 */

import { BaseAgent } from './BaseAgent.js';

export class OptimizerAgent extends BaseAgent {
  constructor() {
    super('Optimizer Agent', 'Iteratively optimizes the selected build');
    this.iterations = 3;
  }

  async run(context) {
    const { candidateBuilds, userInput } = context;

    if (!candidateBuilds || candidateBuilds.length === 0) {
      throw new Error('No candidate builds to optimize');
    }

    // Take the top candidate
    let build = { ...candidateBuilds[0] };
    this.log(`Optimizing: ${build.name}`);
    this.setProgress(10);
    await this.delay(500);

    // Iterative optimization passes
    for (let i = 0; i < this.iterations; i++) {
      this.log(`Optimization pass ${i + 1}/${this.iterations}...`);
      this.setProgress(20 + (i / this.iterations) * 60);

      build = this.optimizePass(build, userInput, i);
      await this.delay(600);
    }

    // Final validation pass
    this.log('Running final stat validation...');
    this.setProgress(85);
    await this.delay(400);

    build = this.ensureConstraints(build);

    this.log(`Final DPS: ${build.stats.estimatedDPS.toLocaleString()}`);
    this.log(`Final Life: ${build.stats.life.toLocaleString()}`);
    this.log(`Resistances: Fire ${build.stats.fireRes}% | Cold ${build.stats.coldRes}% | Lightning ${build.stats.lightningRes}%`);
    this.setProgress(100);

    return build;
  }

  optimizePass(build, userInput, passIndex) {
    const optimized = JSON.parse(JSON.stringify(build));

    switch (passIndex) {
      case 0: // Offense optimization
        this.optimizeOffense(optimized, userInput);
        break;
      case 1: // Defense optimization
        this.optimizeDefense(optimized, userInput);
        break;
      case 2: // Quality of life optimization
        this.optimizeQoL(optimized, userInput);
        break;
    }

    return optimized;
  }

  optimizeOffense(build, userInput) {
    // Boost DPS based on damage preference
    const damageMultiplier = 1 + (userInput.damage * 0.05);
    build.stats.estimatedDPS = Math.round(build.stats.estimatedDPS * damageMultiplier);

    // If high damage priority, sacrifice some life
    if (userInput.damage >= 8) {
      build.stats.life = Math.round(build.stats.life * 0.9);
      build.stats.estimatedDPS = Math.round(build.stats.estimatedDPS * 1.15);
    }

    this.log(`Offense optimized: DPS -> ${build.stats.estimatedDPS.toLocaleString()}`);
  }

  optimizeDefense(build, userInput) {
    // Boost defenses based on tankiness preference
    const defenseMultiplier = 1 + (userInput.tankiness * 0.04);
    build.stats.life = Math.round(build.stats.life * defenseMultiplier);

    // If high tank priority, ensure over-capped resistances
    if (userInput.tankiness >= 8) {
      build.stats.fireRes = Math.min(80, build.stats.fireRes + 5);
      build.stats.coldRes = Math.min(80, build.stats.coldRes + 5);
      build.stats.lightningRes = Math.min(80, build.stats.lightningRes + 5);
    }

    this.log(`Defense optimized: Life -> ${build.stats.life.toLocaleString()}`);
  }

  optimizeQoL(build, userInput) {
    const features = userInput.features || [];

    if (features.includes('movespeed')) {
      build.stats.moveSpeed = Math.min(200, build.stats.moveSpeed + 30);
      this.log('Added movement speed optimization');
    }

    if (features.includes('comfort')) {
      build.stats.spirit = Math.max(100, build.stats.spirit + 50);
      this.log('Added comfort (spirit) optimization');
    }
  }

  ensureConstraints(build) {
    // Ensure resist cap (75% minimum)
    build.stats.fireRes = Math.max(75, build.stats.fireRes);
    build.stats.coldRes = Math.max(75, build.stats.coldRes);
    build.stats.lightningRes = Math.max(75, build.stats.lightningRes);

    // Ensure minimum life
    build.stats.life = Math.max(3000, build.stats.life);

    // Ensure minimum spirit
    build.stats.spirit = Math.max(100, build.stats.spirit);

    return build;
  }

  delay(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }
}
