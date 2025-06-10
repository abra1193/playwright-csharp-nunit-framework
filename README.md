# Playwright - Csharp - NUnit Framework

This repository contains a QA automation framework for web pages and API endpoints. The framework is organized for both UI and API automated testing using .NET, NUnit, and Playwright.

---

## Project Structure

```
qa-automation-exercise--mejiabritoabraham-1.sln         # Solution file
qa-automation-exercise--mejiabritoabraham.csproj        # Project file
README.md                                               # Project documentation

.github/
  workflows/
    dotnet.yml                                          # GitHub Actions CI workflow

Base/
  API/                                                  # Base classes/utilities for API tests
  UI/                                                   # Base classes/utilities for UI tests

Framework/
  API/                                                  # API framework (helpers, clients, etc.)
  UI/                                                   # UI framework (pages, components, etc.)

Shared/
  Constants.cs                                          # Shared constants
  Randomizer.cs                                         # Shared randomization utilities

Tests/
  API/                                                  # API test cases
  UI/                                                   # UI test cases

bin/, obj/                                              # Build output and intermediate files
```
---
## Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) (for Playwright installation)
- Chrome/Edge/Firefox browsers (Playwright will install them automatically)

### Setup

1. **Restore dependencies:**
   ```sh
   dotnet restore
   ```

2. **Install Playwright browsers:**
   ```sh
   npx playwright install
   ```

---

## Tests

### Test Organization

- **UI Tests:**  
  Located in `Tests/UI/`. These tests use Playwright to automate browser interactions and validate the product page’s UI and analytics events.

  **UI Test Classes & Methods:**

    - `ProductPageTests`
        - `ShouldLoadPageMenu`  
          *Verifies that the page menu loads and displays correctly.*
        - `ShouldLoadPageTitle`  
          *Checks that the page title is present and correct.*
        - `ShouldDisplayNewsletterSubConfirmationMessage`  
          *Ensures the newsletter subscription confirmation message appears after subscribing.*
        - `ShouldContainAmazonLinksOnTagParameter`  
          *Checks that Amazon links include the required `tag` query parameter.*
        - `ShouldTriggerGoogleAnalyticsEvents`  
          *Validates that Google Analytics events are triggered as expected (e.g., page view, promotion view, promotion select).*
        - `ShouldContainPromotionIdOnTagParameter`  
          *Ensures that the promotion ID is included in the `tag` parameter of Amazon links.*

- **API Tests:**  
  Located in `Tests/API/`. These tests validate the product management API endpoints, including product creation, retrieval, and partner access control.

  **API Test Classes & Methods:**

    - `ProductApiTests`
        - `ShouldCreateProduct`  
          *Verifies that a product can be created successfully with valid data.*
        - `ShouldRetrieveProduct`  
          *Checks that a created product can be retrieved and its data is correct.*
        - `ShouldNotAccessOtherPartnerProducts`  
          *Ensures that a partner cannot access products belonging to another partner.*
        - `ShouldReturnsBadRequestOnInvalidProductId`  
          *Checks that the API returns a bad request response when the product ID is invalid.*
        - `ShouldCreateProductWithoutDescription`  
          *Verifies that a product can be created without providing a description.*
        - `ShouldReturnUnauthorizedWhenPartnerHeaderIsMissing`  
          *Ensures the API returns an unauthorized response if the `X-Partner-ID` header is missing.*

### Running Tests

- **All tests:**
  ```sh
  dotnet test
  ```

- **UI tests only:**
  ```sh
  dotnet test --filter Category=UI
  ```

- **API tests only:**
  ```sh
  dotnet test --filter Category=API
  ```

### Test Results

- Test results are displayed in the terminal after execution.
- For more detailed output or to generate test reports, you can use:
  ```sh
  dotnet test --logger "console;verbosity=detailed"
  ```
- In CI, results are available in the GitHub Actions workflow summary.

---

### Allure Reports

Allure is used to generate rich, interactive test reports.

#### How to Configure Allure

1. **Install Allure CLI**  
   You can install Allure globally using Homebrew (recommended for Mac):
   ```sh
   brew install allure
   ```
   Or download from [Allure Releases](https://github.com/allure-framework/allure2/releases).

2. **Add Allure NUnit Adapter**  
   Add the Allure adapter NuGet package to your project:
   ```sh
   dotnet add package Allure.NUnit
   ```

3. **Run Tests with Allure Output**  
   Run your tests with the Allure logger:
   ```sh
   dotnet test --logger:"allure"
   ```
   This will generate results in the `allure-results` directory.

4. **Generate and View the Report Locally**
   ```sh
   allure generate allure-results --clean -o allure-report
   allure open allure-report
   ```
   This will open the Allure report in your browser.

#### Allure Reports in CI

- Allure results are collected as artifacts in the CI pipeline (see `.github/workflows/dotnet.yml`).
- After each CI run, you can download the Allure report artifacts from the GitHub Actions summary page.
- To view the report locally:
    1. Download the `allure-results` artifact from the workflow run.
    2. Extract it and run:
       ```sh
       allure generate allure-results --clean -o allure-report
       allure open allure-report
       ```

---

## Continuous Integration

GitHub Actions is configured via [.github/workflows/dotnet.yml](.github/workflows/dotnet.yml) to run tests on push and pull requests.

---

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Open a pull request

---

## License

This project is for educational and evaluation purposes only.

---

## Authors

- Abraham Mejía Brito
