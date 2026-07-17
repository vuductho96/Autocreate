import './style.css';
import { ASCENDANCY_MAP } from './data/poe2Data.js';
import { CoordinatorAgent } from './agents/CoordinatorAgent.js';

// ─── App State ───
const state = {
  currentView: 'forge',
  isGenerating: false,
  buildResult: null,
  coordinator: new CoordinatorAgent()
};

// ─── Render App ───
function renderApp() {
  document.querySelector('#app').innerHTML = `
    <div class="app-container">
      ${renderSidebar()}
      <div class="main-content">
        ${renderContentHeader()}
        <div class="content-body">
          ${state.currentView === 'forge' ? renderForgeView() : ''}
          ${state.currentView === 'results' ? renderResultsView() : ''}
        </div>
      </div>
    </div>
  `;
  bindEvents();
}

// ─── Sidebar ───
function renderSidebar() {
  return `
    <aside class="sidebar">
      <div class="sidebar-header">
        <h2>PoE2 Forge</h2>
        <div class="subtitle">AI Build Compiler</div>
      </div>
      <nav class="sidebar-nav">
        <button class="nav-btn ${state.currentView === 'forge' ? 'active' : ''}" data-view="forge">
          <span class="icon">⚒️</span> New Build
        </button>
        <button class="nav-btn ${state.currentView === 'results' ? 'active' : ''}" data-view="results" ${!state.buildResult ? 'disabled style="opacity:0.4;pointer-events:none;"' : ''}>
          <span class="icon">📊</span> Build Result
        </button>
      </nav>
      <div class="sidebar-footer">
        PoE2 Forge v1.0 • Powered by AI<br/>
        Path of Exile 2 Only
      </div>
    </aside>
  `;
}

// ─── Content Header ───
function renderContentHeader() {
  const titles = {
    forge: { title: 'Forge Your Build', sub: 'Define your playstyle and let the AI compile the optimal build.' },
    results: { title: 'Build Results', sub: 'Your AI-compiled build is ready for review.' }
  };
  const t = titles[state.currentView] || titles.forge;
  return `
    <div class="content-header">
      <h1>${t.title}</h1>
      <p>${t.sub}</p>
    </div>
  `;
}

