/**
 * PoE2 Forge - Agent Architecture
 * 
 * This module defines the multi-agent pipeline for AI build compilation.
 * Each agent is a specialized processor that handles a specific phase
 * of the build generation workflow.
 */

// Agent status enum
export const AgentStatus = {
  IDLE: 'idle',
  RUNNING: 'running',
  COMPLETE: 'complete',
  ERROR: 'error',
  WAITING: 'waiting'
};

/**
 * Base Agent class - all specialized agents extend this.
 */
export class BaseAgent {
  constructor(name, description) {
    this.name = name;
    this.description = description;
    this.status = AgentStatus.IDLE;
    this.progress = 0;
    this.logs = [];
    this.result = null;
    this.error = null;
  }

  log(message) {
    const entry = { timestamp: Date.now(), agent: this.name, message };
    this.logs.push(entry);
    // Dispatch event for UI updates
    window.dispatchEvent(new CustomEvent('agent-log', { detail: entry }));
  }

  async execute(context) {
    this.status = AgentStatus.RUNNING;
    this.progress = 0;
    this.log(`Starting ${this.name}...`);
    
    try {
      this.result = await this.run(context);
      this.status = AgentStatus.COMPLETE;
      this.progress = 100;
      this.log(`${this.name} completed successfully.`);
      return this.result;
    } catch (err) {
      this.status = AgentStatus.ERROR;
      this.error = err.message;
      this.log(`ERROR: ${err.message}`);
      throw err;
    }
  }

  // Override in subclasses
  async run(context) {
    throw new Error(`${this.name}.run() not implemented`);
  }

  setProgress(pct) {
    this.progress = Math.min(100, Math.max(0, pct));
    window.dispatchEvent(new CustomEvent('agent-progress', {
      detail: { agent: this.name, progress: this.progress }
    }));
  }
}
