# RecoVR — VR-Powered Remote Physiotherapy

> Incubated at Carnegie Mellon University | Founded by [Sneha Hassan](https://www.linkedin.com/in/sneha-hassan)

RecoVR is a VR application that enables patients with mobility challenges to perform guided physiotherapy exercises from home, with **real-time body tracking** to ensure correct form and measure progress over time.

**Pilot Results:** 40% improvement in patient fitness outcomes vs. traditional home exercises across a 20-patient study.

---

## The Problem

Patients with mobility challenges often struggle to access regular in-person physiotherapy — long commutes, scheduling constraints, and cost all create barriers. Traditional home exercise programs have poor adherence because patients have no feedback on whether they're performing exercises correctly.

RecoVR solves this by bringing the physiotherapy session into the patient's home via VR, with real-time tracking that acts as a virtual form-checker.

---

## Architecture

```
┌──────────────────────────────────────────────────────────┐
│                     VR Headset (HMD)                     │
│              Head + Hand Position @ ~90Hz                 │
└──────────────────┬───────────────────────────────────────┘
                   │ Raw sensor stream
                   ▼
┌──────────────────────────────────────────────────────────┐
│              Positional Tracking Pipeline                 │
│  ┌─────────────┐  ┌──────────────┐  ┌────────────────┐  │
│  │ Noise Filter │→│ IK Solver    │→│ Skeletal Model  │  │
│  │ (Adaptive)   │  │ (3-pt → body)│  │ (Normalized)   │  │
│  └─────────────┘  └──────────────┘  └────────────────┘  │
└──────────────────┬───────────────────────────────────────┘
                   │ Joint angles + positions
                   ▼
┌──────────────────────────────────────────────────────────┐
│            Exercise Protocol State Machine                │
│                                                          │
│  idle → exercise_active → rep_in_progress → rep_complete │
│                    → set_complete → exercise_complete     │
│                                                          │
│  Transitions triggered by positional thresholds          │
│  (e.g., "elbow angle > 90° for 3s" = hold complete)     │
└──────┬───────────────────────────────┬───────────────────┘
       │                               │
       ▼                               ▼
┌──────────────┐              ┌─────────────────┐
│   VR UI      │              │  Data Logging    │
│  Visual cues │              │  Session metrics │
│  Progress    │              │  Form scores     │
│  Feedback    │              │  ROM tracking    │
└──────────────┘              └─────────────────┘
```

---

## Key Technical Decisions

### Adaptive Noise Filtering
VR controllers produce jittery positional data. I implemented an adaptive filtering approach:
- **Slow movements** (stretches, holds): Aggressive smoothing for stable form detection
- **Fast movements** (reps, transitions): Reduced filtering for responsiveness
- Velocity thresholds trigger mode switching automatically

### 3-Point Inverse Kinematics
Consumer VR headsets provide only 3 tracking points (head + 2 hands). I built a custom IK solver that infers elbow, shoulder, and torso positions from this limited data — less accurate than full-body tracking suits, but **deployable on consumer hardware** (no extra equipment needed).

### Hard Gates vs. Soft Scores
- **Hard gates:** Safety-critical thresholds (e.g., prevent hyperextension) — blocks the exercise
- **Soft scores:** Form quality reported as a percentage — encouraging, not punitive

This separation was designed with physiotherapists to balance clinical rigor with patient motivation.

---

## Tech Stack

| Layer | Technology |
|-------|-----------|
| VR Runtime | Unity |
| Tracking Scripts | C, C# |
| State Machine | Custom event-driven engine |
| Data Logging | Structured session events |

---

## Results

| Metric | Value |
|--------|-------|
| Pilot study size | 20 patients |
| Fitness improvement vs. traditional exercises | **40%** |
| Tracking frequency | ~90Hz |
| Processing latency per frame | <20ms |
| Clinician feedback on session reports | "Clinically actionable" |

---

## What I Learned

- **Designing for ambiguity:** Patients don't move cleanly. Defining "close enough" thresholds for exercise transitions required iterating directly with physiotherapists watching real sessions.
- **Solo founder trade-offs:** Every architectural decision optimized for maintainability by one person. Flat state machines over hierarchical ones, synchronous over async where possible, explicit over abstract.
- **Clinical validation matters:** Building the tech was 50% of the challenge. The other 50% was designing a pilot study, coordinating with clinicians, and proving outcomes with real data.

---

## Status

🟢 Active development — iterating based on pilot study feedback.

---

## Contact

**Sneha Hassan** — [LinkedIn](https://www.linkedin.com/in/sneha-hassan) · [Email](mailto:snehahassan9970@gmail.com)
