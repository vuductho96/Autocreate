/**
 * PoE2 Forge - QA Agent
 * 
 * Final validation agent that checks the optimized build
 * against known game constraints and produces a quality report.
 */

import { BaseAgent } from './BaseAgent.js';

export class QAAgent extends BaseAgent {
  constructor() {
    super('QA Agent', 'Validates build against PoE2 game constraints');
  }

  async run(context) {
    const { optimizedBuild } = context;

    if (!optimizedBuild) {
      throw new Error('No optimized build to validate');
    }

    this.log('Starting final QA validation...');
    this.setProgress(10);
    await this.delay(500);

    const checks = [];

    // Check 1: Resist cap
    checks.push(this.checkResistCap(optimizedBuild));
    this.setProgress(25);
    await this.delay(300);

    // Check 2: Life pool
    checks.push(this.checkLifePool(optimizedBuild));
    this.setProgress(40);
    await this.delay(300);

    // Check 3: Spirit / Mana
    checks.push(this.checkSpirit(optimizedBuild));
    this.setProgress(55);
    await this.delay(300);

    // Check 4: DPS sanity
    checks.push(this.checkDPS(optimizedBuild));
    this.setProgress(70);
    await this.delay(300);

    // Check 5: Gear slots filled
    checks.push(this.checkGear(optimizedBuild));
    this.setProgress(85);
    await this.delay(300);

    // Check 6: Support gem count
    checks.push(this.checkSupports(optimizedBuild));

    const passed = checks.filter(c => c.passed).length;
    const total = checks.length;
    const score = Math.round((passed / total) * 100);

    this.log(`QA Score: ${score}% (${passed}/${total} checks passed)`);
    this.setProgress(100);

    return {
      ...optimizedBuild,
      qa: {
        checks,
        score,
        passed,
        total,
        verdict: score >= 80 ? 'APPROVED' : score >= 50 ? 'NEEDS REVIEW' : 'FAILED'
      }
    };
  }

  checkResistCap(build) {
    const fire = build.stats.fireRes >= 75;
    const cold = build.stats.coldRes >= 75;
    const lightning = build.stats.lightningRes >= 75;
    const passed = fire && cold && lightning;

    this.log(`Resist Cap: ${passed ? '✅ PASS' : '❌ FAIL'} (F:${build.stats.fireRes}% C:${build.stats.coldRes}% L:${build.stats.lightningRes}%)`);

    return {
      name: 'Resistance Cap',
      passed,
      details: `Fire: ${build.stats.fireRes}%, Cold: ${build.stats.coldRes}%, Lightning: ${build.stats.lightningRes}%`,
      requirement: '75% minimum for all elemental resistances'
    };
  }

  checkLifePool(build) {
    const minLife = 3000;
    const passed = build.stats.life >= minLife;

    this.log(`Life Pool: ${passed ? '✅ PASS' : '❌ FAIL'} (${build.stats.life} / ${minLife} minimum)`);

    return {
      name: 'Life Pool',
      passed,
      details: `${build.stats.life} HP`,
      requirement: `Minimum ${minLife} HP`
    };
  }

  checkSpirit(build) {
    const minSpirit = 100;
    const passed = build.stats.spirit >= minSpirit;

    this.log(`Spirit: ${passed ? '✅ PASS' : '❌ FAIL'} (${build.stats.spirit} / ${minSpirit} minimum)`);

    return {
      name: 'Spirit',
      passed,
      details: `${build.stats.spirit} Spirit`,
      requirement: `Minimum ${minSpirit} Spirit`
    };
  }

  checkDPS(build) {
    const minDPS = 10000;
    const passed = build.stats.estimatedDPS >= minDPS;

    this.log(`DPS: ${passed ? '✅ PASS' : '❌ FAIL'} (${build.stats.estimatedDPS.toLocaleString()} / ${minDPS.toLocaleString()} minimum)`);

    return {
      name: 'DPS Check',
      passed,
      details: `${build.stats.estimatedDPS.toLocaleString()} estimated DPS`,
      requirement: `Minimum ${minDPS.toLocaleString()} DPS`
    };
  }

  checkGear(build) {
    const gearSlots = Object.keys(build.gear || {});
    const passed = gearSlots.length >= 10;

    this.log(`Gear Slots: ${passed ? '✅ PASS' : '❌ FAIL'} (${gearSlots.length}/10 filled)`);

    return {
      name: 'Gear Slots',
      passed,
      details: `${gearSlots.length} slots filled`,
      requirement: '10 gear slots must be filled'
    };
  }

  checkSupports(build) {
    const supports = build.supportGems || [];
    const passed = supports.length >= 4;

    this.log(`Support Gems: ${passed ? '✅ PASS' : '❌ FAIL'} (${supports.length} linked)`);

    return {
      name: 'Support Gems',
      passed,
      details: `${supports.length} support gems`,
      requirement: 'Minimum 4 support gems'
    };
  }

  delay(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }
}