// ─── Forge View ───
function renderForgeView() {
  return `
    <div class="fade-in">
      <!-- Class & Config -->
      <div class="panel">
        <div class="panel-header">
          <h3>⚔️ Character Setup</h3>
        </div>
        <div class="grid-4">
          <div class="form-group">
            <label for="class-select">Class</label>
            <select id="class-select">
              <option value="any">Any Class</option>
              <option value="warrior">Warrior</option>
              <option value="ranger">Ranger</option>
              <option value="huntress">Huntress</option>
              <option value="sorceress">Sorceress</option>
              <option value="mercenary">Mercenary</option>
              <option value="monk">Monk</option>
            </select>
          </div>
          <div class="form-group">
            <label for="ascendancy-select">Ascendancy</label>
            <select id="ascendancy-select">
              <option value="any">Any Ascendancy</option>
            </select>
          </div>
          <div class="form-group">
            <label for="league-select">League</label>
            <select id="league-select">
              <option value="trade">Trade League</option>
              <option value="ssf">SSF</option>
              <option value="hardcore">Hardcore</option>
            </select>
          </div>
          <div class="form-group">
            <label for="budget-select">Budget</label>
            <select id="budget-select">
              <option value="starter">League Starter</option>
              <option value="10div">10 Divine</option>
              <option value="20div">20 Divine</option>
              <option value="50div">50 Divine</option>
              <option value="100div">100 Divine</option>
              <option value="unlimited">Unlimited</option>
            </select>
          </div>
        </div>
      </div>

      <!-- Playstyle -->
      <div class="panel">
        <div class="panel-header">
          <h3>🎯 Playstyle Preferences</h3>
        </div>
        <div class="grid-2">
          <div class="slider-container">
            <div class="slider-header">
              <label for="tank-slider">🛡️ Tankiness</label>
              <span class="slider-value" id="tank-value">5</span>
            </div>
            <input type="range" id="tank-slider" min="0" max="10" value="5">
          </div>
          <div class="slider-container">
            <div class="slider-header">
              <label for="damage-slider">⚔️ Damage</label>
              <span class="slider-value" id="damage-value">5</span>
            </div>
            <input type="range" id="damage-slider" min="0" max="10" value="5">
          </div>
        </div>

        <hr class="divider" />

        <label style="margin-bottom: 8px; display: block;">Features & Mechanics</label>
        <div class="grid-3">
          <label class="checkbox-group"><input type="checkbox" name="feature" value="bossing"> 🎯 Bossing</label>
          <label class="checkbox-group"><input type="checkbox" name="feature" value="mapping"> 🗺️ Mapping</label>
          <label class="checkbox-group"><input type="checkbox" name="feature" value="comfort"> ✨ Comfort</label>
          <label class="checkbox-group"><input type="checkbox" name="feature" value="movespeed"> 💨 Movement Speed</label>
          <label class="checkbox-group"><input type="checkbox" name="feature" value="onebutton"> 🔘 One Button</label>
          <label class="checkbox-group"><input type="checkbox" name="feature" value="explosion"> 💥 Explosion</label>
          <label class="checkbox-group"><input type="checkbox" name="feature" value="noflask"> 🧪 No Flask Piano</label>
          <label class="checkbox-group"><input type="checkbox" name="feature" value="noweaponswap"> ⚔️ No Weapon Swap</label>
          <label class="checkbox-group"><input type="checkbox" name="feature" value="mirror"> 💎 Mirror Tier OK</label>
        </div>
      </div>

      <!-- Pipeline (shown during generation) -->
      <div id="pipeline-section" class="panel section-hidden">
        <div class="panel-header">
          <h3>🔄 AI Pipeline</h3>
          <div class="spinner" id="pipeline-spinner"></div>
        </div>
        <div class="pipeline-container" id="pipeline-nodes"></div>
        <hr class="divider" />
        <div class="agent-log" id="agent-log"></div>
      </div>

      <!-- Generate Button -->
      <button id="generate-btn" class="btn btn-primary btn-lg" style="width: 100%;" ${state.isGenerating ? 'disabled' : ''}>
        ${state.isGenerating ? '<span class="spinner" style="display:inline-block;width:16px;height:16px;border-width:2px;vertical-align:middle;margin-right:8px;"></span> Generating...' : '⚡ Generate Build'}
      </button>
    </div>
  `;
}

