/**
 * PoE2 Forge - Coordinator Agent
 * 
 * The Coordinator manages the entire build generation pipeline.
 * It orchestrates all specialized agents in the correct order,
 * passes context between them, and handles errors gracefully.
 */

import { BaseAgent, AgentStatus } from './BaseAgent.js';
import { ResearchAgent } from './ResearchAgent.js';
import { MetaAgent } from './MetaAgent.js';
import { BuildGeneratorAgent } from './BuildGeneratorAgent.js';
import { OptimizerAgent } from './OptimizerAgent.js';
import { QAAgent } from './QAAgent.js';

export class CoordinatorAgent extends BaseAgent {
  constructor() {
    super('Coordinator', 'Orchestrates the entire build generation pipeline');
    
    this.agents = {
      research: new ResearchAgent(),
      meta: new MetaAgent(),
      generator: new BuildGeneratorAgent(),
      optimizer: new OptimizerAgent(),
      qa: new QAAgent()
    };

    this.pipeline = ['research', 'meta', 'generator', 'optimizer', 'qa'];
    this.currentStep = -1;
  }

  getAgents() {
    return this.agents;
  }

  getCurrentStepName() {
    if (this.currentStep < 0 || this.currentStep >= this.pipeline.length) return null;
    return this.pipeline[this.currentStep];
  }

  async run(context) {
    const buildContext = {
      userInput: context.userInput,
      gameData: null,
      metaAnalysis: null,
      candidateBuilds: [],
      optimizedBuild: null,
      validatedBuild: null,
      logs: []
    };

    for (let i = 0; i < this.pipeline.length; i++) {
      this.currentStep = i;
      const agentName = this.pipeline[i];
      const agent = this.agents[agentName];

      this.log(`Pipeline step ${i + 1}/${this.pipeline.length}: ${agent.name}`);
      this.setProgress(Math.round((i / this.pipeline.length) * 100));

      window.dispatchEvent(new CustomEvent('pipeline-step', {
        detail: { step: i, agentName, total: this.pipeline.length }
      }));

      try {
        const result = await agent.execute(buildContext);
        
        // Merge result into context for the next agent
        switch (agentName) {
          case 'research':
            buildContext.gameData = result;
            break;
          case 'meta':
            buildContext.metaAnalysis = result;
            break;
          case 'generator':
            buildContext.candidateBuilds = result;
            break;
          case 'optimizer':
            buildContext.optimizedBuild = result;
            break;
          case 'qa':
            buildContext.validatedBuild = result;
            break;
        }
      } catch (err) {
        this.log(`Pipeline failed at step: ${agent.name}`);
        throw err;
      }
    }

    this.currentStep = this.pipeline.length;
    this.setProgress(100);
    return buildContext;
  }
}
