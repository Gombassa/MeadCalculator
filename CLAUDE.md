# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Repository Overview

**MeadCalculator** is a web-based application for mead makers, featuring:
- General calculator for mead recipes
- Nutrient calculator
- Specialist sub-category calculators

This is a newly initialized repository (as of Oct 26, 2025) with the basic structure being set up.

## Technology Stack

Based on the .gitignore configuration, the project will use:
- **Backend**: .NET (C#, ASP.NET Core)
- **Frontend**: Node.js/JavaScript (web application)
- **Development Tools**: Visual Studio or Visual Studio Code

## Project Structure (Expected)

As the project develops, the following structure is anticipated:

```
MeadCalculator/
├── Backend/              # .NET backend (ASP.NET Core API)
│   ├── [ProjectName].csproj
│   ├── Controllers/
│   ├── Models/
│   ├── Services/
│   └── appsettings.json
├── Frontend/             # Node.js/JavaScript frontend
│   ├── package.json
│   ├── src/
│   ├── public/
│   └── webpack.config.js
├── README.md
├── CLAUDE.md
└── .gitignore
```

## Common Development Commands

### .NET Backend (MeadCalculator.API/)
```bash
# Restore NuGet packages
dotnet restore

# Build the project
dotnet build

# Run the application locally
dotnet run
# API runs on https://localhost:5001

# Run tests (when test project is added)
dotnet test

# Publish for deployment
dotnet publish -c Release
```

### Frontend (frontend/)
```bash
# Install dependencies
npm install

# Development server with hot reload
npm run dev
# Frontend runs on http://localhost:5173

# Build for production
npm run build

# Preview production build locally
npm run preview

# Run linter (when configured)
npm run lint
```

## Key Files to Check

- **README.md** - Project overview and setup instructions
- **.gitignore** - Configured for Visual Studio/.NET and Node.js workflows
- **appsettings.json** - Configuration for ASP.NET Core (when added)
- **package.json** - Frontend dependencies and scripts (when added)
- **.csproj** files - .NET project files (when added)

## Architecture Notes

### Current Implementation

**Frontend** (React + Tailwind CSS):
- `LandingPage.jsx`: Beautiful hero section with feature highlights and call-to-action
- `Calculator.jsx`: Main calculator interface with dynamic ingredient management
- `IngredientForm.jsx`: Reusable component for adding/editing ingredients
- `CalculationResults.jsx`: Displays ABV, gravity, and ingredient breakdown
- `Navigation.jsx`: Responsive header with mobile menu
- `api.js`: Centralized API service layer using Axios

**Backend** (ASP.NET Core 9.0):
- **Models**: Clean data structures for ingredients, recipes, and calculations
  - `Ingredient.cs`: Defines honey, fruits, juices, water with sugar content percentages
  - `CalculationRequest/Result.cs`: Request/response DTOs
  - `Recipe.cs`: Future recipe storage model (not yet persisted)

- **Services**:
  - `CalculationService.cs`: Core business logic
    - ABV calculation using fermentation efficiency (51%)
    - Gravity calculations (OG, FG)
    - Three calculation modes: HoneyWeight, TargetABV, TargetVolume
  - `IngredientService.cs`: In-memory ingredient database
    - Pre-loaded with common honeys, fruits, and juices
    - Sugar content data by weight (fruits) or volume (juices)

- **Controllers**:
  - `CalculatorController.cs`: POST endpoint for calculations
  - `IngredientsController.cs`: GET endpoints for ingredient data

### Data Flow
1. Frontend loads ingredients from `/api/ingredients`
2. User adds ingredients and selects calculation mode
3. Frontend sends `CalculationRequest` to `/api/calculator/calculate`
4. Backend service calculates ABV, gravity, and returns detailed breakdown
5. Results displayed in sticky panel on calculator page

### Future Enhancements
- Database integration (Entity Framework Core)
- User authentication and recipe persistence
- Advanced nutrient calculations
- Fermentation timeline tracking
- PDF export functionality

## Getting Started

When project files are added:
1. Clone the repository
2. Install dependencies (npm install, dotnet restore)
3. Check specific setup instructions in README.md
4. Run local development servers/API
5. Verify tests pass

## Important Notes

- **VS Code settings are tracked**: `.vscode/settings.json`, `.vscode/tasks.json`, `.vscode/launch.json`, and `.vscode/extensions.json` are intentionally included in the repository (see negated patterns in .gitignore)
- **Environment files are ignored**: `.env` files are in .gitignore for security
- **Build outputs are ignored**: `bin/`, `obj/`, `Debug/`, `Release/`, and other build artifacts
- **Test results are ignored**: TestResult files and coverage reports are not tracked

## When Adding New Features

1. Create appropriate directory structure if needed
2. Add unit tests alongside feature code
3. Update documentation in README.md
4. Run `dotnet build` and `npm run build` before committing
5. Ensure all tests pass with `dotnet test` and `npm test`
6. Follow the coding patterns established in the codebase
