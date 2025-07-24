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
### 1. 🚀 Run the setup script (Using Git Bash)
bash setup.sh

### 2. ▶️ Run the test (With Browser)
dotnet test

### 3. 🐳 Run in Docker (Without Browser)
docker-compose up --build
