# Contract Monthly Claim System (CMCS) - Prototype

A .NET WPF-based prototype for the Contract Monthly Claim System, designed to streamline the process of submitting and approving monthly claims for Independent Contractor (IC) lecturers.

## 📖 Overview

This project is Part 1 of the Portfolio of Evidence (PoE) for PROG6121. It involves the planning and design of a full-stack application, including a database schema, project plan, and a non-functional visual prototype of the user interface built with Windows Presentation Foundation (WPF).

The system aims to replace manual claim processes with a digital solution that enhances efficiency, accuracy, and transparency for lecturers, programme coordinators, and academic managers.

## ✨ Features (Planned)

*   **Role-Based Authentication:**
    *   Lecturers: Submit and track claims.
    *   Programme Coordinators: Verify and approve claims.
    *   Academic Managers: Final verification and approval.
*   **Claim Management:** Submit new claims with automatic calculation of totals.
*   **Document Upload:** Attach supporting documents for each claim.
*   **Transparent Tracking:** Real-time status updates on every claim.
*   **Approval Workflow:** Multi-stage approval process with a full audit trail.

## 🛠️ Technology Stack

*   **Frontend:** WPF (Windows Presentation Foundation) / XAML / C#
*   **Backend (Future):** .NET Core
*   **Database (Designed):** Microsoft SQL Server
*   **Architecture:** MVC / MVVM (To be implemented)
*   **Version Control:** Git / GitHub

## 📁 Project Structure
CMCS_Prototype/
├── Pages/ # WPF XAML Pages for the UI
│ ├── LoginPage.xaml
│ ├── DashboardPage.xaml
│ ├── NewClaimPage.xaml
│ ├── TrackClaimsPage.xaml
│ └── ApproveClaimsPage.xaml
├── Models/ # C# Classes representing data models
│ ├── User.cs
│ ├── Claim.cs
│ └── Approval.cs
├── Windows/ # Main application window
│ └── MainWindow.xaml
├── Documentation/ # Supporting documents (Report, UML, Plan)
└── README.md
