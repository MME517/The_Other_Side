# 🏺 XR Escape Room

> An immersive VR escape room experience built with Unity 6 and the XR Interaction Toolkit — set inside an Ancient Egyptian tomb.

---

## 📖 Overview

This project is an **XR-based Escape Room** developed as a course final project. It features two scenes — an Arcade Lobby and a fully themed Ancient Egyptian puzzle room — and demonstrates applied spatial UX design, complex XR interaction, performance optimization, and inclusive design practices. Players use XR controllers (or the built-in emulator) to solve three progressive puzzles and escape the tomb.

---

## 🗺️ Scenes

### Scene 1 — Arcade Lobby
The entry point of the experience. Players arrive in an arcade-themed environment and use an interactive panel to teleport into the escape room.

- Interactive **Start Game** panel with direct manipulation UI
- Arcade-themed decorations and ambient positional audio
- Teleportation locomotion into Scene 2

### Scene 2 — Ancient Egyptian Escape Room
The main gameplay scene — a fully atmospheric Egyptian tomb featuring torches, statues, hieroglyphic decorations, and spatial audio.

- Three interconnected puzzles separated by locked doors
- Progressive wall fragment lighting as visual feedback
- Spatial audio cues tied to puzzle completion events

---

## 🧩 Puzzles & Interaction Design

All puzzles are designed around a clear **action → feedback → reward** loop using XR-native interaction models.

### Puzzle 1 — Hidden Cubes
Find and place **3 hidden cubes** scattered around the room onto their designated shelves. Each correct placement lights a wall fragment. Once all three are placed, **Door 1 unlocks**.

- Interaction model: **Direct Manipulation** (grab + place using XR controllers)
- Feedback: Visual (fragment lights) + Audio (placement sound)

### Puzzle 2 — Balance Scale
Place a **gold sphere** and a **silver sphere** on the correct sides of a balance scale. A successful balance lights the second fragment and plays audio feedback. **Door 2 unlocks**.

- Interaction model: **Direct Manipulation** (grab + release with physics)
- Feedback: Scale animation + Visual fragment + Audio cue

### Puzzle 3 — Dial Code
A code is hidden in the environment and only becomes **visible on hover**. Observe the sequence, interact with the wall dials, and input the correct order. **End-game panel triggers**.

- Interaction model: **Laser Pointer / Controller Ray** for dial interaction
- Feedback: Hover reveal + dial snap response + final fragment lighting

---

## 🎨 Spatial UX & Design

The experience follows spatial UX best practices across all five dimensions:

| UX Dimension | Implementation |
|---|---|
| **Color** | High-contrast Egyptian palette (gold, dark stone, fire orange) for readability in VR |
| **Lighting** | Dynamic torch lighting and baked ambient light for atmosphere and depth cues |
| **Materials** | Physically-based materials on all interactive objects; holograms used for UI panels |
| **Scale** | All objects and UI panels sized to real-world ergonomic standards for VR |
| **Typography** | TextMeshPro with legible font sizes; panels placed at comfortable reading distances |
| **Spatial Sound** | Positional audio sources tied to torches, puzzle events, and environmental ambiance |

Locomotion is handled via **Teleportation** to minimize vection and reduce motion sickness risk. No free-movement or artificial locomotion is used.

---

## 🧠 Human Factors & Comfort

User comfort was a primary design consideration throughout development:

- **Teleportation-only locomotion** eliminates vection-induced discomfort
- UI panels are placed at **1.5–2m distance** and within the comfortable FOV cone
- No fast camera rotations or forced movement sequences
- Puzzle interactions are designed to avoid **awkward arm positions** (no overhead reaching)
- Scene transitions use **fade-to-black** to prevent sudden disorientation
- All interactive objects are placed at **standing reach height** for accessibility

---

## ⚡ Performance & Optimization

The project is optimized for smooth VR performance targeting **72+ FPS**:

- **Static batching** applied to all non-interactive environment meshes
- **Occlusion culling** configured per scene to avoid rendering unseen geometry
- Low-polycount asset pipeline; hero props modeled within VR polygon budgets
- Audio sources set to **3D spatial blend** with max distance limits to reduce overhead
- Scripts use **event-driven architecture** (no per-frame polling in Update where avoidable)
- Profiler sessions conducted to identify and resolve draw call spikes

---

## ♿ Accessibility & Inclusion

The project implements multiple accessibility considerations:

- **Two locomotion options**: Teleportation (primary) and standing-in-place puzzle interaction for users with limited mobility
- **High-contrast visual feedback**: Fragment lighting and color-coded objects ensure puzzle state is always clear
- **Audio + Visual redundancy**: Every puzzle completion triggers both a sound and a visual cue — no feedback relies on a single channel
- **Scalable UI panels**: TextMeshPro panels are sized for legibility across different headset IPD settings
- **No time pressure**: Puzzles have no countdown timer, reducing stress and accommodating all paces of play

---

## 🏁 Game Completion

After solving all three puzzles, a **congratulations panel** appears with two options:

| Option | Action |
|---|---|
| 🔁 Restart | Replay the escape room from the beginning |
| 🏠 Lobby | Return to Scene 1 (Arcade Lobby) |

---

## 🛠️ Tech Stack

| Technology | Purpose |
|---|---|
| Unity 6 | Game engine and scene management |
| XR Interaction Toolkit | VR grab, teleport, ray, and UI interaction |
| TextMeshPro | Spatial UI text rendering |
| Unity Audio Mixer | Spatial audio routing and mixing |
| XR Device Emulator | Testing and iteration without physical hardware |

---

## 🚀 Getting Started

1. Open the project in **Unity 6**
2. Ensure the following packages are installed via **Package Manager**:
   - XR Interaction Toolkit
   - XR Plugin Management
3. Open **Scene 1 (Lobby)** from `Assets/Scenes/`
4. Press **Play**
5. Use the **XR Device Emulator** to grab, teleport, and interact

---

## 📂 Project Structure

```
Assets/
├── Scenes/
│   ├── Lobby               ← Scene 1: Arcade Lobby
│   └── EscapeRoom          ← Scene 2: Egyptian Escape Room
├── Scripts/
│   ├── PuzzleManager       ← Tracks puzzle state and door triggers
│   ├── DialController      ← Handles dial rotation and code validation
│   ├── ScaleController     ← Physics-based scale balance logic
│   └── Interaction/        ← Custom XR grab and hover scripts
├── Prefabs/
├── Materials/
└── Audio/
```

---

## 🔄 Iterative Design Process

The project followed a structured iterative cycle:

1. **Concept** — Defined puzzle mechanics and scene layout on paper
2. **Prototype** — Greybox environment with basic XR grab and teleport interactions
3. **Alpha** — Full scene art pass, audio integration, and puzzle logic implemented
4. **Testing** — Playtested using XR Emulator; adjusted object placement, feedback timing, and comfort settings based on observations
5. **Final Build** — Optimization pass, UI polish, and end-game flow completed

Version history is tracked via **GitHub commits** throughout all phases.

---

## 👥 Team

Developed as a final project for an XR Development course — demonstrating spatial UX design, technical XR implementation, human factors, and optimization across a complete interactive experience.
