# MeadCalculator

A modern web-based mead calculator for brewers. Design perfect mead recipes with accurate ABV calculations, nutrient analysis, and ingredient databases.

## Features

- **Landing Page**: Modern, attractive design with hero section and feature highlights
- **Mead Calculator**: Input honey, fruits, juices, and water with precise measurements
- **ABV Calculations**: Calculate estimated alcohol by volume based on ingredients
- **Three Calculation Modes**:
  - Calculate ABV from ingredients
  - Target ABV (determine honey needed for desired alcohol content)
  - Target Volume (scale ingredients proportionally)
- **Ingredient Database**: Pre-loaded sugar content data for:
  - Various honey types (Clover, Wildflower, Orange Blossom, Buckwheat)
  - Common fruits (Apples, Berries, Stone Fruits, etc.)
  - Fruit juices (Apple, Orange, Cranberry, Grape, Pomegranate, Blueberry)
  - Water
- **Detailed Results**: View gravity readings, fermentable sugars, and ingredient breakdown
- **Responsive Design**: Works on desktop, tablet, and mobile devices

## Tech Stack

### Backend
- **Framework**: ASP.NET Core 9.0
- **Language**: C#
- **API**: RESTful endpoints for calculations and ingredient data

### Frontend
- **Framework**: React 18 with Vite
- **Styling**: Tailwind CSS
- **State Management**: React Hooks
- **HTTP Client**: Axios

## Project Structure

```
MeadCalculator/
├── MeadCalculator.API/          # ASP.NET Core backend
│   ├── Controllers/             # API endpoints
│   │   ├── CalculatorController.cs
│   │   └── IngredientsController.cs
│   ├── Models/                  # Data models
│   │   ├── Ingredient.cs
│   │   ├── Recipe.cs
│   │   ├── CalculationRequest.cs
│   │   └── CalculationResult.cs
│   ├── Services/                # Business logic
│   │   ├── CalculationService.cs
│   │   └── IngredientService.cs
│   ├── Program.cs               # Startup configuration
│   └── MeadCalculator.API.csproj
├── frontend/                    # React frontend
│   ├── src/
│   │   ├── pages/
│   │   │   ├── LandingPage.jsx
│   │   │   └── Calculator.jsx
│   │   ├── components/
│   │   │   ├── Navigation.jsx
│   │   │   ├── IngredientForm.jsx
│   │   │   └── CalculationResults.jsx
│   │   ├── services/
│   │   │   └── api.js
│   │   ├── App.jsx
│   │   ├── App.css
│   │   └── main.jsx
│   ├── public/
│   ├── package.json
│   ├── vite.config.js
│   ├── tailwind.config.js
│   └── postcss.config.js
├── MeadCalculator.sln
├── global.json
├── README.md
└── CLAUDE.md
```

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- Node.js 16+ and npm

### Setup & Installation

#### 1. Clone the repository
```bash
git clone https://github.com/Gombassa/MeadCalculator.git
cd MeadCalculator
```

#### 2. Setup Backend
```bash
cd MeadCalculator.API
dotnet restore
dotnet build
```

#### 3. Setup Frontend
```bash
cd ../frontend
npm install
npm run build
```

### Running Locally

#### Terminal 1 - Start the Backend API
```bash
cd MeadCalculator.API
dotnet run
# API will be available at https://localhost:5001
```

#### Terminal 2 - Start the Frontend Dev Server
```bash
cd frontend
npm run dev
# Frontend will be available at http://localhost:5173
```

Open your browser and navigate to `http://localhost:5173`

## Usage

1. **Visit the Landing Page**: See an overview of features and get started
2. **Navigate to Calculator**: Click "Open Calculator" or use the navigation menu
3. **Add Ingredients**: Select ingredients from the dropdown and specify amounts
4. **Choose Calculation Mode**:
   - Default: Calculate ABV from your ingredients
   - Target ABV: Specify desired alcohol percentage, get honey requirement
   - Target Volume: Set desired batch volume, scale ingredients proportionally
5. **View Results**: See detailed calculations including ABV, gravity, and ingredient breakdown

### Calculation Details

**ABV Calculation Formula:**
- Uses fermentation efficiency of 51% (typical yeast performance)
- Accounts for sugar content by weight (fruits, honey) and volume (juices)
- Produces ethanol gram estimates based on fermentable sugars

**Gravity Calculations:**
- Original Gravity (OG): Based on sugar concentration
- Final Gravity (FG): Estimated from fermentation efficiency
- Used for flavor prediction and fermentation tracking

## API Endpoints

### GET /api/ingredients
Returns all available ingredients with sugar content data

### GET /api/ingredients/{id}
Returns a specific ingredient by ID

### GET /api/ingredients/by-type/{type}
Returns ingredients filtered by type (Honey, Fruit, FruitJuice, Water)

### POST /api/calculator/calculate
Performs mead calculation

**Request Body:**
```json
{
  "ingredients": [
    {
      "ingredientId": 1,
      "ingredientName": "Clover Honey",
      "type": "Honey",
      "amount": 1000,
      "sugarContentPercentage": 80,
      "unit": "g"
    }
  ],
  "mode": "HoneyWeight",
  "targetValue": null
}
```

**Response:**
```json
{
  "totalVolumeML": 1000,
  "totalFermentableSugarsGrams": 408,
  "estimatedABV": 11.5,
  "estimatedOriginalGravity": 1.041,
  "estimatedFinalGravity": 0.997,
  "calculatedHoneyWeightGrams": null,
  "calculatedVolumeML": null,
  "calculatedABV": null,
  "ingredients": [...]
}
```

## Development

### Build Backend
```bash
dotnet build
```

### Test Backend
```bash
dotnet test
```

### Development Frontend Server
```bash
cd frontend
npm run dev
```

### Build Frontend
```bash
cd frontend
npm run build
```

## Future Enhancements

- User authentication and recipe saving
- Database integration for persistent storage
- Advanced nutrient calculator (yeast nutrients, acid balance)
- Fermentation timeline and temperature recommendations
- Export recipes as PDF
- Batch comparison tools
- Community recipe sharing

## License

This project is open source and available under the MIT License.

## Contributing

Contributions are welcome! Please feel free to submit pull requests or open issues for bugs and feature requests.

## Contact

For questions or support, please reach out through the GitHub repository.
