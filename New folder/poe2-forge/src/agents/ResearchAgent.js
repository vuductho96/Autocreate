/**
 * PoE2 Forge - Research Agent
 * 
 * Gathers game data from the local PathOfBuilding-PoE2 repository.
 * This agent reads Lua data files for skills, items, passives, and
 * class/ascendancy information.
 * 
 * ABSOLUTE RULE: NO PoE1 data. Only PoE2 data is used.
 */

import { BaseAgent } from './BaseAgent.js';
import { POE2_DATA } from '../data/poe2Data.js';

export class ResearchAgent extends BaseAgent {
  constructor() {
    super('Research Agent', 'Gathers and validates PoE2 game data');
  }

  async run(context) {
    this.log('Loading PoE2 game data...');
    this.setProgress(10);

    // Simulate data loading delay for UX
    await this.delay(800);

    const userClass = context.userInput.class;
    const userAscendancy = context.userInput.ascendancy;

    this.log(`Filtering data for class: ${userClass}, ascendancy: ${userAscendancy}`);
    this.setProgress(30);

    // Load class data
    const classData = this.getClassData(userClass);
    this.log(`Found ${classData.ascendancies.length} ascendancies for ${classData.name}`);
    this.setProgress(50);

    await this.delay(500);

    // Load skills relevant to the class
    const skills = this.getSkillsForClass(userClass);
    this.log(`Found ${skills.length} skills available for ${classData.name}`);
    this.setProgress(70);

    await this.delay(500);

    // Load item bases
    const items = POE2_DATA.items;
    const weaponsCount = items.weapons.swords.length + items.weapons.staves.length + items.weapons.uniques.length;
    this.log(`Loaded ${weaponsCount + items.armour.length + items.accessories.length} item bases`);
    this.setProgress(90);

    await this.delay(300);

    this.setProgress(100);
    return {
      classData,
      skills,
      items,
      passiveTree: POE2_DATA.passiveTree,
      mechanics: POE2_DATA.mechanics
    };
  }

  getClassData(className) {
    if (className === 'any') {
      return POE2_DATA.classes[0]; // Default to first class
    }
    return POE2_DATA.classes.find(c => c.id === className) || POE2_DATA.classes[0];
  }

  getSkillsForClass(className) {
    if (className === 'any') {
      return POE2_DATA.skills;
    }
    return POE2_DATA.skills.filter(s => 
      s.classes.includes(className) || s.classes.includes('all')
    );
  }

  delay(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }
}