// ─── Results View ───
function renderResultsView() {
  if (!state.buildResult) return '<p style="color: var(--text-muted);">No build generated yet.</p>';

  const build = state.buildResult.validatedBuild;
  const qa = build.qa;

  return `
    <div class="build-output">
      <!-- Build Header -->
      <div class="panel">
        <div class="panel-header">
          <div>
            <h2>${build.name}</h2>
            <p style="color: var(--text-secondary); font-size: 0.85rem;">${build.class} • ${build.ascendancy} • ${build.archetype}</p>
          </div>
          <span class="qa-badge ${qa.verdict === 'APPROVED' ? 'approved' : qa.verdict === 'NEEDS REVIEW' ? 'review' : 'failed'}">
            ${qa.verdict === 'APPROVED' ? '✅' : qa.verdict === 'NEEDS REVIEW' ? '⚠️' : '❌'} ${qa.verdict}
          </span>
        </div>
        <p style="color: var(--text-secondary); font-size: 0.85rem;">${build.reasoning}</p>
      </div>

      <!-- Stats -->
      <div class="panel">
        <h3>📊 Key Stats</h3>
        <div class="stat-grid">
          <div class="stat-card">
            <span class="stat-value">${build.stats.estimatedDPS.toLocaleString()}</span>
            <span class="stat-label">Estimated DPS</span>
          </div>
          <div class="stat-card">
            <span class="stat-value">${build.stats.life.toLocaleString()}</span>
            <span class="stat-label">Life</span>
          </div>
          <div class="stat-card">
            <span class="stat-value" style="color: var(--success);">${build.stats.fireRes}%</span>
            <span class="stat-label">Fire Res</span>
          </div>
          <div class="stat-card">
            <span class="stat-value" style="color: var(--info);">${build.stats.coldRes}%</span>
            <span class="stat-label">Cold Res</span>
          </div>
          <div class="stat-card">
            <span class="stat-value" style="color: var(--warning);">${build.stats.lightningRes}%</span>
            <span class="stat-label">Lightning Res</span>
          </div>
          <div class="stat-card">
            <span class="stat-value">${build.stats.spirit}</span>
            <span class="stat-label">Spirit</span>
          </div>
          <div class="stat-card">
            <span class="stat-value">${build.stats.moveSpeed}%</span>
            <span class="stat-label">Move Speed</span>
          </div>
          ${build.stats.energyShield > 0 ? `
          <div class="stat-card">
            <span class="stat-value">${build.stats.energyShield.toLocaleString()}</span>
            <span class="stat-label">Energy Shield</span>
          </div>` : ''}
        </div>
      </div>

      <!-- Main Skill & Supports -->
      <div class="panel">
        <h3>💎 Skill Setup</h3>
        <div style="margin-bottom: var(--space-md);">
          <label>Main Skill</label>
          <p style="font-size: 1.1rem; font-weight: 600; color: var(--accent-color); margin-top: 4px;">${build.mainSkill.name}</p>
          <p style="font-size: 0.8rem; color: var(--text-secondary); margin-top: 2px;">Tags: ${build.mainSkill.tags.join(', ')}</p>
        </div>
        <div>
          <label>Support Gems</label>
          <div style="display: flex; flex-wrap: wrap; gap: 8px; margin-top: 8px;">
            ${build.supportGems.map(g => `<span class="support-gem">💠 ${g.name}</span>`).join('')}
          </div>
        </div>
      </div>

      <!-- Gear -->
      <div class="panel">
        <h3>🛡️ Gear Recommendations</h3>
        <div style="display: grid; gap: 8px;">
          ${Object.values(build.gear).map(g => `
            <div class="gear-slot">
              <span class="slot-name">${g.slot}</span>
              <div>
                <div class="slot-item">${g.name}</div>
                <div class="slot-mods">${g.mods.join(' • ')}</div>
              </div>
            </div>
          `).join('')}
        </div>
      </div>

      <!-- Passive Tree -->
      <div class="panel">
        <h3>🌳 Interactive Passive Skill Tree Simulation</h3>
        <p style="font-size: 0.8rem; color: var(--text-secondary); margin-bottom: var(--space-md);">Interactive map representing the Path of Building 2 tree layout. Drag to pan. Double click nodes to allocate.</p>
        <div class="grid-3" style="margin-bottom: var(--space-md);">
          <div class="stat-card">
            <span class="stat-value" id="tree-offense-nodes">${build.passives.offense}</span>
            <span class="stat-label">Offense Nodes</span>
          </div>
          <div class="stat-card">
            <span class="stat-value" id="tree-defense-nodes">${build.passives.defense}</span>
            <span class="stat-label">Defense Nodes</span>
          </div>
          <div class="stat-card">
            <span class="stat-value" id="tree-utility-nodes">${build.passives.utility}</span>
            <span class="stat-label">Utility Nodes</span>
          </div>
        </div>
        
        <div class="skill-tree-preview" style="position: relative; margin-bottom: var(--space-md); border-radius: var(--border-radius); overflow: hidden; border: 1px solid var(--border-color); box-shadow: var(--shadow-sm); background: #07080b; height: 350px;">
          <canvas id="pob-tree-canvas" style="width: 100%; height: 100%; display: block; cursor: grab;"></canvas>
          <div style="position: absolute; bottom: 10px; right: 10px; background: rgba(0,0,0,0.7); padding: 4px 8px; border-radius: 4px; font-size: 0.7rem; color: var(--accent-color);">
            [Scroll to Zoom • Drag to Pan]
          </div>
        </div>

        ${build.passives.keystones.length > 0 ? `
        <div style="margin-top: var(--space-md);">
          <label>Keystones</label>
          <div style="display: flex; flex-wrap: wrap; gap: 8px; margin-top: 8px;">
            ${build.passives.keystones.map(k => `<span class="support-gem" style="border-color: var(--accent-color); color: var(--accent-color); background: var(--accent-dim);">🔑 ${k}</span>`).join('')}
          </div>
        </div>` : ''}
      </div>

      <!-- PoB Import/Export -->
      <div class="panel">
        <h3>🔌 Path of Building Import Code</h3>
        <p style="font-size: 0.8rem; color: var(--text-secondary); margin-bottom: var(--space-sm);">Copy this base64 XML string and paste it into Path of Building 2's "Import" tab to view this build.</p>
        <div style="display: flex; gap: var(--space-sm);">
          <textarea id="pob-code-area" readonly style="flex: 1; font-family: monospace; font-size: 0.75rem; height: 80px; resize: none; background: rgba(0,0,0,0.4);">${window.currentPoBCode || ''}</textarea>
          <button class="btn btn-primary" id="copy-pob-btn" style="align-self: flex-end;">📋 Copy</button>
        </div>
      </div>

      <!-- QA Checks -->
      <div class="panel">
        <div class="panel-header">
          <h3>✅ QA Validation</h3>
          <span style="color: var(--text-secondary); font-size: 0.85rem;">${qa.passed}/${qa.total} checks passed (${qa.score}%)</span>
        </div>
        ${qa.checks.map(c => `
          <div class="qa-check">
            <span class="check-icon">${c.passed ? '✅' : '❌'}</span>
            <span class="check-name">${c.name}</span>
            <span class="check-detail">${c.details}</span>
          </div>
        `).join('')}
      </div>

      <button class="btn" style="width: 100%;" onclick="document.querySelector('[data-view=forge]').click()">⚒️ Create Another Build</button>
    </div>
  `;
}

