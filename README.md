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

## 💻 Setup
### 🔧 Pre-Setup Requirements
✅ Required on all platforms:
- Git
- .NET SDK 8.0+
- Docker

### 🔧 Pre-Setup Requirements
#### 🪟 Windows
- Use Git Bash (not PowerShell or CMD)

### 1. 🧾 Clone and move to the repository.
git clone https://github.com/FlokiPatris/Safetica.git && cd Safetica       

### 2. 🚀 Run the setup script in the same folder where the repository is cloned. (Using Git Bash)
bash setup.sh

### 3. ▶️ Run the test (With Browser)
dotnet test

### 4. 🐳 Run in Docker (Without Browser)
docker-compose up --build
