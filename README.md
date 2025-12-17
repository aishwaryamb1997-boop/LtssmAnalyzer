# LTSSM Analyzer – PCIe Protocol Visualization Tool

LTSSM Analyzer is a **C#/.NET-based protocol analysis and visualization tool** designed to decode, analyze, and visually represent **PCIe LTSSM (Link Training and Status State Machine) transitions**.

The project is structured as a **Core analysis engine** with a **WPF-based visualization layer**, following clean separation of concerns and industry-standard architecture.

---

## 📌 Key Features

- Decode PCIe LTSSM state transitions
- Visual timeline rendering of state changes
- Duration-based state blocks
- Upstream / Downstream lane separation
- Speed renegotiation markers
- Recovery state highlighting
- Interactive tooltips with detailed transition metadata
- Scalable architecture for future protocol extensions

---

## 🏗️ Project Architecture

<img width="204" height="252" alt="image" src="https://github.com/user-attachments/assets/737023b8-ccdd-4099-816f-eef8e1e351ba" />



---

## 🧠 Technology Stack

- **Language:** C#
- **Framework:** .NET (WPF)
- **UI:** Windows Presentation Foundation (WPF)
- **Architecture:** MVVM
- **Rendering:** Canvas-based timeline visualization
- **Version Control:** Git & GitHub

---

## 🖥️ Application Overview

The WPF UI renders LTSSM transitions as a **time-aligned graphical timeline**, where:

- Each rectangle represents a PCIe LTSSM state
- Width corresponds to state duration
- Color represents state type
- Vertical lanes separate Upstream and Downstream ports
- Vertical markers indicate link speed changes
- Tooltips expose detailed transition metadata

This enables engineers to quickly identify:
- Link training issues
- Recovery events
- Speed renegotiations
- Timing anomalies

---

## 🚀 Getting Started

### Prerequisites
- Visual Studio 2022 or later
- .NET Desktop Development workload enabled
- Windows OS

### Steps to Run
1. Clone the repository:
   ```bash
   git clone https://github.com/aishwaryamb1997-boop/LtssmAnalyzer.git
2. Open the solution in Visual Studio

3. Set LtssmAnalyzer.Wpf as the startup project

4. Build and run (F5)
📌 Target Audience

PCIe design and validation engineers

Protocol analysis tool developers

Embedded and system software engineers

Developers building visualization tools for hardware protocols

📜 License

This project is currently shared for learning, demonstration, and portfolio purposes.
Licensing terms can be added as the project matures.

👤 Author

Aishwarya M B
Software Engineer | C# | WPF | Protocol Analysis
GitHub: https://github.com/aishwaryamb1997-boop

⭐ Acknowledgements

Inspired by real-world PCIe protocol analyzers and validation workflows used in hardware bring-up and verification environments.