// ─── Event Binding ───
function bindEvents() {
  // Navigation
  document.querySelectorAll('.nav-btn').forEach(btn => {
    btn.addEventListener('click', () => {
      const view = btn.dataset.view;
      if (view && !btn.disabled) {
        state.currentView = view;
        renderApp();
      }
    });
  });

  // Class -> Ascendancy
  const classSelect = document.getElementById('class-select');
  if (classSelect) {
    classSelect.addEventListener('change', () => updateAscendancies(classSelect.value));
  }

  // Sliders
  const tankSlider = document.getElementById('tank-slider');
  const damageSlider = document.getElementById('damage-slider');
  if (tankSlider) {
    tankSlider.addEventListener('input', () => {
      document.getElementById('tank-value').textContent = tankSlider.value;
    });
  }
  if (damageSlider) {
    damageSlider.addEventListener('input', () => {
      document.getElementById('damage-value').textContent = damageSlider.value;
    });
  }

  // Generate
  const genBtn = document.getElementById('generate-btn');
  if (genBtn) {
    genBtn.addEventListener('click', handleGenerate);
  }

  // Copy PoB Code
  const copyBtn = document.getElementById('copy-pob-btn');
  if (copyBtn) {
    copyBtn.addEventListener('click', () => {
      const codeArea = document.getElementById('pob-code-area');
      if (codeArea) {
        codeArea.select();
        document.execCommand('copy');
        copyBtn.textContent = '✅ Copied!';
        setTimeout(() => {
          copyBtn.textContent = '📋 Copy';
        }, 2000);
      }
    });
  }
}

function updateAscendancies(classId) {
  const ascSelect = document.getElementById('ascendancy-select');
  if (!ascSelect) return;

  ascSelect.innerHTML = '<option value="any">Any Ascendancy</option>';
  
  const ascendancies = ASCENDANCY_MAP[classId] || [];
  ascendancies.forEach(asc => {
    const opt = document.createElement('option');
    opt.value = asc.id;
    opt.textContent = asc.name;
    ascSelect.appendChild(opt);
  });
}

