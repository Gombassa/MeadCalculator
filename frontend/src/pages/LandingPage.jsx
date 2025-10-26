import { Link } from 'react-router-dom'

export default function LandingPage() {
  return (
    <div className="w-full">
      {/* Hero Section */}
      <section className="bg-gradient-to-br from-amber-50 via-yellow-50 to-orange-50 min-h-screen flex items-center">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 w-full">
          <div className="grid md:grid-cols-2 gap-12 items-center">
            <div>
              <h1 className="text-5xl md:text-6xl font-bold text-amber-900 mb-6 leading-tight">
                Master the Art of Mead Making
              </h1>
              <p className="text-xl text-amber-700 mb-8 leading-relaxed">
                Precise calculations for perfect mead every time. From honey selection to fermentation monitoring, our calculator guides you through every step.
              </p>
              <div className="flex flex-col sm:flex-row gap-4">
                <Link
                  to="/calculator"
                  className="bg-gradient-to-r from-amber-600 to-yellow-600 text-white px-8 py-3 rounded-lg font-semibold hover:from-amber-700 hover:to-yellow-700 transition shadow-lg text-center"
                >
                  Start Calculating
                </Link>
                <a
                  href="#features"
                  className="border-2 border-amber-600 text-amber-900 px-8 py-3 rounded-lg font-semibold hover:bg-amber-50 transition text-center"
                >
                  Learn More
                </a>
              </div>
            </div>
            <div className="flex justify-center">
              <div className="text-9xl animate-bounce">🍯</div>
            </div>
          </div>
        </div>
      </section>

      {/* Features Section */}
      <section id="features" className="py-20 bg-white">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="text-center mb-16">
            <h2 className="text-4xl md:text-5xl font-bold text-amber-900 mb-4">
              Powerful Features
            </h2>
            <p className="text-xl text-amber-700">
              Everything you need to create exceptional mead
            </p>
          </div>

          <div className="grid md:grid-cols-3 gap-8">
            {/* Feature 1 */}
            <div className="bg-gradient-to-br from-amber-50 to-yellow-50 p-8 rounded-lg shadow-md hover:shadow-xl transition">
              <div className="text-5xl mb-4">🧬</div>
              <h3 className="text-2xl font-bold text-amber-900 mb-3">
                Nutrient Calculator
              </h3>
              <p className="text-amber-700">
                Calculate exact nutrient levels for optimal fermentation. Ensure your yeast has everything it needs to thrive.
              </p>
            </div>

            {/* Feature 2 */}
            <div className="bg-gradient-to-br from-amber-50 to-yellow-50 p-8 rounded-lg shadow-md hover:shadow-xl transition">
              <div className="text-5xl mb-4">📊</div>
              <h3 className="text-2xl font-bold text-amber-900 mb-3">
                ABV Calculator
              </h3>
              <p className="text-amber-700">
                Accurately predict alcohol by volume based on honey weight, target volume, or desired ABV. Plan your batches with precision.
              </p>
            </div>

            {/* Feature 3 */}
            <div className="bg-gradient-to-br from-amber-50 to-yellow-50 p-8 rounded-lg shadow-md hover:shadow-xl transition">
              <div className="text-5xl mb-4">🍎</div>
              <h3 className="text-2xl font-bold text-amber-900 mb-3">
                Ingredient Database
              </h3>
              <p className="text-amber-700">
                Access pre-loaded sugar content data for fruits and juices. Build complex recipes with confidence.
              </p>
            </div>
          </div>
        </div>
      </section>

      {/* How It Works Section */}
      <section className="py-20 bg-gradient-to-br from-amber-900 to-yellow-800 text-white">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <h2 className="text-4xl md:text-5xl font-bold text-center mb-16">
            How It Works
          </h2>

          <div className="grid md:grid-cols-4 gap-6">
            <div className="flex flex-col items-center">
              <div className="w-16 h-16 bg-yellow-300 text-amber-900 rounded-full flex items-center justify-center text-2xl font-bold mb-4">
                1
              </div>
              <h3 className="text-lg font-semibold mb-2">Add Honey</h3>
              <p className="text-center text-yellow-100">
                Specify honey weight in your recipe
              </p>
            </div>

            <div className="flex flex-col items-center">
              <div className="w-16 h-16 bg-yellow-300 text-amber-900 rounded-full flex items-center justify-center text-2xl font-bold mb-4">
                2
              </div>
              <h3 className="text-lg font-semibold mb-2">Select Fruits</h3>
              <p className="text-center text-yellow-100">
                Choose from our database of fruits and juices
              </p>
            </div>

            <div className="flex flex-col items-center">
              <div className="w-16 h-16 bg-yellow-300 text-amber-900 rounded-full flex items-center justify-center text-2xl font-bold mb-4">
                3
              </div>
              <h3 className="text-lg font-semibold mb-2">Set Parameters</h3>
              <p className="text-center text-yellow-100">
                Choose target ABV, volume, or honey weight
              </p>
            </div>

            <div className="flex flex-col items-center">
              <div className="w-16 h-16 bg-yellow-300 text-amber-900 rounded-full flex items-center justify-center text-2xl font-bold mb-4">
                4
              </div>
              <h3 className="text-lg font-semibold mb-2">Get Results</h3>
              <p className="text-center text-yellow-100">
                View detailed calculations and nutrient info
              </p>
            </div>
          </div>
        </div>
      </section>

      {/* CTA Section */}
      <section className="py-20 bg-white">
        <div className="max-w-3xl mx-auto px-4 sm:px-6 lg:px-8 text-center">
          <h2 className="text-4xl font-bold text-amber-900 mb-6">
            Ready to Brew?
          </h2>
          <p className="text-xl text-amber-700 mb-8">
            Start calculating your perfect mead recipe today. Our calculator makes it easy to create consistent, delicious batches.
          </p>
          <Link
            to="/calculator"
            className="inline-block bg-gradient-to-r from-amber-600 to-yellow-600 text-white px-12 py-4 rounded-lg font-semibold hover:from-amber-700 hover:to-yellow-700 transition shadow-lg text-lg"
          >
            Open Calculator
          </Link>
        </div>
      </section>

      {/* Footer */}
      <footer className="bg-amber-900 text-white py-12">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="grid md:grid-cols-3 gap-8 mb-8">
            <div>
              <h3 className="text-lg font-semibold mb-4 flex items-center">
                <span className="text-2xl mr-2">🍯</span>
                MeadCalculator
              </h3>
              <p className="text-yellow-100">
                The essential tool for mead makers everywhere.
              </p>
            </div>
            <div>
              <h4 className="text-lg font-semibold mb-4">Quick Links</h4>
              <ul className="space-y-2 text-yellow-100">
                <li>
                  <Link to="/" className="hover:text-white transition">
                    Home
                  </Link>
                </li>
                <li>
                  <Link to="/calculator" className="hover:text-white transition">
                    Calculator
                  </Link>
                </li>
              </ul>
            </div>
            <div>
              <h4 className="text-lg font-semibold mb-4">About</h4>
              <p className="text-yellow-100">
                Created by mead enthusiasts for mead enthusiasts.
              </p>
            </div>
          </div>
          <div className="border-t border-yellow-700 pt-8 text-center text-yellow-100">
            <p>
              &copy; 2025 MeadCalculator. All rights reserved.
            </p>
          </div>
        </div>
      </footer>
    </div>
  )
}
