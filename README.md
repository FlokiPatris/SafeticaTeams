# Safetica
Safetica tests with pipeline set up.

## 🔐 Configuration
This project uses **GitHub Secrets** to securely inject credentials and configuration.  
No `.env` file is required or supported.

### Secrets used:
- `TEAMS_LOGIN`
- `TEAMS_PASSWORD`
- `TEAMS_CHAT_NAME`
- `TEAMS_BASE_URL`

> 💡 All secrets are injected via GitHub Actions and are not stored in the repository.

## 🔧 Pre-Setup Requirements
Before running the setup script, make sure the following tools are installed:

- **Git** – for cloning the repository  
- **.NET SDK 8.0+** – required to build and run tests  
- **Docker** – for running tests in containers  
- **Node.js** – required for Playwright CLI

> 💡 These tools must be installed manually. The setup script does not install them automatically.

### 🪟 Windows
- Use Git Bash (not PowerShell or CMD)

## 💻 Setup
### 1. 🧾 Clone and move to the repository.
git clone https://github.com/FlokiPatris/Safetica.git && cd Safetica 

### 2. 🚀 Run the setup script in the same folder where the repository is cloned. (Using Git Bash)
bash setup.sh

## 🧪 Running Tests
### 1. ▶️ Run tests with browser (headless = false)
dotnet test

### 2. 🐳 Run tests in Docker (headless = true)
docker-compose up --build