// ─── Pipeline Visualization ───
function showPipeline() {
  const section = document.getElementById('pipeline-section');
  if (section) section.classList.remove('section-hidden');

  const container = document.getElementById('pipeline-nodes');
  if (!container) return;

  const agents = state.coordinator.getAgents();
  const steps = ['research', 'meta', 'generator', 'optimizer', 'qa'];
  const labels = ['Research', 'Meta', 'Generator', 'Optimizer', 'QA'];
  const icons = ['🔍', '📈', '⚙️', '🔧', '✅'];

  container.innerHTML = steps.map((key, i) => {
    const agent = agents[key];
    const statusClass = agent.status === 'running' ? 'active' :
                        agent.status === 'complete' ? 'complete' :
                        agent.status === 'error' ? 'error' : '';
    return `
      <div class="pipeline-step">
        <div class="pipeline-node ${statusClass}" id="pipeline-node-${key}">
          <span class="node-icon">${icons[i]}</span>
          <span class="node-name">${labels[i]}</span>
          <span class="node-progress" id="progress-${key}">${agent.progress}%</span>
        </div>
        ${i < steps.length - 1 ? `<span class="pipeline-arrow ${statusClass === 'complete' ? 'active' : ''}">→</span>` : ''}
      </div>
    `;
  }).join('');
}

function addLogEntry(entry) {
  const logContainer = document.getElementById('agent-log');
  if (!logContainer) return;

  const time = new Date(entry.timestamp).toLocaleTimeString('en-US', { hour12: false });
  const div = document.createElement('div');
  div.className = 'log-entry';
  div.innerHTML = `
    <span class="log-time">${time}</span>
    <span class="log-agent">[${entry.agent}]</span>
    <span class="log-message">${entry.message}</span>
  `;
  logContainer.appendChild(div);
  logContainer.scrollTop = logContainer.scrollHeight;
}

// ─── Handle Generate ───
async function handleGenerate() {
  if (state.isGenerating) return;

  state.isGenerating = true;
  state.buildResult = null;
  state.coordinator = new CoordinatorAgent();

  // Update button
  const genBtn = document.getElementById('generate-btn');
  if (genBtn) {
    genBtn.disabled = true;
    genBtn.innerHTML = '<span class="spinner" style="display:inline-block;width:16px;height:16px;border-width:2px;vertical-align:middle;margin-right:8px;"></span> Generating...';
  }

  // Show pipeline
  showPipeline();

  // Listen for events
  const logHandler = (e) => addLogEntry(e.detail);
  const progressHandler = (e) => {
    const el = document.getElementById(`progress-${e.detail.agent.toLowerCase().replace(/ /g, '-')}`);
    if (el) el.textContent = `${e.detail.progress}%`;
  };
  const stepHandler = () => showPipeline();

  window.addEventListener('agent-log', logHandler);
  window.addEventListener('agent-progress', progressHandler);
  window.addEventListener('pipeline-step', stepHandler);

  // Gather user input
  const userInput = {
    class: document.getElementById('class-select')?.value || 'any',
    ascendancy: document.getElementById('ascendancy-select')?.value || 'any',
    league: document.getElementById('league-select')?.value || 'trade',
    budget: document.getElementById('budget-select')?.value || 'starter',
    tankiness: parseInt(document.getElementById('tank-slider')?.value || '5'),
    damage: parseInt(document.getElementById('damage-slider')?.value || '5'),
    features: Array.from(document.querySelectorAll('input[name="feature"]:checked')).map(cb => cb.value)
  };

  try {
    const result = await state.coordinator.execute({ userInput });
    state.buildResult = result;
    // Set global code for results view textarea
    const { exportToPoBCode } = await import('./data/poe2Data.js');
    window.currentPoBCode = exportToPoBCode(result.validatedBuild);
    state.currentView = 'results';
  } catch (err) {
    console.error('Build generation failed:', err);
    addLogEntry({ timestamp: Date.now(), agent: 'System', message: `ERROR: ${err.message}` });
  } finally {
    state.isGenerating = false;
    window.removeEventListener('agent-log', logHandler);
    window.removeEventListener('agent-progress', progressHandler);
    window.removeEventListener('pipeline-step', stepHandler);
    renderApp();
    if (state.currentView === 'results') {
      initInteractiveTree(state.buildResult.validatedBuild);
    }
  }
}

