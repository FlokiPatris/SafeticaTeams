# 🧰 Use .NET 9.0 SDK preview image
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview

# 🧩 Install system dependencies required by Playwright browsers
RUN apt-get update && \
    apt-get install -y --no-install-recommends \
    curl gnupg wget xdg-utils lsb-release \
    libnss3 libatk-bridge2.0-0 libxcomposite1 libxdamage1 libxrandr2 libgbm1 \
    libasound2 libxshmfence1 libgtk-3-0 libx11-xcb1 libdrm2 libxfixes3 \
    libxext6 libxrender1 libxcb1 libx11-6 libxtst6 libnss3-tools libxss1 \
    libgconf-2-4 libpango-1.0-0 libpangocairo-1.0-0 fonts-liberation \
    libappindicator3-1 libatk1.0-0 libcups2 libdbus-glib-1-2 \
    libgdk-pixbuf2.0-0 libnspr4 libxinerama1 && \
    apt-get clean && rm -rf /var/lib/apt/lists/*

# 🟢 Install Node.js and Playwright CLI
RUN curl -fsSL https://deb.nodesource.com/setup_18.x | bash - && \
    apt-get install -y nodejs && \
    npm install -g playwright && \
    dotnet tool install --global Microsoft.Playwright.CLI

# 🔧 Add dotnet tools to PATH
ENV PATH="$PATH:/root/.dotnet/tools"

# 📁 Set working directory and copy project files
WORKDIR /app
COPY . .

# 📁 Move into test project folder
WORKDIR /app/SafeticaTests

# 🔧 Restore .NET dependencies
RUN dotnet restore

# 🌐 Install Playwright browsers and dependencies
RUN playwright install --with-deps

# ▶️ Run tests when container starts
CMD ["dotnet", "test"]