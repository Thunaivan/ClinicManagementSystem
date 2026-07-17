# Angular Handbook

## Table of Contents

1. Project Structure
2. Standalone Components
3. Dependency Injection
4. Routing
5. HttpClient
6. Environment Configuration
7. Pagination
8. Angular Material
9. Application Shell

---

# Chapter 9 - Application Shell

## Objective

Create a reusable application layout that hosts all feature pages.

## Why?

Instead of each page having its own menu and header, we build one common layout.

Benefits:

- Consistent UI
- Reusable navigation
- Easy maintenance
- Professional architecture
- Separation of concerns

## Planned Architecture

```text
App
 │
 ▼
MainLayout
 │
 ├── Toolbar
 ├── Side Navigation
 └── Router Outlet
        │
        ├── Dashboard
        ├── Patients
        ├── Doctors
        ├── Departments
        └── Appointments
```

## Components Planned

| Component    | Purpose                |
| ------------ | ---------------------- |
| MatToolbar   | Header                 |
| MatSidenav   | Navigation             |
| MatNavList   | Menu                   |
| RouterOutlet | Displays feature pages |

## Commands Used

```bash
ng add @angular/material

ng generate component core/layout/main-layout
```

## Lesson Learned

Build the application shell before building feature pages. Every business module (Patients, Doctors, Appointments) should plug into the same layout rather than creating its own.