// ─── Interactive Tree Simulation Engine ───
function initInteractiveTree(build) {
  const canvas = document.getElementById('pob-tree-canvas');
  if (!canvas) return;
  const ctx = canvas.getContext('2d');
  
  // Set dimensions
  const dpr = window.devicePixelRatio || 1;
  const rect = canvas.getBoundingClientRect();
  canvas.width = rect.width * dpr;
  canvas.height = rect.height * dpr;
  ctx.scale(dpr, dpr);

  let zoom = 0.5;
  let offsetX = rect.width / 2;
  let offsetY = rect.height / 2;
  let isDragging = false;
  let startX, startY;

  // Generate simulated tree nodes
  const nodes = [];
  const total = 100;
  
  // Base starting node in center
  nodes.push({ id: 0, x: 0, y: 0, type: 'start', label: build.class + ' Start', allocated: true });

  // Generate branches in radial patterns representing actual PoE2 passive tree orbits
  const branches = 6;
  const nodesPerBranch = 8;
  let nodeId = 1;

  for (let b = 0; b < branches; b++) {
    const angle = (b * Math.PI * 2) / branches;
    let prevNodeId = 0;
    
    for (let n = 1; n <= nodesPerBranch; n++) {
      const dist = n * 70;
      const x = Math.cos(angle) * dist + (Math.sin(n * 2) * 15);
      const y = Math.sin(angle) * dist + (Math.cos(n * 2) * 15);
      
      let nodeType = 'normal';
      let label = `Passive Node`;
      if (n === nodesPerBranch) {
        nodeType = 'keystone';
        label = build.passives.keystones[b % build.passives.keystones.length] || 'Ancient Keystone';
      } else if (n % 3 === 0) {
        nodeType = 'notable';
        label = 'Notable Passive';
      }

      // Check allocation based on build ratio
      let allocated = false;
      if (nodeType === 'start') {
        allocated = true;
      } else {
        const threshold = build.passives.offense / 100;
        allocated = (n / nodesPerBranch) <= threshold || (b === 0 && n < 5) || (b === 2 && n < 4);
      }

      nodes.push({
        id: nodeId,
        x,
        y,
        type: nodeType,
        label,
        allocated,
        connections: [prevNodeId]
      });
      prevNodeId = nodeId;
      nodeId++;
    }
  }

  // Draw Function
  function draw() {
    ctx.clearRect(0, 0, rect.width, rect.height);
    ctx.save();
    ctx.translate(offsetX, offsetY);
    ctx.scale(zoom, zoom);

    // Draw grid background representing cosmic passive screen grid
    ctx.strokeStyle = 'rgba(207, 166, 91, 0.03)';
    ctx.lineWidth = 1;
    for (let i = -1500; i < 1500; i += 50) {
      ctx.beginPath();
      ctx.moveTo(i, -1500);
      ctx.lineTo(i, 1500);
      ctx.stroke();
      ctx.beginPath();
      ctx.moveTo(-1500, i);
      ctx.lineTo(1500, i);
      ctx.stroke();
    }

    // Draw Orbits
    ctx.strokeStyle = 'rgba(255, 255, 255, 0.05)';
    ctx.lineWidth = 2;
    [100, 220, 380, 520].forEach(radius => {
      ctx.beginPath();
      ctx.arc(0, 0, radius, 0, Math.PI * 2);
      ctx.stroke();
    });

    // Draw Connections/Pathways
    nodes.forEach(node => {
      if (node.connections) {
        node.connections.forEach(connId => {
          const parent = nodes.find(n => n.id === connId);
          if (parent) {
            ctx.beginPath();
            ctx.moveTo(node.x, node.y);
            ctx.lineTo(parent.x, parent.y);
            
            if (node.allocated && parent.allocated) {
              ctx.strokeStyle = '#CFA65B'; // glowing golden pathway
              ctx.lineWidth = 4;
              ctx.shadowColor = '#CFA65B';
              ctx.shadowBlur = 10;
            } else {
              ctx.strokeStyle = '#3a4d61'; // dark metal pathways
              ctx.lineWidth = 1.5;
              ctx.shadowBlur = 0;
            }
            ctx.stroke();
          }
        });
      }
    });

    // Reset shadow for nodes
    ctx.shadowBlur = 0;

    // Draw Nodes
    nodes.forEach(node => {
      ctx.beginPath();
      let size = 6;
      let strokeColor = '#3a4d61';
      let fillColor = '#0f141d';

      if (node.type === 'start') {
        size = 14;
        strokeColor = node.allocated ? '#CFA65B' : '#8c92ac';
        fillColor = '#1f2833';
      } else if (node.type === 'keystone') {
        size = 10;
        strokeColor = node.allocated ? '#CFA65B' : '#ff4c4c';
        fillColor = node.allocated ? 'rgba(207, 166, 91, 0.2)' : '#1f2833';
      } else if (node.type === 'notable') {
        size = 8;
        strokeColor = node.allocated ? '#CFA65B' : '#45b7d1';
        fillColor = '#0f141d';
      } else {
        if (node.allocated) {
          strokeColor = '#CFA65B';
          fillColor = '#CFA65B';
        }
      }

      ctx.arc(node.x, node.y, size, 0, Math.PI * 2);
      ctx.fillStyle = fillColor;
      ctx.fill();
      ctx.strokeStyle = strokeColor;
      ctx.lineWidth = node.allocated ? 2.5 : 1.5;
      ctx.stroke();

      // Text labels for Keystone & Start nodes
      if (node.type === 'keystone' || node.type === 'start') {
        ctx.fillStyle = node.allocated ? '#CFA65B' : '#8c92ac';
        ctx.font = `bold ${node.type === 'start' ? 12 : 9}px Arial`;
        ctx.textAlign = 'center';
        ctx.fillText(node.label, node.x, node.y - size - 5);
      }
    });

    ctx.restore();
  }

  // Event Handlers for Drag & Pan & Zoom
  canvas.addEventListener('mousedown', e => {
    isDragging = true;
    canvas.style.cursor = 'grabbing';
    startX = e.clientX - offsetX;
    startY = e.clientY - offsetY;
  });

  window.addEventListener('mouseup', () => {
    isDragging = false;
    canvas.style.cursor = 'grab';
  });

  canvas.addEventListener('mousemove', e => {
    if (!isDragging) return;
    offsetX = e.clientX - startX;
    offsetY = e.clientY - startY;
    draw();
  });

  canvas.addEventListener('wheel', e => {
    e.preventDefault();
    const zoomFactor = 1.1;
    if (e.deltaY < 0) {
      zoom *= zoomFactor;
    } else {
      zoom /= zoomFactor;
    }
    zoom = Math.min(Math.max(zoom, 0.15), 3);
    draw();
  });

  // Handle Double Click to Allocate Node (Manual Customization Simulation)
  canvas.addEventListener('dblclick', e => {
    const mouseX = (e.clientX - rect.left - offsetX) / zoom;
    const mouseY = (e.clientY - rect.top - offsetY) / zoom;

    // Find nearest node
    let closestNode = null;
    let minDist = 20;

    nodes.forEach(node => {
      const dist = Math.sqrt((node.x - mouseX) ** 2 + (node.y - mouseY) ** 2);
      if (dist < minDist) {
        minDist = dist;
        closestNode = node;
      }
    });

    if (closestNode && closestNode.type !== 'start') {
      closestNode.allocated = !closestNode.allocated;
      
      // Recalculate stats simulation
      let off = 0, def = 0, uti = 0;
      nodes.forEach(n => {
        if (n.allocated && n.id !== 0) {
          if (n.type === 'keystone') off += 2;
          else if (n.type === 'notable') def += 2;
          else off += 1;
        }
      });

      document.getElementById('tree-offense-nodes').textContent = off;
      document.getElementById('tree-defense-nodes').textContent = def;

      draw();
    }
  });

  draw();
}

// ─── Init ───
renderApp();
